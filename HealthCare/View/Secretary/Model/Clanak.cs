using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sekretar.Model
{
    public class Clanak : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _clanak;
        private string _tekst;
        private ImageSource _slika;
        public ImageSource Slika
        {
            get
            {
                return _slika;
            }
            set
            {
                if (value != _slika)
                {
                    _slika = value;
                    OnPropertyChanged("Slika");
                }
            }
        }
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
        public string Clanci
        {
            get
            {
                return _clanak;
            }
            set
            {
                if (value != _clanak)
                {
                    _clanak = value;
                    OnPropertyChanged("Clanci");
                }
            }
        }
    }
}
