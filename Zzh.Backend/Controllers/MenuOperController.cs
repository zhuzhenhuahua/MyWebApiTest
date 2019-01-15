using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;

namespace Zzh.Backend.Controllers
{
    public class MenuOperController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetListPager(int page, int rows, int menuId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var tuple = await Sys_MenuOperRepository.GetListPager(visiter, page, rows, menuId);
                return Json(new { total = tuple.Item1, rows = tuple.Item2 });
            }
        }
        #region 增删改
        public async Task<ActionResult> EditMeunOper(int menuOperId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                Sys_MenuOper model = new Sys_MenuOper();
                if (menuOperId > 0)
                {
                    model = await Sys_MenuOperRepository.GetModelAsync(visiter,menuOperId);
                    var menu = await Sys_MenuRepository.GetMenuAsync(visiter,model.MenuId);//根据menuID找到对应的菜单
                    model.MenuParentID = menu.ParentId;
                }


                return View(model);
            }
        }
        public async Task<JsonResult> Save(Sys_MenuOper model)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                model.CreateTime = DateTime.Now;
                var result = await Sys_MenuOperRepository.AddOrUpdateAsync(visiter,model);
                return Json(new { isOk = result });
            }
        }
        public async Task<JsonResult> DelMenuOper(int menuOperID)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuOperRepository.DelMenuOper(visiter,menuOperID);
                return Json(new { isOk = result });
            }
        }
        #endregion
    }
}