using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{
    public class RuleUpdate
    {
        public string tracker { get; set; }
        public bool disabled { get; set; }
        public bool apply { get; set; }        
    }
}
