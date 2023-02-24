using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sekretar.Model
{
    public class Obavestenja : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _obavestenje;
        private string _tekst;
        public string Tekst
        {
            get
            {
                return _tekst;
            }
            set
            {
                if (value != _tekst)
                {
                    _tekst = value;
                    OnPropertyChanged("Tekst");
                }
            }
        }
        public string Obavestenje
        {
            get
            {
                return _obavestenje;
            }
            set
            {
                if (value != _obavestenje)
                {
                    _obavestenje = value;
                    OnPropertyChanged("Obavestenje");
                }
            }
        }
    }
}
