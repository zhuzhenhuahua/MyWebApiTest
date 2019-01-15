using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zzh.Lib.DB.Repositorys;
using Zzh.Backend.Controllers.Filter;
using Zzh.Utility;
using Zzh.Lib.DB.Context;

namespace Zzh.Backend.Controllers
{
    //该类作为登录验证，暂不继承BaseController，如果后期需要继承，则在filter中判断
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            Session.Clear();
            return View(model);
        }
        [LogRecord("系统登录")]
        public async Task<ActionResult> LoginSubmit(LoginViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.LoginName))
            {
                ViewData["errorMsg"] = "用户名不能为空";
                return View("Login", viewModel);
            }
            if (string.IsNullOrEmpty(viewModel.Password))
            {
                ViewData["errorMsg"] = "密码不能为空";
                return View("Login", viewModel);
            }
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var userModel = await Sys_UserRepository.GetUserAsync(visiter,viewModel.LoginName, viewModel.Password);
                if (userModel == null)
                {
                    ViewData["errorMsg"] = "账号或密码输入错误";
                    return View("Login", viewModel);
                }
                CurrentUser currentUser = new CurrentUser();
                currentUser.Sys_User = userModel;
                //currentUser.Sys_RoleMenu = await Sys_RoleMenuRepository.GetListAsync(userModel.RoleId);
                //currentUser.Sys_RoleOper = await Sys_RoleOperRepository.GetListAsync(userModel.RoleId);
                Session["CurrentUser"] = currentUser;
                Session.Timeout = 180;//登录过期时间（分钟）
                return Redirect("/Home/Index");
            }

        }
        public JsonResult GetSessionUser()
        {
            var user = Session["CurrentUser"] as CurrentUser;
            if (user != null)
            {
                user.Sys_User.Themes = EasyuiThemesHelper.GetValue(user.Sys_User.Uid);
            }
            return Json(user.Sys_User);
        }
        public JsonResult GetSessionUserExists()
        {
            var json = Json(new { isExists= Session["CurrentUser"] != null });
            return json;
        }
        public class LoginViewModel
        {
            public string LoginName { get; set; }
            public string Password { get; set; }
        }
    }
}