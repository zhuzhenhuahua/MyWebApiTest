using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Backend.Models;
using Zzh.Lib.Util;

namespace Zzh.Backend.Controllers.Demo
{
    public class OperFileController : Controller
    {
        // GET: OperFile
        public ActionResult UpDownload()
        {
            string filePath = Server.MapPath(string.Format("~/{0}", "Files"));

            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            if (!directoryInfo.Exists)
            {
                Directory.CreateDirectory(filePath);
            }
            List<JTFileModel> fileModels = new List<JTFileModel>();
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                fileModels.Add(new JTFileModel()
                {
                    Name = file.Name,
                    Extensioin = file.Extension,
                    FullName = file.FullName,
                    Length = file.Length,
                    IsReadOnly = file.IsReadOnly,
                    CreationTime = file.CreationTime
                });
            }
            return View(new FileModesObj() { List = fileModels });
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase filePara)
        {
            string filePath = Server.MapPath(string.Format("~/{0}", "Files"));
            if (filePara != null && filePara.ContentLength > 0)
            {
                var fileName = filePara.FileName;
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                filePara.SaveAs(Path.Combine(filePath, fileName));
                ViewBag.msg = "上传成功";
            }

            List<JTFileModel> fileModels = new List<JTFileModel>();
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                fileModels.Add(new JTFileModel()
                {
                    Name = file.Name,
                    Extensioin = file.Extension,
                    FullName = file.FullName,
                    Length = file.Length,
                    IsReadOnly = file.IsReadOnly,
                    CreationTime = file.CreationTime
                });
            }
            return View("UpDownload", new FileModesObj() { List = fileModels });
        }
        public class FileModesObj
        {
            public List<JTFileModel> List { get; set; }
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
        }
        private List<JTFileModel> GetDirectoryFiles()
        {
            string filePath = Server.MapPath(string.Format("~/{0}", "Files"));
            List<JTFileModel> fileModels = new List<JTFileModel>();
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {

            }
            return null;
        }


        /// <summary>
        /// 提供下载接口的api
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public HttpResponseMessage DownLoader(string fileNamePara)
        {
            try
            {
                if (string.IsNullOrEmpty(fileNamePara))
                    return JsonResponseHelper.HttpRMtoJson("参数不能为空", HttpStatusCode.OK, ECustomStatus.Fail);
                HttpResponseMessage result = null;

                string fileName = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\license.lic";
                System.IO.File.WriteAllText(fileName, "需要写入的字符串");
                //读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open);
                result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new StreamContent(fs);
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(fileName);
                return result;
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.HttpRMtoJson(ex.Message, HttpStatusCode.OK, ECustomStatus.Fail);
            }

        }
    }
}