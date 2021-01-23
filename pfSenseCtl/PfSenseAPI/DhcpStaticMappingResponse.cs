using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{


    public class DhcpStaticMappingResponse
    {
        public class Datum
        {
            public int id { get; set; }
            public string mac { get; set; }
            public string cid { get; set; }
            public string ipaddr { get; set; }
            public string hostname { get; set; }
            public string descr { get; set; }
            public string filename { get; set; }
            public string rootpath { get; set; }
            public string defaultleasetime { get; set; }
            public string maxleasetime { get; set; }
            public string gateway { get; set; }
            public string domain { get; set; }
            public string domainsearchlist { get; set; }
            public string ddnsdomain { get; set; }
            public string ddnsdomainprimary { get; set; }
            public string ddnsdomainkeyname { get; set; }
            public string ddnsdomainkey { get; set; }
            public string tftp { get; set; }
            public string ldap { get; set; }
        }

        public string status { get; set; }
        public int code { get; set; }
        public int @return { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }

}
