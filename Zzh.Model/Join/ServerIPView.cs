using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB; 

namespace Zzh.Model.Join
{
    [Serializable]
   public class ServerIPView
    {
        public int sid { get; set; }
        public IpAddress IPAddress { get; set; }
    }
}
