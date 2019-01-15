using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Lib.DB.Context;
using Zzh.Model.DB;

namespace Zzh.Lib.DB.Repositorys
{
    public static class Sys_MenuRepository 
    {
        public static async Task<List<Sys_Menu>> GetListAsync(RepositoryVisiter visiter)
        {
            var list = await (from j in visiter.context.Sys_Menus
                              orderby  j.MenuSortID,j.MenuId
                              select j).ToListAsync();
            return list;
        }
        public static List<Sys_Menu> GetList(RepositoryVisiter visiter)
        {
            var list = (from j in visiter.context.Sys_Menus
                        orderby j.MenuSortID, j.MenuId
                        select j).ToList();
            return list;
        }
        public static List<Sys_Menu> GetMeunListByRoleID(RepositoryVisiter visiter, int roleID)
        {
            var list = (from j in visiter.context.Sys_Menus
                        join roleMenu in visiter.context.Sys_RoleMenus on j.MenuId equals roleMenu.MenuId
                        where roleMenu.RoleId == roleID
                        orderby j.MenuSortID
                        select j).ToList();
            return list;
        }
        public static async Task<List<Sys_Menu>> GetTreeListAsync(RepositoryVisiter visiter, string menuName, int parentId)
        {
            var list = await visiter.context.Sys_Menus.OrderBy(p=>p.MenuSortID).ToListAsync();
            List<Sys_Menu> treeList = new List<Sys_Menu>();
            treeList = list.Where(p => p.ParentId == parentId).ToList();
            foreach (var item in treeList)
            {
                item.children = list.Where(p => p.ParentId == item.MenuId).ToList();
            }
            return treeList;
        }
        public static async Task<Tuple<int, List<Sys_Menu>>> GetListAsync(RepositoryVisiter visiter, int page, int rows, string menuName, int parentId)
        {
            int from = (page - 1) * rows;
            var total = await (from j in visiter.context.Sys_Menus
                               where (j.MenuName.Contains(menuName)) && (parentId > 0 ? j.ParentId == parentId : 1 == 1)
                               select j).CountAsync();
            var list = await (from j in visiter.context.Sys_Menus
                              where (j.MenuName.Contains(menuName)) && (parentId > 0 ? j.ParentId == parentId : 1 == 1)
                              orderby j.MenuId, j.MenuSortID
                              select j).Skip(from).Take(rows).ToListAsync();
            return Tuple.Create(total, list);
        }
        public static async Task<Sys_Menu> GetMenuAsync(RepositoryVisiter visiter, int menuId)
        {
            return await visiter.context.Sys_Menus.Where(p => p.MenuId == menuId).FirstOrDefaultAsync();
        }
        public static async Task<bool> AddOrUpdateAsync(RepositoryVisiter visiter, Sys_Menu menu)
        {
            try
            {
                var sysMenu = await GetMenuAsync(visiter,menu.MenuId);
                bool isNew = false;

                if (sysMenu == null)
                {
                    isNew = true;
                    sysMenu = new Sys_Menu();
                }
                foreach (var p in sysMenu.GetType().GetProperties())
                {
                    //更新属性
                    var v = menu.GetType().GetProperty(p.Name).GetValue(menu);
                    if (v != null)
                    {
                        //其他字段更新
                        p.SetValue(sysMenu, v);
                    }
                }
                if (isNew)
                    visiter.context.Sys_Menus.Add(sysMenu);
                return await visiter.context.SaveChangesAsync() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<bool> DeleteMenuAsync(RepositoryVisiter visiter, int menuId)
        {
            var menu = await visiter.context.Sys_Menus.Where(p => p.MenuId == menuId).FirstOrDefaultAsync();
            if (menu != null)
            {
                visiter.context.Sys_Menus.Remove(menu);
                return await visiter.context.SaveChangesAsync() == 1;
            }
            return false;
        }
        public static async Task<List<Sys_Menu>> GetListByParentIdAsync(RepositoryVisiter visiter, int parentId)
        {
            return await visiter.context.Sys_Menus.Where(p => p.ParentId == parentId).OrderBy(p => p.MenuSortID).ToListAsync();
        }
        public static async Task<List<Sys_Menu>> GetAllChildMeunList(RepositoryVisiter visiter)
        {
            return await visiter.context.Sys_Menus.Where(p => p.ParentId > 0).ToListAsync();
        }
    }
}
