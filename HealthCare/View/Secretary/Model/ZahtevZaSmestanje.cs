using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sekretar.Model
{
    public class ZahtevZaSmestanje : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _zahtev;
        private string _karton;
        public string Karton
        {
            get
            {
                return _karton;
            }
            set
            {
                if (value != _karton)
                {
                    _karton = value;
                    OnPropertyChanged("Karton");
                }
            }
        }
        public string Zahtev
        {
            get
            {
                return _zahtev;
            }
            set
            {
                if (value != _zahtev)
                {
                    _zahtev = value;
                    OnPropertyChanged("Zahtev");
                }
            }
        }
    }
}

   