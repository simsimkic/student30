using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sekretar.Model
{
    public class Pregledi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _dan;
        private string _pacijent;
        private string _vreme;
        public string Pacijent
        {
            get
            {
                return _pacijent;
            }
            set
            {
                if (value != _pacijent)
                {
                    _pacijent = value;
                    OnPropertyChanged("Pacijent");
                }
            }
        }
        public string Vreme
        {
            get
            {
                return _vreme;
            }
            set
            {
                if (value != _vreme)
                {
                    _vreme = value;
                    OnPropertyChanged("Vreme");
                }
            }
        }
        public string Dan
        {
            get
            {
                return _dan;
            }
            set
            {
                if (value != _dan)
                {
                    _dan = value;
                    OnPropertyChanged("Dan");
                }
            }
        }
    }
}
