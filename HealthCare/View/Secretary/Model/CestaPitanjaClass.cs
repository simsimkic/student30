
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sekretar.Model
{
    public class CestaPitanjaClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _pitanje;
        private string _odgovor;
        public string Odgovor
        {
            get
            {
                return _odgovor;
            }
            set
            {
                if (value != _odgovor)
                {
                    _odgovor = value;
                    OnPropertyChanged("Odgovor");
                }
            }
        }
        public string Pitanje
        {
            get
            {
                return _pitanje;
            }
            set
            {
                if (value != _pitanje)
                {
                    _pitanje = value;
                    OnPropertyChanged("Pitanje");
                }
            }
        }
    }

    }
