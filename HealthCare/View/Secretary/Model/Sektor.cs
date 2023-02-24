using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.Secretary.Model
{
    class Sektor
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _sector;
        private List<Soba> sobe = new List<Soba>();

        public List<Soba> Sobe
        {
            set { sobe = value; }
            get { return sobe; }
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
    }
}
