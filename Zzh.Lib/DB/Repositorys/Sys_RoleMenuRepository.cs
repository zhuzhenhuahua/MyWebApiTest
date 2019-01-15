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
    public static class Sys_RoleMenuRepository
    {
        public static async Task<bool> SaveRoleMenuAsync(RepositoryVisiter visiter, int rid, List<int> menuListId)
        {
            try
            {
                var roleMenus = await visiter.DB.Sys_RoleMenus.Where(p => p.RoleId == rid).ToListAsync();
                visiter.DB.Sys_RoleMenus.RemoveRange(roleMenus);//先全部删除该角色下所有的菜单权限
                var roleMenuOper = await visiter.DB.Sys_RoleOpers.Where(p => p.RoleId == rid).ToListAsync();
                visiter.DB.Sys_RoleOpers.RemoveRange(roleMenuOper);//先全部删除该角色下所有的操作权限
                var menuOperList = await Sys_MenuOperRepository.GetListAsync(visiter);//所有的实体，用于获取menuID
                //await context.SaveChangesAsync();
                foreach (int menuid in menuListId)
                {
                    if (menuid.ToString().Length == 7)
                    {
                        //插入Sys_RoleOper表
                        var newRoleOper = new Sys_RoleOper();
                        newRoleOper.RoleId = rid;
                        newRoleOper.MenuId = menuOperList.Where(p => p.MenuOperId == menuid).FirstOrDefault().MenuId;
                        newRoleOper.MenuOperId = menuid;
                        visiter.DB.Sys_RoleOpers.Add(newRoleOper);
                    }
                    else
                    {
                        //插入Sys_RoleMenu表
                        var newRoleMenu = new Sys_RoleMenu();
                        newRoleMenu.RoleId = rid;
                        newRoleMenu.MenuId = menuid;
                        visiter.DB.Sys_RoleMenus.Add(newRoleMenu);
                    }
                }
                int s = await visiter.DB.SaveChangesAsync();
                return s > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Sys_RoleMenu>> GetListAsync(RepositoryVisiter visiter, int rid)
        {
            return await visiter.DB.Sys_RoleMenus.Where(p => p.RoleId == rid).ToListAsync();
        }
    }
}
