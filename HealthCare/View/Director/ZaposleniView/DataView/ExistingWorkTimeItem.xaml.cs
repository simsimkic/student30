using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ZaposleniView.DataView
{
    /// <summary>
    /// Interaction logic for ExistingWorkTimeItem.xaml
    /// </summary>
    public partial class ExistingWorkTimeItem : UserControl
    {
        private int _id;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private string _monday;
        private string _tuesday;
        private string _wednesday;
        private string _thursday;
        private string _friday;
        private string _saturday;
        private string _sunday;
        public ExistingWorkTimeItem()
        {
            InitializeComponent();
            DataContext = this;
        }
        public string Monday
        {
            get { return _monday; }
            set
            {
                if (_monday != value)
                {
                    _monday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Tuesday
        {
            get { return _tuesday; }
            set
            {
                if (_tuesday != value)
                {
                    _tuesday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Wednesday
        {
            get { return _wednesday; }
            set
            {
                if (_wednesday != value)
                {
                    _wednesday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Thursday
        {
            get { return _thursday; }
            set
            {
                if (_thursday != value)
                {
                    _thursday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Friday
        {
            get { return _friday; }
            set
            {
                if (_friday != value)
                {
                    _friday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Saturday
        {
            get { return _saturday; }
            set
            {
                if (_saturday != value)
                {
                    _saturday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Sunday
        {
            get { return _sunday; }
            set
            {
                if (_sunday != value)
                {
                    _sunday = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime dateFrom
        {
            get { return _dateFrom; }
            set
            {
                if (_dateFrom != value)
                {
                    _dateFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime dateTo
        {
            get { return _dateTo; }
            set
            {
                if (_dateTo != value)
                {
                    _dateTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
