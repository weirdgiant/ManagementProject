using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementProject
{
    public class AsyncTcpClient
    {
        private TcpClient _tcpClient;
        private bool _isConnected = false;//是否连接
        private bool _isClosed = false;//软件是否关闭
        private IPAddress _ip = null;
        private int _port = 0;
        private Socket _socket = null;
        private static ManualResetEvent _connectDone = new ManualResetEvent(false);
        private static ManualResetEvent _sendDone = new ManualResetEvent(false);
        private static ManualResetEvent _receiveDone = new ManualResetEvent(false);
        public AsyncTcpClient(IPAddress ip, int port)
        {
            this._ip = ip;
            this._port = port;
        }

    }
}
