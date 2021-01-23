using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace pfSenseCtl.PfSenseAPI
{
    class PfSenseApi
    {
        string Url;
        string Username;
        string Password;
        string Token = "";

        public PfSenseApi()
        {
            Url = ConfigurationManager.AppSettings["Url"];
            Username = ConfigurationManager.AppSettings["Username"];
            Password = ConfigurationManager.AppSettings["Password"];

        }
        public bool Authenticate()
        {
            AccessTokenRequest req = new AccessTokenRequest() { ClientId = Username, ClientToken = Password };
            var json = JsonConvert.SerializeObject(req);
            var data = new StringContent(json, null, "application/json");
            data.Headers.ContentType.CharSet = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(Url + "access_token", data).Result;
                var respTxt = response.Content.ReadAsStringAsync().Result;
                AccessTokenResult result = JsonConvert.DeserializeObject<AccessTokenResult>(respTxt);
                if (result.code == 200)
                {
                    Token = result.data.token;
                    return true;
                }
                else
                    return false;
            }
        }

        public T Post<T>(string service, object data)
        {
            string json;

            if (data != null)
                json = JsonConvert.SerializeObject(data);
            else
                json = "{}";

            var content = new StringContent(json, null, "application/json");
            content.Headers.ContentType.CharSet = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                var response = client.PostAsync(Url + service, content).Result;
                var respTxt = response.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(respTxt);
                return result;
            }
        }

        public T Get<T>(string service)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                var response = client.GetAsync(Url + service).Result;
                var respTxt = response.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(respTxt);
                return result;
            }
        }

        public T Put<T>(string service, object data)
        {
            string json;

            if (data != null)
                json = JsonConvert.SerializeObject(data);
            else
                json = "{}";

            var content = new StringContent(json, null, "application/json");
            content.Headers.ContentType.CharSet = string.Empty;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                var response = client.PutAsync(Url + service, content).Result;
                var respTxt = response.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(respTxt);
                return result;
            }
        }

        public void CheckConnection()
        {
            if (Token == null)
                Authenticate();

            VersionsResult version = null;
            try
            {
                version = Get<VersionsResult>("system/version");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            if(version==null || version.code!=200)
                Authenticate();

        }

        public DhcpLeasesResponse GetDhcpLeases()
        {
            CheckConnection();

            return Get<DhcpLeasesResponse>("services/dhcpd/lease");
        }

        public DhcpStaticMappingResponse GetDhcpStaticMappings(string interf)
        {
            CheckConnection();

            return Get<DhcpStaticMappingResponse>("services/dhcpd/static_mapping?interface=" + interf);
        }

        public FirewallRulesResponse GetFirewallRules()
        {
            CheckConnection();

            return Get<FirewallRulesResponse>("firewall/rule");
        }

        public void UpdateFilewallRule(string tracker,bool enabled)
        {
            CheckConnection();

            Put<object>("firewall/rule", new RuleUpdate() { tracker = tracker, disabled = !enabled, apply = true });
        }

        public void CreateFirewallRule(NewFirewallRule rule)
        {
            CheckConnection();

            Post<object>("firewall/rule", rule);
        }
    }
}
