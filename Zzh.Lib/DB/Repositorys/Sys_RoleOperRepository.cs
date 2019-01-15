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
    public static class Sys_RoleOperRepository 
    {
        public static async Task<List<Sys_RoleOper>> GetListAsync(RepositoryVisiter visiter, int roleId)
        {
            return await visiter.DB.Sys_RoleOpers.Where(p => p.RoleId == roleId).ToListAsync();
        }
    }
}
