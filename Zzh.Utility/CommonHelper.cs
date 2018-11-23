using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Utility
{
   public static class CommonHelper
    {
        public static string GetRandomString(string startWith)
        {
            Random r = new Random();
            return startWith + DateTime.Now.ToString("yyyyMMddHHmmss")+ r.Next(1000, 9999).ToString();
        }
        #region 本机信息
        public static string GetHostName()
        {
            return Environment.MachineName;
        }

        public static string GetLoggerName(Type t)
        {
            return t.ToString();
        }

        public static string GetThreadId()
        {
            return System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
        }

        public static string GetOSName()
        {
            return Environment.OSVersion.ToString();
        }

        public static string GetDomain()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }
        /// <summary>
        /// 设置本地电脑的年月日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static void SetLocalDate(int year, int month, int day)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c date {0}-{1}-{2}", year, month, day);
            //关闭Shell的使用 
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口 
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();
            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的时分秒
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        public static void SetLocalTime(int hour, int min, int sec)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c time {0}:{1}:{2}", hour, min, sec);
            //关闭Shell的使用 
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口 
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();
            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }
        #endregion
    }
}
