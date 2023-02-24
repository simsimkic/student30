using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sekretar.Model
{
    public class UvidOperacije : INotifyPropertyChanged
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
        private string _vreme;
        private string _trajanje;
        private bool _premesti;
        private string _sector;
        private string _soba;
        private string _doktor;
        private string _datum;

        public string Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                if (value != _datum)
                {
                    _datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }

        public string Doktor
        {
            get
            {
                return _doktor;
            }
            set
            {
                if (value != _doktor)
                {
                    _doktor = value;
                    OnPropertyChanged("Doktor");
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
        public string Trajanje
        {
            get
            {
                return _trajanje;
            }
            set
            {
                if (value != _trajanje)
                {
                    _trajanje = value;
                    OnPropertyChanged("Trajanje");
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
    }
}
