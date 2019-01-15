using Zzh.Model.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Lib.DB.Context;

namespace Zzh.Lib.DB.Repositorys
{
    public static class CodeTypeRepository 
    {
        #region CodeType
        public static async Task<Tuple<int, List<CodeType>>> GetCodeTypeListAsync(RepositoryVisiter visiter, int page, int rows, string typeName)
        {
            int pageIndex = (page - 1) * rows;
            var total = await (from j in visiter.context.CodeTypes
                               where j.IsDelete == 0
                               && (string.IsNullOrEmpty(typeName) ? 1 == 1 : j.TypeName.Contains(typeName))
                               select j).CountAsync();
            var list = await (from j in visiter.context.CodeTypes
                              where j.IsDelete == 0
                              && (string.IsNullOrEmpty(typeName) ? 1 == 1 : j.TypeName.Contains(typeName))
                              orderby j.TypeId descending
                              select j).Skip(pageIndex).Take(rows).ToListAsync();
            return Tuple.Create(total, list);
        }
        public static async Task<CodeType> GetCodeTypeAsync(RepositoryVisiter visiter, int typeId)
        {
            var model = await visiter.context.CodeTypes.Where(p => p.TypeId == typeId).FirstOrDefaultAsync();
            return model;
        }
        public static async Task<bool> AddOrUpdateAsync(RepositoryVisiter visiter, CodeType modelPara)
        {
            var isAdd = false;
            var model = await visiter.context.CodeTypes.Where(p => p.TypeId == modelPara.TypeId).FirstOrDefaultAsync();
            if (model == null)
            {
                isAdd = true;
                model = new CodeType() { IsDelete = 0 };
            }
            model.TypeName = modelPara.TypeName;
            model.Remark = modelPara.Remark;
            if (isAdd)
                visiter.context.CodeTypes.Add(model);
            return await visiter.context.SaveChangesAsync() > 0;
        }
        public static async Task<bool> DelCodeTypeAsync(RepositoryVisiter visiter,int typeID)
        {
            var model = await visiter.context.CodeTypes.Where(p => p.TypeId == typeID).FirstOrDefaultAsync();
            if (model != null)
            {
                model.IsDelete = 1;
                return await visiter.context.SaveChangesAsync() > 0;
            }
            return false;
        }
        #endregion
    }
}
