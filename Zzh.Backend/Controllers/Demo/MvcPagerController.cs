using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;

namespace Zzh.Backend.Controllers.Demo
{
    public class MvcPagerController : Controller
    {
        public static List<Sys_User> listSource = new List<Sys_User>();
        public MvcPagerController()
        {
            using (BaseRepository context=new BaseRepository ())
            {
                var list = context.context.Sys_Users.ToList();
                var model = new Sys_User() { Uid = 1, Name = "张", LoginName = "James" };
               int i= context.Insert<Sys_User>(model);
            }
            if (listSource.Count == 0)
            {
                for (int i = 0; i < 1000; i++)
                {
                    listSource.Add(new Sys_User() { Uid = i, Name = "张" + i, LoginName = "James" + i });
                }
            }
        }
        // GET: MvcPager
        public ActionResult Index(int pageIndex = 1)
        {
            var modelList = listSource.ToPagedList(pageIndex, 5);
            return View(modelList);
        }

        public ActionResult PartialIndex(int pageIndex = 1, string title = "", string id = "")
        {
            var pageList = listSource.Where(p => p.Name.Contains(title)).ToPagedList(pageIndex, 5);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_partialList", pageList);
            }
            return View(pageList);
        }
    }
}