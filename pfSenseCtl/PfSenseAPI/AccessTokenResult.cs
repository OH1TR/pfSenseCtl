using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<AccessTokenResult>(myJsonResponse); 


    public class AccessTokenResult
    {
        public class Data
        {
            public string token { get; set; }
        }

        public string status { get; set; }
        public int code { get; set; }
        public int @return { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }


}
