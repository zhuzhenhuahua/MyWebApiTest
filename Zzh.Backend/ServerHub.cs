using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Zzh.Backend
{
    public class ServerHub : Hub
    {
        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        public void Send(SignalRMessage sendMessage)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ServerHub>();
            context.Clients.All.send(sendMessage);
        }
        /// <summary>
        /// 接收客户端发送的消息
        /// </summary>
        /// <param name="receiveMessage"></param>
        public void Receive(SignalRMessage receiveMessage)
        {
            var message = receiveMessage;
        }
    }
    /// <summary>
    /// 返回的数据类型
    /// </summary>
    public class SignalRMessage
    {
        public string clientIp { get; set; }
        public string head { get; set; }
        public string body { get; set; }
        public SignalRMessage(string clientIp, string _head, string _body)
        {
            this.clientIp = clientIp;
            this.head = _head;
            this.body = _body;
        }
    }
}