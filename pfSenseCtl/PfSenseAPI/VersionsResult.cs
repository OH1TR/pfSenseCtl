using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{


    public class VersionsResult
    {
        public class Data
        {
            public string version { get; set; }
            public string patch { get; set; }
            public string buildtime { get; set; }
            public string lastcommit { get; set; }
            public double program { get; set; }
        }

        public string status { get; set; }
        public int code { get; set; }
        public int @return { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
