using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Backend.Controllers.Filter;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;

namespace Zzh.Backend.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetList(int page, int rows, string userName)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_UserRepository.GetListAsync(visiter, page, rows, userName);
                return Json(new { total = result.Item1, rows = result.Item2 });
            }
        }
        public async Task<JsonResult> GetAllUsers(int isAddAll)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_UserRepository.GetUserListAsync(visiter);
                if (isAddAll == 1)
                    result.Insert(0, new Sys_User() { Uid = 0, Name = "全部" });
                return Json(result);
            }
        }
        public async Task<JsonResult> GetListTest()
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_UserRepository.GetListAsync(visiter,1, 10, "");
                var json = Json(result.Item2);
                return json;
            }
        }
        #region 增删改
        public async Task<ActionResult> EditUser(int uid)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                Sys_User user = await Sys_UserRepository.GetUserAsync(visiter, uid);
                ViewBag.RoleList = await Sys_RoleRepository.GetListAsync(visiter);
                return View(user);
            }

        }
        public async Task<JsonResult> SaveUser(Sys_User user)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_UserRepository.AddOrUpdateAsync(visiter,user);
                return Json(new { isOk = result });
            }
        }
        public JsonResult BatchSave(List<Sys_User> userList)
        {
            var result = true;
            return Json(new { isOk = result });
        }
        public async Task<JsonResult> DelUser(int uid)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_UserRepository.DeleteUser(visiter,uid);
                return Json(new { isOk = result });
            }
        }
        #endregion
    }
}