using pfSenseCtl.PfSenseAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pfSenseCtl
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// pfSense API access object
        /// </summary>
        PfSenseApi Api;

        /// <summary>
        /// List of hosts in UI
        /// </summary>

        string LanNetworkName = "lan";

        public ObservableCollection<Model.Host> Hosts { get; set; }
        public MainWindow()
        {             
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            LanNetworkName = ConfigurationManager.AppSettings["LanNetworkName"];

            Hosts = new ObservableCollection<Model.Host>();
            DataContext = this;

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Timer for updating host list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateHostList();
        }


        /// <summary>
        /// Main window loaded. Load hosts
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Api = new PfSenseApi();
            Api.Authenticate();
            UpdateHostList();

        }

        /// <summary>
        /// We are marking our rules by inserting this string to rule description
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        string IPToDesc(string ip)
        {
            return "pfSenseCtl:" + ip+":";
        }


        /// <summary>
        /// Loads new host list from API. Update window.
        /// </summary>
        void UpdateHostList()
        {
            foreach (var i in GetHosts())
            {
                if(!Hosts.Any(j=>j.IPAddress.Address==i.IPAddress.Address))
                    Hosts.Add(i);
            }
            
            var rules = Api.GetFirewallRules();

            foreach (var h in Hosts)
            {
                var rule = rules.data.FirstOrDefault(i => i.descr.Contains(IPToDesc(h.IPAddress.ToString())));

                if (rule == null || rule.disabled != null)
                {
                    h.Blocked = false;
                }
                else
                    h.Blocked = true;
            }
        }

        /// <summary>
        /// Load known hosts from DHCP-leases and static mappings
        /// </summary>
        /// <returns></returns>
        List<Model.Host> GetHosts()
        {
            var leases = Api.GetDhcpLeases();
            var staticMapping = Api.GetDhcpStaticMappings("lan");

            List<Model.Host> result = new List<Model.Host>();

            result.AddRange(leases.data.Where(i => i.online).Select(i => new Model.Host() { IPAddress = System.Net.IPAddress.Parse(i.ip), Name = i.hostname }));
            result.AddRange(staticMapping.data.Select(i => new Model.Host() { IPAddress = System.Net.IPAddress.Parse(i.ipaddr), Name = i.hostname }));
            return result.OrderBy(i => i.IPAddress.Address).ToList();
        }

        /// <summary>
        /// User clicked block/unblock button. Toggle rule disabled status, of create rule if it does not exist.
        /// </summary>
        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            Button s = sender as Button;
            Model.Host host = s?.Tag as Model.Host;
            if(host!=null)
            {
                var rules = Api.GetFirewallRules();
                var rule = rules.data.FirstOrDefault(i => i.descr.Contains(IPToDesc(host.IPAddress.ToString())));
                if(rule!=null)
                {
                    Api.UpdateFilewallRule(rule.tracker, !host.Blocked);
                }
                else
                {
                    var newRule = new NewFirewallRule()
                    {
                        type = "block",
                        @interface = "lan",
                        ipprotocol = "inet",
                        protocol = "any",
                        src = host.IPAddress.ToString(),
                        dst="any",
                        srcport="any",
                        dstport=0,
                        descr= IPToDesc(host.IPAddress.ToString()),
                        top=true,
                        apply=true
                    };

                    Api.CreateFirewallRule(newRule);
                }
            }
            UpdateHostList();
        }
    }
}
