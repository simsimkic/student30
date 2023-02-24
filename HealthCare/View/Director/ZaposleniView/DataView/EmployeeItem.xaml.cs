using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ZaposleniView.DataView
{
    /// <summary>
    /// Interaction logic for EmployeeItem.xaml
    /// </summary>
    public partial class EmployeeItem : UserControl
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _workPlace;
        private string picture;

        public EmployeeItem()
        {
            InitializeComponent();
            DataContext = this;
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

        public string EmployeeName
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

        public string Picture
        {
            get { return picture; }
            set
            {
                if (picture != value)
                {
                    picture = value;
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
