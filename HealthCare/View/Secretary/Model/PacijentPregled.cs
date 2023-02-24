using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sekretar.Model
{
    public class PacijentPregled : INotifyPropertyChanged
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
        private bool _izmeni;
        private bool _premesti;
        private bool _zakazi;
        private string _smena;
        private string _doktor;
        private string _jmbg;

        public string Jmbg
        {
            get
            {
                return _jmbg;
            }
            set
            {
                if (value != _jmbg)
                {
                    _jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }

        public string Smena
        {
            get
            {
                return _smena;
            }
            set
            {
                if (value != _smena)
                {
                    _smena = value;
                    OnPropertyChanged("Smena");
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
        public bool BoolZakazi
        {
            get
            {
                return _zakazi;
            }
            set
            {
                if (value != _zakazi)
                {
                    _zakazi = value;
                    OnPropertyChanged("BoolZakazi");
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
        public bool BoolIzmeni
        {
            get
            {
                return _izmeni;
            }
            set
            {
                if (value != _izmeni)
                {
                    _izmeni = value;
                    OnPropertyChanged("BoolIzmeni");
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