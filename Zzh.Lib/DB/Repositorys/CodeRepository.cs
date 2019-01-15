using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;
using System.Collections.Concurrent;
using System.Data.Entity;
using Zzh.Lib.DB.Context;

namespace Zzh.Lib.DB.Repositorys
{
    /// <summary>
    /// 统一字典表的封装类
    /// </summary>
    public static class CodeRepository 
    {
        #region Codes
        static ConcurrentDictionary<int, List<Codes>> _dicCodes = new ConcurrentDictionary<int, List<Codes>>();
        public static async Task<List<Codes>> GetCodesListAsync(RepositoryVisiter visiter, ECodesTypeId typeId)
        {
            int typeid = Convert.ToInt32(typeId);
            if (_dicCodes.ContainsKey(typeid))
            {
                return _dicCodes[typeid];
            }
            var list = await (from j in visiter.context.Codes
                              where j.TypeId == typeid && j.Status == 1
                              select j).ToListAsync();
            if (list.Count > 0)
            {
                _dicCodes[typeid] = list;
            }
            return list;
        }
        public static async Task<List<Codes>> GetCodesListAsync(RepositoryVisiter visiter, int typeId)
        {
            var list = await (from j in visiter.context.Codes
                              where j.TypeId == typeId
                              select j).ToListAsync();
            return list;
        }
        public static async Task<string> GetCodesTextAsync(RepositoryVisiter visiter, ECodesTypeId typeid, string code)
        {
            var list = await GetCodesListAsync(visiter,typeid);
            if (list == null || list.Count == 0)
                return string.Empty;
            var codeModel = list.Where(p => p.Code == code).FirstOrDefault();
            if (codeModel != null)
            {
                return codeModel.Text;
            }
            int typID = Convert.ToInt32(typeid);
            var codeList = await (from j in visiter.context.Codes
                                  where j.TypeId == typID
                                  select j).ToListAsync();
            if (codeList.Count>0)
            {
                _dicCodes[typID] = codeList;
                return codeList.Where(p => p.Code == code).FirstOrDefault()?.Text;
            }
            return string.Empty;
        }
        public static async Task<Codes> GetCodeAsync(RepositoryVisiter visiter, int codeId)
        {
            var model = await visiter.context.Codes.Where(p => p.Id == codeId).FirstOrDefaultAsync();
            return model;
        }
        public static async Task<bool> AddOrUpdateCode(RepositoryVisiter visiter, Codes codePara)
        {
            var isAdd = false;
            var model = await visiter.context.Codes.Where(p => p.Id == codePara.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                isAdd = true;
                model = new Codes() { TypeId = codePara.TypeId };
            }
            model.Code = codePara.Code;
            model.Text = codePara.Text;
            model.Status = codePara.Status;
            if (isAdd)
                visiter.context.Codes.Add(model);
            int res = await visiter.context.SaveChangesAsync();
            if (res > 0)
            {
                var listCode = new List<Codes>();
                _dicCodes.TryRemove(model.TypeId, out listCode);
            }
            return res>0;
        }
        #endregion
    }
}
