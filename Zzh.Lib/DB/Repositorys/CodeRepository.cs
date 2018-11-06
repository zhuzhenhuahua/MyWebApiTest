using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;
using System.Collections.Concurrent;
using System.Data.Entity;

namespace Zzh.Lib.DB.Repositorys
{
    /// <summary>
    /// 统一字典表的封装类
    /// </summary>
    public class CodeRepository : BaseRepository
    {
        private static CodeRepository codeRepository;
        private static readonly object locker = new object();
        /// <summary>
        /// 此处private不允许new对象，防止使用using释放资源
        /// </summary>
        private CodeRepository()
        {

        }
        public static CodeRepository CreateInstance()
        {
            if (codeRepository == null)
            {
                lock (locker)
                {
                    if (codeRepository == null)
                        codeRepository = new CodeRepository();
                }
            }
            return codeRepository;
        }

        #region Codes
        static ConcurrentDictionary<int, List<Codes>> _dicCodes = new ConcurrentDictionary<int, List<Codes>>();
        public async Task<List<Codes>> GetCodesListAsync(ECodesTypeId typeId)
        {
            int typeid = Convert.ToInt32(typeId);
            if (!_dicCodes.ContainsKey(typeid))
            {
                var list = await (from j in context.Codes
                                  where j.TypeId == typeid && j.Status == 1
                                  select j).ToListAsync();
                _dicCodes[typeid] = list;
                return list;
            }
            return _dicCodes[typeid];

        }
        #endregion
    }
}
