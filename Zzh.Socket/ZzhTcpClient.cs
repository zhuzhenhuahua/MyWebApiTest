using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zzh.Socket
{
    public class ZzhTcpClient : SocketObject
    {
        bool IsClose = false;
        /// <summary>
        /// 当前管理对象
        /// </summary>
        Sockets sk;
        /// <summary>
        /// 当前连接服务器地址
        /// </summary>
        IPAddress Ipaddress;
        /// <summary>
        /// 当前连接服务器端口号
        /// </summary>
        int Port;
        /// <summary>
        /// 服务器端IP+端口
        /// </summary>
        IPEndPoint ip;
        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient client;
        /// <summary>
        /// 发送与接收使用的流
        /// </summary>
        NetworkStream nStream;
        /// <summary>
        /// 初始化Socket
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public override void InitSocket(string ipAddress, int port)
        {
            Ipaddress = IPAddress.Parse(ipAddress);
            Port = port;
            ip = new IPEndPoint(Ipaddress, Port);
            client = new TcpClient();
        }
        /// <summary>
        /// 初始化Socket
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public override void InitSocket(IPAddress ipAddress, int port)
        {
            Ipaddress = ipAddress;
            Port = port;
            ip = new IPEndPoint(Ipaddress, Port);
            client = new TcpClient();
        }

        private void Connect()
        {
            client.Connect(ip);
            nStream = new NetworkStream(client.Client, true);
            sk = new Sockets(this.ip, this.client, nStream);
            sk.nStream.BeginRead(sk.RecBuffer, 0, sk.RecBuffer.Length, new AsyncCallback(EndReader), sk);
        }
        public static PushSockets pushSockets;
        private void EndReader(IAsyncResult ir)
        {
            Sockets s = ir.AsyncState as Sockets;
            try
            {
                if (s != null)
                {
                    if (IsClose && client == null)
                    {
                        sk.nStream.Close();
                        sk.nStream.Dispose();
                        return;
                    }
                    s.Offset = s.nStream.EndRead(ir);
                    pushSockets.Invoke(s);//推送至UI
                    sk.nStream.BeginRead(sk.RecBuffer, 0, sk.RecBuffer.Length, new AsyncCallback(EndReader), sk);
                }
            }
            catch (Exception ex)
            {
                Sockets sks = s;
                sks.ex = ex;
                sks.ClientDispose = true;
                pushSockets.Invoke(sks);
            }
        }

        /// <summary>
        /// 连接服务端
        /// </summary>
        public override void Start()
        {
            Connect();
        }

        public override void Stop()
        {
            Sockets sks = new Sockets();
            if (client != null)
            {
                client.Client.Shutdown(SocketShutdown.Both);
                Thread.Sleep(10);
                client.Close();
                IsClose = true;
                client = null;
            }
            else
            {
                sks.ex = new Exception("客户端没有初始化");
            }
            pushSockets.Invoke(sks);
        }
        public void SendData(string sendData)
        {
            try
            {
                if (client == null || client.Connected == false)
                {
                    Sockets sks = new Sockets(new Exception("客户端没有连接到服务器，请先建立连接。"));
                    pushSockets.Invoke(sks);//推送至UI
                    return;
                }
                if (client.Connected)//正在连接状态
                {
                    if (nStream == null)
                    {
                        nStream = client.GetStream();
                    }
                    byte[] buffer = Encoding.UTF8.GetBytes(sendData);
                    nStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Sockets sks = new Sockets(ex);
                pushSockets.Invoke(sks);
            }
        }

    }
}
