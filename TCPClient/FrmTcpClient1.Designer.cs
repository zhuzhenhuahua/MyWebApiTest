namespace TCPClient
{
    partial class FrmTcpClient1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnServer = new System.Windows.Forms.Button();
            this.btnDisConn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxText = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBoxStates = new System.Windows.Forms.ListBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtServerPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtServerIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器IP：";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(79, 24);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(167, 21);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "127.0.0.1";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(79, 51);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(167, 21);
            this.txtServerPort.TabIndex = 4;
            this.txtServerPort.Text = "1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "服务器Port：";
            // 
            // btnConnServer
            // 
            this.btnConnServer.Location = new System.Drawing.Point(327, 28);
            this.btnConnServer.Name = "btnConnServer";
            this.btnConnServer.Size = new System.Drawing.Size(108, 23);
            this.btnConnServer.TabIndex = 1;
            this.btnConnServer.Text = "连接服务器";
            this.btnConnServer.UseVisualStyleBackColor = true;
            this.btnConnServer.Click += new System.EventHandler(this.btnConnServer_Click);
            // 
            // btnDisConn
            // 
            this.btnDisConn.Location = new System.Drawing.Point(327, 66);
            this.btnDisConn.Name = "btnDisConn";
            this.btnDisConn.Size = new System.Drawing.Size(108, 23);
            this.btnDisConn.TabIndex = 2;
            this.btnDisConn.Text = "断开链接";
            this.btnDisConn.UseVisualStyleBackColor = true;
            this.btnDisConn.Click += new System.EventHandler(this.btnDisConn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBoxText);
            this.groupBox4.Location = new System.Drawing.Point(20, 338);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(551, 241);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "客户端文字信息";
            // 
            // listBoxText
            // 
            this.listBoxText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxText.FormattingEnabled = true;
            this.listBoxText.ItemHeight = 12;
            this.listBoxText.Location = new System.Drawing.Point(3, 17);
            this.listBoxText.Name = "listBoxText";
            this.listBoxText.Size = new System.Drawing.Size(545, 221);
            this.listBoxText.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBoxStates);
            this.groupBox3.Location = new System.Drawing.Point(20, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(543, 220);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "客户端状态信息";
            // 
            // listBoxStates
            // 
            this.listBoxStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxStates.FormattingEnabled = true;
            this.listBoxStates.ItemHeight = 12;
            this.listBoxStates.Location = new System.Drawing.Point(3, 17);
            this.listBoxStates.Name = "listBoxStates";
            this.listBoxStates.Size = new System.Drawing.Size(537, 200);
            this.listBoxStates.TabIndex = 2;
            // 
            // txtSendData
            // 
            this.txtSendData.Location = new System.Drawing.Point(54, 611);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(392, 21);
            this.txtSendData.TabIndex = 7;
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(452, 609);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(108, 23);
            this.btnSendData.TabIndex = 8;
            this.btnSendData.Text = "发送";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // FrmTcpClient1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 690);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.txtSendData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnDisConn);
            this.Controls.Add(this.btnConnServer);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmTcpClient1";
            this.Text = "连接客户端";
            this.Load += new System.EventHandler(this.FrmTcpClient1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnServer;
        private System.Windows.Forms.Button btnDisConn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listBoxText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxStates;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Button btnSendData;
    }
}