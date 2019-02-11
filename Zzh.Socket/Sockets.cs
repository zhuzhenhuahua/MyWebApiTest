using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Zzh.Socket
{
   public class Sockets
    {
        /// <summary>
        /// 接收缓冲区大小8K
        /// </summary>
        public byte[] RecBuffer = new byte[8 * 1024];
        /// <summary>
        /// 发送缓冲区大小8K
        /// </summary>
        public byte[] SendBuffer = new byte[8 * 1024];
        /// <summary>
        /// 异步接收后包的大小
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 当前IP地址+端口号
        /// </summary>
        public IPEndPoint Ip { get; set; }
        /// <summary>
        /// 客户端主通信程序
        /// </summary>
        public TcpClient Client { get; set; }
        /// <summary>
        /// 承载客户端Socket的网络流
        /// </summary>
        public NetworkStream nStream { get; set; }
        /// <summary>
        /// 发生异常时不为null
        /// </summary>
        public Exception ex { get; set; }
        /// <summary>
        /// 客户端退出标识，如果服务端发现此标识为true，那么认为客户端下线
        /// 客户端接收此标识时，认为客户端异常
        /// </summary>
        public bool ClientDispose { get; set; }
        public Sockets(){ }
        public Sockets(Exception ex)
        {
            this.ex = ex;
            this.ClientDispose = true;
        }
        public Sockets(IPEndPoint ip,TcpClient tcpClient,NetworkStream ns)
        {
            this.Ip = ip;
            this.Client = tcpClient;
            this.nStream = ns;
        }
    }
}
