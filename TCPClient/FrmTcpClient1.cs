using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zzh.Socket;
using System.Threading;

namespace TCPClient
{
    public partial class FrmTcpClient1 : Form
    {
        ZzhTcpClient tcpClient;
        private string ip = string.Empty;
        private string port = string.Empty;
        public FrmTcpClient1()
        {
            InitializeComponent();
        }

        private void FrmTcpClient1_Load(object sender, EventArgs e)
        {
            ZzhTcpClient.pushSockets = new PushSockets(Rec);
            tcpClient = new ZzhTcpClient();
            this.ip = txtServerIP.Text.Trim();
            this.port = txtServerPort.Text.Trim();
        }
        private void Rec(Sockets sks)
        {
            this.Invoke(new ThreadStart(delegate
            {
                if (this.listBoxText.Items.Count > 100)
                {
                    this.listBoxText.Items.Clear();
                }
                if (sks.ex != null)
                {
                    if (sks.ClientDispose == true)
                    {
                        ////由于未知原因引发异常.导致客户端下线.   比如网络故障.或服务器断开连接.
                        this.listBoxStates.Items.Add("客户端报错下线");
                    }
                    this.listBoxStates.Items.Add($"异常消息{sks.ex}");
                }
                else if (sks.Offset == 0)
                {
                    this.listBoxStates.Items.Add("客户端主动下线");
                }
                else
                {
                    byte[] buffer = new byte[sks.Offset];
                    Array.Copy(sks.RecBuffer, buffer, sks.Offset);
                    string str = Encoding.UTF8.GetString(buffer);
                    this.listBoxText.Items.Add($"服务端{sks.Ip}发来消息：{str}");
                }
            }));
        }
        //连接服务器
        private void btnConnServer_Click(object sender, EventArgs e)
        {
            try
            {
                this.ip = txtServerIP.Text.Trim();
                this.port = txtServerPort.Text.Trim();
                tcpClient.InitSocket(this.ip, Convert.ToInt32(this.port));
                tcpClient.Start();
                listBoxStates.Items.Add("连接服务端成功！");
                btnConnServer.Enabled = false;
            }
            catch (Exception ex)
            {
                listBoxStates.Items.Add($"连接失败，原因：{ex.Message}");
                btnConnServer.Enabled = true;
            }
        }

        //断开连接
        private void btnDisConn_Click(object sender, EventArgs e)
        {
            tcpClient.Stop();
            this.btnConnServer.Enabled = true;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            tcpClient.SendData(this.txtSendData.Text.Trim());
        }
    }
}
