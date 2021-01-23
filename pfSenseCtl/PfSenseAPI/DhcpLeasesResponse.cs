using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{
    public class DhcpLeasesResponse
    {
        public class Datum
        {
            public string state { get; set; }
            public string ip { get; set; }
            public string mac { get; set; }
            public string starts { get; set; }
            public string ends { get; set; }
            public string hostname { get; set; }
            public bool online { get; set; }
        }

        public string status { get; set; }
        public int code { get; set; }
        public int @return { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }
}
