using NLite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Model.DB
{
    [Table(Name = "Sys_User")]
    public class NLiteSys_User
    {
        [Id("Uid",DbType = DBType.Int32, IsDbGenerated =false,Length =8)]
        public int Uid { get; set; }
        [Column("Name",DbType = DBType.VarChar,Length =200)]
        public string Name { get; set; }
        [Column("LoginName", DbType = DBType.VarChar,Length =200)]
        public string LoginName { get; set; }
    }
}
