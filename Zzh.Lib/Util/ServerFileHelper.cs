using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileUpLoadClient
{
    public class ServerFileHelper
    {
        private readonly string api = "http://localhost:5247/api/files";

        /// <summary>
        /// 客户端向Webapi下载文件时的方法，webapi代码见Zzh.Backend.Controllers.Demo
        /// </summary>
        /// <param name="ServerFileName"></param>
        /// <param name="SaveFileName"></param>
        /// <returns></returns>
        public bool DownLoad(string ServerFileName, string SaveFileName)
        {
            Uri server = new Uri(String.Format("{0}?s={1}", api, ServerFileName));
            HttpClient httpClient = new HttpClient();

            string p = Path.GetDirectoryName(SaveFileName);

            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);

            HttpResponseMessage responseMessage = httpClient.GetAsync(server).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                using (FileStream fs = File.Create(SaveFileName))
                {
                    Stream streamFromService = responseMessage.Content.ReadAsStreamAsync().Result;
                    streamFromService.CopyTo(fs);
                    return true;
                }
            }
            else
                return false;
        }
    }
}
