using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;

namespace Zzh.Backend.Controllers.Demo
{
    public class DevXtraReportController : Controller
    {
        // GET: DevXtraReport
        public async Task<ActionResult> Index()
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                string filePath = Server.MapPath(string.Format("~/{0}", "Files"));
                var result = await Sys_UserRepository.GetListAsync(visiter, 1, 10, "");
                return View();
            }
        }
    }
}