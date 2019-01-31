using NLite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Model.DB
{
    [Serializable]
    [Table( Name = "areas")]
   public class Sys_Areas
    {
        [Id(Name = "id",DbType = DBType.Int32,IsDbGenerated =false)]
        public int id { get; set; }
        [Column(Name = "areaname",DbType = DBType.VarChar,Length =200)]
        public string areaname { get; set; }
        [Column(Name = "parentid", DbType = DBType.Int32, Length = 200)]
        public int parentid { get; set; }
        [Column(Name = "shortname", DbType = DBType.VarChar, Length = 200)]
        public string shortname { get; set; }
        [Column(Name = "LEVEL", DbType = DBType.Int32, Length = 200)]
        public int LEVEL { get; set; }
        [Column(Name = "sort", DbType = DBType.Int32, Length = 200)]
        public int sort { get; set; }
    }
}
