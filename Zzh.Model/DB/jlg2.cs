using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Model.DB
{
    [Serializable]
    [Table("jlg2")]
   public class jlg2
    {
        [Key]
        public int id { get; set; }
        public DateTime rq { get; set; }
        public decimal priceNum1 { get; set; }
        public decimal priceNum2 { get; set; }
    }
}
