using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Model.DB
{
    [Serializable]
    [Table("dm_drillBrand")]
   public class dm_drillBrand
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
