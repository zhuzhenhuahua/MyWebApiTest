using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zzh.Utility;
using System.Threading;
using Zzh.Tester.Properties;
using System.Globalization;
using System.Diagnostics;

namespace Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.label1.Text = Thread.CurrentThread.CurrentCulture.Name;
            this.label2.Text = Resources.testName;
            //设置区域
            Thread.CurrentThread.CurrentUICulture =  new CultureInfo("en-US");
            this.label3.Text = Resources.testName;
            this.label4.Text = Resources.names;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
