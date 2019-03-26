using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using com.mango.protocol.Enum;
using com.mango.protocol.msg;
//using log4net;
//using Newtonsoft.Json;

namespace com.mango.protocol
{
    public interface IReadMessage
    {
    }

    public interface IWriteMessage
    {
        void write(InStream buffer);
        short getProtocol();
    }

    public class Message
    {
        short protocol;
        long seq;
        IWriteMessage message;

        public Message(IWriteMessage message)
        {
            this.protocol = message.getProtocol();
            this.message = message;
        }

        public long getSeq()
        {
            return seq;
        }

        public void setSeq(long seq)
        {
            this.seq = seq;
        }

        public byte[] ToBytes()
        {
            InStream stream = new InStream();
            stream.WriteInt(0);
            stream.WriteLong(seq);
            stream.WriteShort(protocol);
            message.write(stream);
            int size = stream.GetSize();
            stream.WriteInt(0, size);
            return stream.GetBuffer();
        }
    }

    public class Stream
    {
        protected int writeIndex;
        protected int readIndex;
        protected byte[] buffer;

        private int capacity;

        public byte[] GetBuffer()
        {
            byte[] tmp = new byte[writeIndex];
            System.Array.Copy(buffer, tmp, writeIndex);
            return buffer;
        }



        public void Reset()
        {
            this.writeIndex = 0;
            this.readIndex = 0;
        }

        public int Readable()
        {
            return this.writeIndex - this.readIndex;
        }

        public int GetSize()
        {
            return this.writeIndex;
        }

        public Stream(byte[] buffer)
        {
            this.buffer = buffer;
            this.capacity = buffer.Length;
        }

        public Stream(int capacity)
        {
            this.buffer = new byte[capacity];
            this.capacity = capacity;
        }

        public int GetInt()
        {
            return ((int)this.buffer[readIndex] & 0xff) << 24 |
                    ((int)this.buffer[readIndex+1] & 0xff) << 16 |
                    ((int)this.buffer[readIndex+2] & 0xff) << 8 |
                    (int)this.buffer[readIndex+3] & 0xff;
        }

        public void ReadBytes(byte[] value,int size)
        {
            System.Array.Copy(buffer, readIndex, value,0, size);
            this.readIndex += size;
        }

        public void WriteInt(int index,int value)
        {
            this.buffer[index++] = (byte)(value >> 24);
            this.buffer[index++] = (byte)(value >> 16);
            this.buffer[index++] = (byte)(value >> 8);
            this.buffer[index++] = (byte)value;
        }

        public void WriteBytes(byte[] value,int size)
        {
            CheckSize(size);
            System.Array.Copy(value, 0, buffer, writeIndex, size);
            this.writeIndex += size;
        }

        protected void CheckSize(int size)
        {
            if(size + writeIndex < capacity)
            {
                return;
            }
            int newSize = capacity * 2 + size;
            byte[] tmp = new byte[newSize];
            System.Array.Copy(buffer, tmp, buffer.Length);

            this.buffer = tmp;
            this.capacity = newSize;
        }
    }

    public class OutStream:Stream
    {
        public OutStream(byte[] buffer):base(buffer)
        {

        }

        public bool ReadBool()
        {
            return this.buffer[readIndex++] == 1;
        }

        public double ReadDouble()
        {
            long tmp = ReadLong();
            return BitConverter.Int64BitsToDouble(tmp);
        }

        public string ReadString()
        {
            int size = ReadInt();
            byte[] tmp = new byte[size];
            ReadBytes(tmp, size);

            return System.Text.Encoding.UTF8.GetString(tmp);
        }

        public sbyte ReadSByte()
        {
            return (sbyte)this.buffer[readIndex++];
        }

        public short ReadShort()
        {
            return (short)(((short)this.buffer[readIndex++] & 0xff) << 8 | (short)this.buffer[readIndex++] & 0xff);
        }

        public int ReadInt()
        {
            return  ((int)this.buffer[readIndex++] & 0xff) << 24 |
                    ((int)this.buffer[readIndex++] & 0xff) << 16 |
                    ((int)this.buffer[readIndex++] & 0xff) << 8 |
                    (int)this.buffer[readIndex++] & 0xff;
        }

        public long ReadLong()
        {
            return ((long)this.buffer[readIndex++] & 0xff) << 56 |
                ((long)this.buffer[readIndex++] & 0xff) << 48 |
                ((long)this.buffer[readIndex++] & 0xff) << 40 |
                ((long)this.buffer[readIndex++] & 0xff) << 32 |
                ((long)this.buffer[readIndex++] & 0xff) << 24 |
                ((long)this.buffer[readIndex++] & 0xff) << 16 |
                ((long)this.buffer[readIndex++] & 0xff) << 8 |
                (long)this.buffer[readIndex++] & 0xff;

        }
    }

    public class MangoSocketClient
    {
        private static Object seqLock = new Object();

        private TcpClient client;
        private String host;
        private int port;
        private MessageHandler handler;
        private long seq = 0x01;
        private bool _exit;
        

        public MangoSocketClient(String host,int port,MessageHandler handler)
        {
            client  = new TcpClient();
            this.host = host;
            this.port = port;
            this.handler = handler;
        }

        public bool isConnected()
        {
            return client.Connected;
        }
        
        private void ParseMessage(OutStream os)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(_ParseMessage));
            thread.Start(os);
        }

        private void _ParseMessage(object obj)
        {
            handler.parseMessage(this, obj as OutStream);
        }

        private void Decode(Stream buffer)
        {
            while (buffer.Readable() >= 4)
            {
                int length = buffer.GetInt();
                if (buffer.Readable() >= length)
                {
                    byte[] tmp = new byte[length];
                    buffer.ReadBytes(tmp, length);
                    try
                    {
                        OutStream os = new OutStream(tmp);
                        handler.parseMessage(null, os);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    break;
                }
            }
            
        }

        private void KeepAlive()
        {
            long time = 0;
            while (client.Connected && !_exit)
            {
                if(time >= 12000)
                {
                    CS_KeepAlive alive = new CS_KeepAlive();
                    alive.datetime = DateTime.UtcNow.ToFileTimeUtc();
                    this.Write(alive,(short)alive.protocol);
                    time = 0;
                }
                
                Thread.Sleep(100);
                time += 100;
            }
            Console.WriteLine("KeepAlive Exit");
        }

       

        private void Read()
        {
            NetworkStream stream = client.GetStream();
            Stream buffer = new Stream(1024);
            try
            {
                while (client.Connected && !_exit)
                {
                    byte[] _read = new byte[1024];
                    int size = stream.Read(_read, 0, 1024);
                    if (size > 0)
                    {
                        buffer.WriteBytes(_read, size);
                        this.Decode(buffer);
                        if (buffer.Readable() == 0)
                        {
                            buffer.Reset();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }catch(Exception e)
            {
               // LogManager.GetLogger("MangoSocketClient").Error(""+e);
                Console.WriteLine(e.ToString());
            }
            if (!_exit)
            {
                Thread thread = new Thread(_OnDisconnected);
                thread.Start();
            }
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId+" Read Exit");            
        }

        private void _OnDisconnected()
        {
            handler.onDisconnected(this);
        }

        private void _Write(object message,short protocol, long seq)
        {
            InStream stream = new InStream();
            stream.WriteInt(0);
            stream.WriteLong(seq);
            stream.WriteShort(protocol);
            byte[] buffer = Package.Serizlize(message);
            stream.WriteBytes(buffer, buffer.Length);
            //message.write(stream);
            stream.WriteInt(0, stream.GetSize());
            byte[] data = stream.GetBuffer();
           // CS_LOGIN mm = (CS_LOGIN)Package.DeSerizlize<CS_LOGIN>(buffer);

            Write(data);
        
        }

        public long GetSeq()
        {
            lock (seqLock)
            {
                this.seq++;
                return seq;
            }
        }

        public void Write(object message, short protocol, long seq)
        {
                 
            _Write(message, protocol, seq);
        }

        public void Write(object message,short protocol)
        {
            _Write(message, protocol, 0);
        }

        private void Write(byte[] data)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        public void exit()
        {
            this._exit = true;
            this.client.Close();
        }

        public void Start()
        {
            this._exit = false;
            while (!_exit)
            {
                try
                {
                    handler.onConnectStart(this);
                    client = new TcpClient();
                    Task task = client.ConnectAsync(host, port);
                    task.Wait(5000);
                    break;
                }catch(Exception e)
                {
                    Thread.Sleep(3000);
                    //LogManager.GetLogger("MangoSocketClient").Error("Connect failed:" + e);
                    handler.onConnectFailed(this);
                }
                
            }
            if (client != null && client.Connected)
            {
                Thread thread = new Thread(Read);
                thread.Start();
                Console.WriteLine(thread.ManagedThreadId + "Read Start");

                new Thread(KeepAlive).Start();
                handler.onConnected(this);
            }
        }
    }

    public class InStream :Stream
    {
        public InStream():base(128)
        {
            
        }
                
        public void WriteDouble(double value)
        {
            long tmp    = BitConverter.DoubleToInt64Bits(value);
            WriteLong(tmp);
        }

        public void WriteSByte(sbyte value)
        {
            this.CheckSize(1);
            this.buffer[writeIndex++] = (byte)value;
        }
        public void WriteBool(bool value)
        {
            this.CheckSize(1);
            this.buffer[writeIndex++] = (byte)(value ? 1:0);
        }

        public void WriteLong(long value)
        {
            this.CheckSize(8);
            this.buffer[writeIndex++] = (byte)(value >> 56);
            this.buffer[writeIndex++] = (byte)(value >> 48);
            this.buffer[writeIndex++] = (byte)(value >> 40);
            this.buffer[writeIndex++] = (byte)(value >> 32);
            this.buffer[writeIndex++] = (byte)(value >> 24);
            this.buffer[writeIndex++] = (byte)(value >> 16);
            this.buffer[writeIndex++] = (byte)(value >> 8);
            this.buffer[writeIndex++] = (byte)value;
        }

        public void WriteString(string value)
        {
            if(value == null)
            {
                WriteInt(0);
                return;
            }
            byte[] tmp  = System.Text.Encoding.UTF8.GetBytes(value);
            WriteInt(tmp.Length);
            WriteBytes(tmp, tmp.Length);
        }
        public void WriteShort(short value)
        {
            this.CheckSize(2);
            this.buffer[writeIndex++] = (byte)(value >> 8);
            this.buffer[writeIndex++] = (byte)value;
        }

        public void WriteInt(int value)
        {
            this.CheckSize(4);
            this.buffer[writeIndex++] = (byte)(value >> 24);
            this.buffer[writeIndex++] = (byte)(value >> 16);
            this.buffer[writeIndex++] = (byte)(value >> 8);
            this.buffer[writeIndex++] = (byte)value;
        }
    }


    public static class Package
    {            
        /// <summary>
        /// 数组转结构体
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="strcutType"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        public static object BytesToStruct(byte[] bytes, Type strcutType, int nSize)
        {
            if (bytes == null)
            {
               // LogManager.GetLogger("MangoSocketClient").Error("Buffer is Null");
            }
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(nSize);
            try
            {
                Marshal.Copy(bytes, 0, buffer, nSize);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            catch (Exception ex)
            {
               // LogManager.GetLogger("MangoSocketClient").Error("Type: " + strcutType.ToString() + "---TypeSize:" + size + "----packetSize:" + nSize);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static object SCStruct<T>(OutStream buffer)
        {
            return DeSerizlize<T>(buffer.GetBuffer());
        }
        /// <summary>
        ///  将消息序列化为二进制的方法
        /// </summary>
        /// <param name="meg">要序列化的对象</param>
        /// <returns></returns>
        public static byte[] Serizlize(object meg)
        {
            try
            {
                //涉及格式转换，需要用到流，将二进制序列化到流中
                using (MemoryStream ms = new MemoryStream())
                {
                    //使用ProtoBuf工具的序列化方法
                    ProtoBuf.Serializer.Serialize(ms, meg);

                    //定义二级制数组，保存序列化后的结果
                    byte[] result = new byte[ms.Length];
                    //将流的位置设为0，起始点
                    //ms.Seek(0, SeekOrigin.Begin);
                    ms.Position = 0;
                    //将流中的内容读取到二进制数组中
                    ms.Read(result, 0, result.Length);

                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 将收到的消息反序列化成对象
        /// </summary>
        /// <param name="msg">收到的消息</param>
        /// <returns></returns>
        public static object DeSerizlize<T>(byte[] msg)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //将消息写入流中
                    ms.Write(msg, 14, msg.Length-14);
                    //将流的位置归0
                    ms.Position = 0;
                    //使用工具反序列化对象
                    object mm = ProtoBuf.Serializer.Deserialize<T>(ms);
                    return mm;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }


    class CountDownLatch
    {
        private object lockObj = new Object();
        private int counter;

        public CountDownLatch()
        {
            this.counter = 1;
        }

        public void Await()
        {
            lock (lockObj)
            {
                while (counter > 0)
                {
                    Monitor.Wait(lockObj, 15000); // 等待15秒，如果没有返回值则超时
                }
            }
        }

        public void CountDown()
        {
            lock (lockObj)
            {
                counter--;
                Monitor.PulseAll(lockObj);
            }
        }
    }

    public class SeqCallback
    {
        private OutStream buffer;
        private CountDownLatch cd = new CountDownLatch();
        public void onMessage(OutStream buffer)
        {
            this.buffer = buffer;
            cd.CountDown();
        }

        public OutStream Await()
        {
            cd.Await();
            return buffer;
        }
    }

    public abstract class MessageHandler
    {
        public abstract void onConnectFailed(MangoSocketClient client);
        public abstract void onDisconnected(MangoSocketClient client);
        public abstract void onReconnected(MangoSocketClient client);
        public abstract void onConnected(MangoSocketClient client);
        public abstract void onConnectStart(MangoSocketClient client);

        public virtual void onSC_KeepAlive(Object session, SC_KeepAlive message)
        {
            //throw new NotImplementedException();
        }
        public virtual void onSC_LOGIN(Object session, SC_LOGIN message)
        {
            //throw new NotImplementedException();
        }
        public virtual void onSC_LOGOUT(Object session, SC_LOGOUT message)
        {
            //throw new NotImplementedException();
        }
        //public virtual void onSC_PushClient(Object session, SC_PushClient message)
        //{
        //    //throw new NotImplementedException();
        //}
        public virtual void onSC_Alarm(Object session, SC_Alarm message)
        {
            //throw new NotImplementedException();
        }
        //public virtual void onSC_SendSMS(Object session, SC_SendSMS message)
        //{
        //    //throw new NotImplementedException();
        //}

        private Hashtable seqMap = new Hashtable();

        public OutStream Await(MangoSocketClient client, object message,short protocol)
        {
            long seq = client.GetSeq();
            SeqCallback callback = new SeqCallback();
            seqMap.Add(seq, callback);
            client.Write(message, protocol, seq);
            return callback.Await();
        }

        public object SCStruct<T>(OutStream buffer)
        {
            return Package.DeSerizlize<T>(buffer.GetBuffer());
        }
        public void parseMessage(Object session, OutStream buffer)
        {
            int length = buffer.ReadInt();
            long seq = buffer.ReadLong();
            Protocol protocol =(Protocol)buffer.ReadShort();
            if (seq > 0 && seqMap.ContainsKey(seq))
            {
                SeqCallback callback = (SeqCallback)seqMap[seq];
                seqMap.Remove(seq);
                callback.onMessage(buffer);
                return;
            }

            Package.BytesToStruct(buffer.GetBuffer(), typeof(SC_KeepAlive), buffer.GetBuffer().Length);
            switch (protocol)
            {
                case Protocol.SC_KEEPALIVE:
                    onSC_KeepAlive(session, (SC_KeepAlive)SCStruct<SC_KeepAlive>(buffer));
                    break;
                case Protocol.SC_LOGIN:
                    onSC_LOGIN(session, (SC_LOGIN)SCStruct<SC_LOGIN>(buffer));
                    break;
                case Protocol.SC_LOGOUT:
                    onSC_LOGOUT(session, (SC_LOGOUT)SCStruct<SC_LOGOUT>(buffer));
                    break;
                //case Protocol.SC_PUSHCLIENT:
                //    onSC_PushClient(session, (SC_PushClient)SCStruct<SC_PushClient>(buffer));
                //    break;
                case Protocol.SC_ALARM:
                    onSC_Alarm(session, (SC_Alarm)SCStruct<SC_Alarm>(buffer) );
                    break;
                    break;
                //case Protocol.SC_SENDSMS:
                //    onSC_SendSMS(session, (SC_SendSMS)SCStruct<SC_SendSMS>(buffer));
                //    break;
            }
        }
    }
}
