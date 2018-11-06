using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Backend.Controllers.Filter;

namespace Zzh.Backend.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult myTaskType_Partial()
        {
            var session = Session["CurrentUser"] as CurrentUser;
            return PartialView();
        }
    }
}