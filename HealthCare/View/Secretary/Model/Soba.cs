using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.Secretary.Model
{
    class Soba
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string _soba;
        public string soba
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
                    OnPropertyChanged("soba");
                }
            }
        }
    }
}
