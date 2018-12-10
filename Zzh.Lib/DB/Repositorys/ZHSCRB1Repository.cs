using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;

namespace Zzh.Lib.DB.Repositorys
{
   public class ZHSCRB1Repository:BaseORCLRepository
    {
        public List<ZHSCRB1> GetList()
        {
            var list = context.ZHSCRB1S.ToList();
            return list;
        }
    }
}
