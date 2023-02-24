using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ReportView.DataView
{
    /// <summary>
    /// Interaction logic for FilteredDoctorItem.xaml
    /// </summary>
    public partial class FilteredDoctorItem : UserControl
    {
        private int _id;
        private int _workTime;
        private int _apointmentTime;
        private string _name;
        private string _surname;
        private string _workPlace;
        private string _picture;

        public FilteredDoctorItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Picture
        {
            get { return _picture; }
            set
            {
                if (_picture != value)
                {
                    _picture = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AppointmentTime
        {
            get { return _apointmentTime; }
            set
            {
                if (_apointmentTime != value)
                {
                    _apointmentTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public int WorkTime
        {
            get { return _workTime; }
            set
            {
                if (_workTime != value)
                {
                    _workTime = value;
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

        public string DoctorName
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WorkPlace
        {
            get { return _workPlace; }
            set
            {
                if (_workPlace != value)
                {
                    _workPlace = value;
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
