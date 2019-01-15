using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;

namespace Zzh.Backend.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetList(int rows, int page, string roleName)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_RoleRepository.GetListAsync(visiter,page, rows, roleName);
                return Json(new { total = result.Item1, rows = result.Item2 });
            }

        }
        public async Task<JsonResult> GetlistAll()
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_RoleRepository.GetListAsync(visiter);
                return Json(result);
            }
        }
        public async Task<ActionResult> EditRole(int rid)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var role = new Sys_Role();
                if (rid > 0)
                    role = await Sys_RoleRepository.GetRoleAsync(visiter, rid);
                return View(role);
            }
        }
        public async Task<JsonResult> SaveRole(Sys_Role role)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_RoleRepository.AddOrUpdateAsync(visiter, role);
                return Json(new { isOk = result });
            }
        }
        public async Task<JsonResult> DelRole(int rid)
        {
            using(RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var userTotal = await Sys_UserRepository.GetUserListCountByRoleID(visiter, rid);
                    if (userTotal > 0)
                        return Json(new { isOk = false, msg = "请先删除该角色下的用户" });
                    var result = await Sys_RoleRepository.DeleteRoleAsync(visiter, rid);
                    return Json(new { isOk = result });
                }
        }

        public ActionResult RoleMenuIndex(int rid)
        {
            ViewBag.rid = rid;
            return View();
        }

        public async Task<JsonResult> SaveRoleMenu(int roleId, List<int> checkedIdList)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                bool result = false;
                if (roleId > 0 && checkedIdList.Count > 0)
                {
                    result = await Sys_RoleMenuRepository.SaveRoleMenuAsync(visiter, roleId, checkedIdList);
                }
                return Json(new { isOK = result });
            }
        }
    }
}