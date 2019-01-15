using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;
using System.Collections.Concurrent;
using Zzh.Lib.DB.Context;

namespace Zzh.Lib.DB.Repositorys
{
    public static class Sys_UserRepository
    {
        private static ConcurrentDictionary<int, Sys_User> dicUserList = new ConcurrentDictionary<int, Sys_User>();
        public static async Task<Tuple<int, List<Sys_User>>> GetListAsync(RepositoryVisiter visiter, int pageIndex, int pageSize, string userName)
        {
            int from = (pageIndex - 1) * pageSize;
            int total = await (from j in visiter.context.Sys_Users
                               where userName == "" ? 1 == 1 : j.Name.Contains(userName)
                               select j).CountAsync();
            var list = await (from j in visiter.context.Sys_Users
                              where userName == "" ? 1 == 1 : j.Name.Contains(userName)
                              orderby j.Uid descending
                              select j).Skip(from).Take(pageSize).ToListAsync();
                //这里后期需要优化
                foreach (var item in list)
                {
                    var role = await Sys_RoleRepository.GetRoleAsync(visiter,item.RoleId);
                    if (role != null)
                        item.RoleName = role.RName;
                }
            return Tuple.Create(total, list);
        }

        public static async Task<Sys_User> GetUserAsync(RepositoryVisiter visiter, int uid)
        {
            try
            {
                var user = await (from j in visiter.context.Sys_Users
                                  where j.Uid == uid
                                  select j).FirstOrDefaultAsync();
                if (user == null)
                    return new Sys_User();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<Sys_User> GetUserDicAsync(RepositoryVisiter visiter, int uid)
        {
            if (dicUserList.ContainsKey(uid))
                return dicUserList[uid];
            try
            {
                var user = await (from j in visiter.context.Sys_Users
                                  where j.Uid == uid
                                  select j).FirstOrDefaultAsync();
                if (user != null)
                {
                    dicUserList[uid] = user;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<Sys_User>> GetUserListAsync(RepositoryVisiter visiter)
        {
            var list = await (from j in visiter.context.Sys_Users
                              orderby j.Name
                              select j).ToListAsync();
            return list;
        }
        public static async Task<int> GetUserListCountByRoleID(RepositoryVisiter visiter, int roleID)
        {
            var total = await visiter.context.Sys_Users.Where(p => p.RoleId == roleID).CountAsync();
            return total;
        }
        public static async Task<Sys_User> GetUserAsync(RepositoryVisiter visiter, string loginName, string pwd)
        {
            var user = await (from j in visiter.context.Sys_Users
                              where j.LoginName == loginName && j.PassWord == pwd
                              select j).FirstOrDefaultAsync();
            return user;
        }
        #region 增删改
        public static async Task<bool> AddOrUpdateAsync(RepositoryVisiter visiter, Sys_User user)
        {
            try
            {
                var sysUser = await visiter.context.Sys_Users.Where(p => p.Uid == user.Uid).FirstOrDefaultAsync();
                bool isNew = false;

                if (sysUser == null)
                {
                    isNew = true;
                    sysUser = new Sys_User();
                }
                foreach (var p in sysUser.GetType().GetProperties())
                {
                    //更新属性
                    var v = user.GetType().GetProperty(p.Name).GetValue(user);
                    if (v != null)
                    {
                        //其他字段更新
                        p.SetValue(sysUser, v);
                    }
                }
                if (isNew)
                    visiter.context.Sys_Users.Add(sysUser);
                return await visiter.context.SaveChangesAsync() == 1;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> UpdateTheme(RepositoryVisiter visiter, int userID,string theme)
        {
            var model = await visiter.context.Sys_Users.Where(p => p.Uid == userID).FirstOrDefaultAsync();
            if (model != null)
            {
                model.Themes = theme;
                return await visiter.context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public static async Task<bool> DeleteUser(RepositoryVisiter visiter, int uid)
        {
            var user = await visiter.context.Sys_Users.Where(p => p.Uid == uid).FirstOrDefaultAsync();
            if (user != null)
            {
                visiter.context.Sys_Users.Remove(user);
                return await visiter.context.SaveChangesAsync() == 1;
            }
            return false;
        }
        #endregion
    }
}
