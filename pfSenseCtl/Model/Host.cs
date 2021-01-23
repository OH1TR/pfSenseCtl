using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pfSenseCtl.Model
{
    public class Host : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;     

        IPAddress _IPAddress;
        public IPAddress IPAddress
        {
            get { return _IPAddress; }
            set
            {
                _IPAddress = value;
                OnPropertyChanged();
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        bool _Blocked;
        public bool Blocked
        {
            get { return _Blocked; }
            set
            {
                _Blocked = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
