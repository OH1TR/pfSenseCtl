using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{
    public class NewFirewallRule
    {
        public string type { get; set; }
        public string @interface { get; set; }
        public string ipprotocol { get; set; }
        public string protocol { get; set; }
        public string src { get; set; }
        public string srcport { get; set; }
        public string dst { get; set; }
        public int dstport { get; set; }
        public string descr { get; set; }
        public bool top { get; set; }
        public bool apply { get; set; }
    }
}
