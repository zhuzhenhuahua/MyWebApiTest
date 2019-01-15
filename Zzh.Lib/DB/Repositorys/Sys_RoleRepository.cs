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
    public static class Sys_RoleRepository 
    {
        public static async Task<List<Sys_Role>> GetListAsync(RepositoryVisiter visiter)
        {
            var list = await (from j in visiter.DB.Sys_Roles
                              orderby j.Rid descending
                              select j).ToListAsync();
            return list;
        }
        public static async Task<Tuple<int, List<Sys_Role>>> GetListAsync(RepositoryVisiter visiter, int page, int rows, string roleName)
        {
            int from = (page - 1) * rows;
            var total = await (from j in visiter.DB.Sys_Roles
                               where j.RName.Contains(roleName)
                               select j).CountAsync();
            var list = await (from j in visiter.DB.Sys_Roles
                              where j.RName.Contains(roleName)
                              orderby j.Rid descending
                              select j).Skip(from).Take(rows).ToListAsync();
            return Tuple.Create(total, list);
        }
        public static async Task<Sys_Role> GetRoleAsync(RepositoryVisiter visiter, int rid)
        {
            return await visiter.DB.Sys_Roles.Where(p => p.Rid == rid).FirstOrDefaultAsync();
        }
        public static async Task<bool> AddOrUpdateAsync(RepositoryVisiter visiter, Sys_Role role)
        {
            try
            {
                var sysRole = await GetRoleAsync(visiter,role.Rid);
                bool isNew = false;

                if (sysRole == null)
                {
                    isNew = true;
                    sysRole = new Sys_Role();
                }
                foreach (var p in sysRole.GetType().GetProperties())
                {
                    //更新属性
                    var v = role.GetType().GetProperty(p.Name).GetValue(role);
                    if (v != null)
                    {
                        //其他字段更新
                        p.SetValue(sysRole, v);
                    }
                }
                if (isNew)
                    visiter.DB.Sys_Roles.Add(sysRole);
                return await visiter.DB.SaveChangesAsync() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static async Task<bool> DeleteRoleAsync(RepositoryVisiter visiter, int rid)
        {
            var role = await visiter.DB.Sys_Roles.Where(p => p.Rid == rid).FirstOrDefaultAsync();
            if (role != null)
            {
                var roleMenus = await visiter.DB.Sys_RoleMenus.Where(p => p.RoleId == rid).ToListAsync();
                if(roleMenus.Count>0)
                {
                    visiter.DB.Sys_RoleMenus.RemoveRange(roleMenus);//删除角色时，先删除角色下所有的菜单
                }
                visiter.DB.Sys_Roles.Remove(role);
                return await visiter.DB.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
