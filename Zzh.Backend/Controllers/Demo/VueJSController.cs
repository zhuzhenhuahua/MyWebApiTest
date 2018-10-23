using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zzh.Model.DB;
using Zzh.Lib.DB.Repositorys;
using System.Threading.Tasks;

namespace Zzh.Backend.Controllers.Demo
{
    public class VueJSController : Controller
    {
        // GET: VueJS
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<JsonResult> GetUserList()
        {
            using (Sys_UserRepository repo = new Sys_UserRepository())
            {
                var result = await repo.GetListAsync(1, 100, "");
                var json= Json(new { total = result.Item1, rows = result.Item2 });
                return json;
            }
        }
    }
}