using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementProject
{
    public class AsyncTcpClient : IDisposable
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private TaskCompletionSource<bool> closedTcs = new TaskCompletionSource<bool>();
        public event EventHandler<AsyncTcpEventArgs> Message;
        public AsyncTcpClient()
        {
            closedTcs.SetResult(true);
        }

        #region TcpClient属性
        public TcpClient ServerTcpClient { get; set; }
        /// <summary>
        /// Tcp连接等待时间
        /// </summary>
        public TimeSpan ConnectTimeout { get; set; } =TimeSpan.FromSeconds(5);
        /// <summary>
        /// Tcp重连等待时间
        /// </summary>
        public TimeSpan MaxConnectTimeout { get; set; } = TimeSpan.FromSeconds(1);
        /// <summary>
        /// 自动断线重新连接
        /// </summary>
        public bool AutoReconnect { get; set; }
        /// <summary>
        /// 连接服务器名称
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 服务器IP地址
        /// 当<see cref="HostName"/> 为空时考虑.
        /// </summary>
        public IPAddress IPAddress { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        public ByteBuffer ByteBuffer { get; private set; } = new ByteBuffer();
        /// <summary>
        /// <see cref="Task"/> 这是可等待关闭连接。
        /// 远程关闭连接时，此任务将完成。
        /// </summary>
        public Task ClosedTask => closedTcs.Task;
        /// <summary>
        /// 获取<see cref="ClosedTask"/> 完成状态.
        /// </summary>
        public bool IsClosing => ClosedTask.IsCompleted;
        public Func<AsyncTcpClient, bool, Task> ConnectedCallback { get; set; }
        public Action<AsyncTcpClient, bool> ClosedCallback { get; set; }
        public Func<AsyncTcpClient, int, Task> ReceivedCallback { get; set; }
        #endregion

        public async Task RunAsync()
        {
            bool isReconnected = false;
            int reconnectTry = -1;
            do
            {
                reconnectTry++;
                ByteBuffer = new ByteBuffer();
                if (ServerTcpClient != null)
                {
                    // Take accepted connection from listener
                    tcpClient = ServerTcpClient;
                }
                else
                {
                    // 连接远程服务器
                    var connectTimeout = TimeSpan.FromTicks(ConnectTimeout.Ticks + (MaxConnectTimeout.Ticks - ConnectTimeout.Ticks) / 20 * Math.Min(reconnectTry, 20));
                    tcpClient = new TcpClient(AddressFamily.InterNetworkV6);
                    tcpClient.Client.DualMode = true;
                    Message?.Invoke(this, new AsyncTcpEventArgs("正在连接服务器"));
                    Task connectTask;
                    if (!string.IsNullOrWhiteSpace(HostName))
                    {
                        connectTask = tcpClient.ConnectAsync(HostName, Port);
                    }
                    else
                    {
                        connectTask = tcpClient.ConnectAsync(IPAddress, Port);
                    }
                    var timeoutTask = Task.Delay(connectTimeout);
                    if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                    {
                        Message?.Invoke(this, new AsyncTcpEventArgs("连接超时！"));
                        continue;
                    }
                    try
                    {
                        await connectTask;
                    }
                    catch (Exception ex)
                    {
                        Message?.Invoke(this, new AsyncTcpEventArgs("连接服务器出错！", ex));
                        await timeoutTask;
                        continue;
                    }
                }
                reconnectTry = -1;
                stream = tcpClient.GetStream();
                // Read until the connection is closed.
                // A closed connection can only be detected while reading, so we need to read
                // permanently, not only when we might use received data.
                var networkReadTask = Task.Run(async () =>
                {
                    // 10 KiB should be enough for every Ethernet packet
                    byte[] buffer = new byte[10240];
                    while (true)
                    {
                        int readLength;
                        try
                        {
                            readLength = await stream.ReadAsync(buffer, 0, buffer.Length);
                        }
                        catch (IOException ex) when ((ex.InnerException as SocketException)?.ErrorCode == (int)SocketError.OperationAborted)
                        {
                            // 详细错误代码查阅 https://docs.microsoft.com/en-us/windows/desktop/winsock/windows-sockets-error-codes-2
                            Message?.Invoke(this, new AsyncTcpEventArgs("本地连接已关闭", ex));
                            readLength = -1;
                        }
                        catch (IOException ex) when ((ex.InnerException as SocketException)?.ErrorCode == (int)SocketError.ConnectionAborted)
                        {
                            Message?.Invoke(this, new AsyncTcpEventArgs("连接失败", ex));
                            readLength = -1;
                        }
                        catch (IOException ex) when ((ex.InnerException as SocketException)?.ErrorCode == (int)SocketError.ConnectionReset)
                        {
                            Message?.Invoke(this, new AsyncTcpEventArgs("远程连接已重置", ex));
                            readLength = -2;
                        }
                        if (readLength <= 0)
                        {
                            if (readLength == 0)
                            {
                                Message?.Invoke(this, new AsyncTcpEventArgs("远程连接已关闭"));
                            }
                            closedTcs.TrySetResult(true);
                            OnClosed(readLength != -1);
                            return;
                        }
                        var segment = new ArraySegment<byte>(buffer, 0, readLength);
                        ByteBuffer.Enqueue(segment);
                        await OnReceivedAsync(readLength);
                    }
                });
                closedTcs = new TaskCompletionSource<bool>();
                await OnConnectedAsync(isReconnected);
                // 等待关闭连接
                await networkReadTask;
                tcpClient.Close();

                isReconnected = true;
            }
            while (AutoReconnect && ServerTcpClient == null);
        }

        /// <summary>
        /// Waits asynchronously until received data is available in the buffer.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token used to propagate notification that this operation should be canceled.</param>
        /// <returns>获取数据返回true; 连接断开返回false.</returns>
        /// <exception cref="OperationCanceledException">The <paramref name="cancellationToken"/> was canceled.</exception>
        public async Task<bool> WaitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.WhenAny(ByteBuffer.WaitAsync(cancellationToken), closedTcs.Task) != closedTcs.Task;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task Send(ArraySegment<byte> data)
        {
            await stream.WriteAsync(data.Array, data.Offset, data.Count);
        }


        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    AutoReconnect = false;
                    tcpClient?.Dispose();
                    stream = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AsyncTcpClient() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion


        #region Protected virtual methods

        /// <summary>
        /// 当客户端连接到拂去其发生. This method can implement the
        /// communication logic to execute when the connection was established. The connection will
        /// not be closed before this method completes.
        /// </summary>
        /// <param name="isReconnected">true, if the connection was closed and automatically reopened;
        ///   false, if this is the first established connection for this client instance.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        protected virtual Task OnConnectedAsync(bool isReconnected)
        {
            if (ConnectedCallback != null)
            {
                return ConnectedCallback(this, isReconnected);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when the connection was closed.
        /// </summary>
        /// <param name="remote">true, if the connection was closed by the remote host; false, if
        ///   the connection was closed locally.</param>
        protected virtual void OnClosed(bool remote)
        {
            ClosedCallback?.Invoke(this, remote);
        }

        /// <summary>
        /// Called when data was received from the remote host. This method can implement the
        /// communication logic to execute every time data was received. New data will not be
        /// received before this method completes.
        /// </summary>
        /// <param name="count">The number of bytes that were received. The actual data is available
        ///   through the <see cref="ByteBuffer"/>.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        protected virtual Task OnReceivedAsync(int count)
        {
            if (ReceivedCallback != null)
            {
                return ReceivedCallback(this, count);
            }
            return Task.CompletedTask;
        }

        #endregion Protected virtual methods

    }
    /// <summary>
	/// 为 <see cref="AsyncTcpClient.Message"/> 事件提供数据
	/// </summary>
	public class AsyncTcpEventArgs : EventArgs
    {
        /// <summary>
        /// 初始化<see cref="AsyncTcpEventArgs"/>
        /// </summary>
        /// <param name="message">连接信息</param>
        /// <param name="exception">异常</param>
        public AsyncTcpEventArgs(string message, Exception exception = null)
        {
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; }
    }
}
