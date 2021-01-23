using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{


    public class FirewallRulesResponse
    {
        public class Source
        {
            public string any { get; set; }
            public string network { get; set; }
        }

        public class Destination
        {
            public string address { get; set; }
            public string port { get; set; }
            public string any { get; set; }
        }

        public class Created
        {
            public string time { get; set; }
            public string username { get; set; }
        }

        public class Datum
        {
            public Source source { get; set; }
            public string @interface { get; set; }
            public string protocol { get; set; }
            public Destination destination { get; set; }
            public string descr { get; set; }
            [JsonProperty("associated-rule-id")]
            public string AssociatedRuleId { get; set; }
            public string tracker { get; set; }
            public Created created { get; set; }
            public string type { get; set; }
            public string ipprotocol { get; set; }
            public string disabled { get; set; }
        }
        public string status { get; set; }
        public int code { get; set; }
        public int @return { get; set; }
        public string message { get; set; }
        public List<Datum> data { get; set; }
    }
}
