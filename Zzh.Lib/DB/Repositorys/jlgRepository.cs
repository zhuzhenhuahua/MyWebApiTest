using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;

namespace Zzh.Lib.DB.Repositorys
{
   public class jlgRepository: BaseRepository
    {
        public jlgRepository():base("LocalHostConnection")
        {

        }
        public async Task<List<jlg2>> GetListAsync()
        {
            var list = await (from j in context.jlg2s select j).ToListAsync();
            return list;
        }
    }
}
