using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sekretar.Model
{
    public class SobePacijenti : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _pacijent;
        private bool _premesti;
        private string _sector;
        private string _soba;
        private string _datumOd;
        private string _datumDo;

        public string DatumDo
        {
            get
            {
                return _datumDo;
            }
            set
            {
                if (value != _datumDo)
                {
                    _datumDo = value;
                    OnPropertyChanged("DatumDo");
                }
            }
        }

        public string DatumOd
        {
            get
            {
                return _datumOd;
            }
            set
            {
                if (value != _datumOd)
                {
                    _datumOd = value;
                    OnPropertyChanged("DatumOd");
                }
            }
        }

        public string Soba
        {
            get
            {
                return _soba;
            }
            set
            {
                if (value != _soba)
                {
                    _soba = value;
                    OnPropertyChanged("Soba");
                }
            }
        }

        public string Sector
        {
            get
            {
                return _sector;
            }
            set
            {
                if (value != _sector)
                {
                    _sector = value;
                    OnPropertyChanged("Sector");
                }
            }
        }



        public bool BoolPremesti
        {
            get
            {
                return _premesti;
            }
            set
            {
                if (value != _premesti)
                {
                    _premesti = value;
                    OnPropertyChanged("BoolPremesti");
                }
            }
        }
        
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
    }
}

