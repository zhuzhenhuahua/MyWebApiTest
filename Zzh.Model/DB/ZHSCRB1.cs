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
    [Table("ZHSCRB1")]
    public class ZHSCRB1
    {
        [Key]
        public int? DWDT_DWS_ZS { get; set; }
        public int? DWDT_DWS_ZH { get; set; }
        public int? DWDT_DWS_QC { get; set; }
    }
}
