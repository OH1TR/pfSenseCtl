using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.PfSenseAPI
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class AccessTokenRequest
    {
        [JsonProperty("client-id")]
        public string ClientId { get; set; }
        [JsonProperty("client-token")]
        public string ClientToken { get; set; }
    }


}
