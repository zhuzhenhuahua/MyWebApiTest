﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zzh.Backend.Models;
using Zzh.Lib.DB.Context;
using Zzh.Lib.DB.Repositorys;
using Zzh.Model.DB;

namespace Zzh.Backend.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        //获取列表分页数据
        public async Task<JsonResult> GetList(int page, int rows, string menuName, int parentId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuRepository.GetListAsync(visiter, page, rows, menuName, parentId);
                return Json(new { total = result.Item1, rows = result.Item2 });
            }

        }
        public async Task<JsonResult> GetTreeList(string menuName, int parentId)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var treeResult = await Sys_MenuRepository.GetTreeListAsync(visiter, menuName, parentId);
                return Json(treeResult);
            }
        }
        public PartialViewResult GetMenuListPartialView()
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var currentUser = Session["CurrentUser"] as CurrentUser;
                var list = Sys_MenuRepository.GetMeunListByRoleID(visiter, currentUser.Sys_User.RoleId);//该角色下的菜单
                return PartialView("_PartialMenu", new MenuListPartial() { list = list });
            }
        }
        public class MenuListPartial
        {
            public List<Sys_Menu> list { get; set; }
        }
        //根据父ID获取子菜单列表
        public async Task<JsonResult> GetMeunListByParentID(int parentID)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuRepository.GetListByParentIdAsync(visiter, parentID);
                return Json(result);
            }
        }
        //获取所有父级菜单
        public async Task<JsonResult> GetParentMenuList(int isAddAll)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuRepository.GetListByParentIdAsync(visiter, 0);
                if (isAddAll == 1)
                    result.Insert(0, new Sys_Menu() { MenuId = 0, MenuName = "全部" });
                return Json(result);
            }
        }
        //获取所有二级菜单列表
        public async Task<JsonResult> GetAllChildMeunList(int isAddAll)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuRepository.GetAllChildMeunList(visiter);
                if (isAddAll == 1)
                    result.Insert(0, new Sys_Menu() { MenuId = 0, MenuName = "全部" });
                return Json(result);
            }
        }
        #region 增删改操作
        public async Task<ActionResult> EditMenu(int mid)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var menu = new Sys_Menu();
                if (mid > 0)
                    menu = await Sys_MenuRepository.GetMenuAsync(visiter,mid);
                var list = await Sys_MenuRepository.GetListByParentIdAsync(visiter,0);
                var parentMenuList = new List<SelectListItem>()
                {
                     new SelectListItem() {   Value="0", Text="无", Selected=true}
                };
                var selectList = new SelectList(list, "MenuId", "MenuName");
                parentMenuList.AddRange(selectList);
                ViewBag.parentMenuList = parentMenuList;
                return View(menu);
            }
        }
        public async Task<JsonResult> SaveMenu(Sys_Menu menu)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuRepository.AddOrUpdateAsync(visiter,menu);
                return Json(new { isOk = result });
            }
        }
        public async Task<JsonResult> DelMenu(int mid)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var result = await Sys_MenuRepository.DeleteMenuAsync(visiter,mid);
                return Json(new { isOk = result });
            }
        }
        #endregion
        public async Task<JsonResult> GetMenuListTree(int rid)
        {
            using (RepositoryVisiter visiter = new RepositoryVisiter())
            {
                var menuList = await Sys_MenuRepository.GetListAsync(visiter);//所有菜单
                var roleMenu = await Sys_RoleMenuRepository.GetListAsync(visiter, rid);//该角色已经有的菜单ID
                List<int> menuIds = roleMenu.Select(p => p.MenuId).ToList();
                var meunOperList = await Sys_MenuOperRepository.GetListAsync(visiter);//所有菜单操作按钮
                var roleMenuOper = await Sys_RoleOperRepository.GetListAsync(visiter, rid);//该角色已经有的菜单操作按钮ID
                List<int> menuOperIds = roleMenuOper.Select(p => p.MenuOperId).ToList();
                var result = ConvertMenuToEasyUiTree(menuList, menuIds, meunOperList, menuOperIds);
                return Json(result);
            }
        }

        /// <summary>
        /// 将menuList转换为EasyUiTreeViewModel
        /// </summary>
        /// <param name="menuList">所有的菜单</param>
        /// <param name="roleMenu">角色所拥有的MenuID的列表</param>
        /// <param name="menuOperList">所有的菜单操作按钮</param>
        /// <param name="roleMenuOperIds">角色所拥有的MenuOperID列表</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<EasyUiTreeViewModel> ConvertMenuToEasyUiTree(List<Sys_Menu> menuList, List<int> roleMenu, List<Sys_MenuOper> menuOperList, List<int> roleMenuOperIds, int parentId = 0)
        {
            List<EasyUiTreeViewModel> treeModeList = new List<Models.EasyUiTreeViewModel>();
            treeModeList.AddRange(menuList.Where(p => p.ParentId == parentId).OrderBy(p => p.MenuSortID).Select(m => new EasyUiTreeViewModel()
            {
                id = m.MenuId,
                text = m.MenuName,
                @checked = roleMenu.Contains(m.MenuId),
                children = ConvertMenuToEasyUiTree(menuList, roleMenu, menuOperList, roleMenuOperIds, m.MenuId)
            }));
            treeModeList.SelectMany(p => p.children).ToList().ForEach(m => m.children.AddRange(menuOperList.Where(p => p.MenuId == m.id).Select(v => new EasyUiTreeViewModel()
            {
                id = v.MenuOperId,
                text = v.MenuOperBtnName,
                @checked = roleMenuOperIds.Contains(v.MenuOperId),
                children = new List<EasyUiTreeViewModel>()
            })));
            return treeModeList;
        }
    }
}