
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Sekretar.Model
{
    public class Zahtev : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _od;
        private string _do;
        private string _vrsta;
        private string _odobreno;
        public string Od
        {
            get
            {
                return _od;
            }
            set
            {
                if (value != _od)
                {
                    _od = value;
                    OnPropertyChanged("Od");
                }
            }
        }

        public string Odobreno
        {
            get
            {
                return _odobreno;
            }
            set
            {
                if (value != _odobreno)
                {
                    _odobreno = value;
                    OnPropertyChanged("Odobreno");
                }
            }
        }

        public string Do
        {
            get
            {
                return _do;
            }
            set
            {
                if (value != _do)
                {
                    _do = value;
                    OnPropertyChanged("Do");
                }
            }
        }

        public string Vrsta
        {
            get
            {
                return _vrsta;
            }
            set
            {
                if (value != _vrsta)
                {
                    _vrsta = value;
                    OnPropertyChanged("Vrsta");
                }
            }
        }
    }

}
