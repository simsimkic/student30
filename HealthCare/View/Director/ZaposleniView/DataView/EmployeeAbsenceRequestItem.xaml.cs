using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ZaposleniView.DataView
{
    /// <summary>
    /// Interaction logic for EmployeeAbsenceRequestItem.xaml
    /// </summary>
    public partial class EmployeeAbsenceRequestItem : UserControl
    {
        private int _id;
        private int _idAbsence;
        private string _name;
        private string _surname;
        private string _workPlace;
        private AbsenceType _absenceKind;
        private string _reason;
        private string _picture;
        private DateTime _dateStartAbsence;
        private DateTime _dateEndAbsence;
        public EmployeeAbsenceRequestItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        public int IdAbsence
        {
            get { return _idAbsence; }
            set
            {
                if (_idAbsence != value)
                {
                    _idAbsence = value;
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

        public AbsenceType AbsenceKind
        {
            get { return _absenceKind; }
            set
            {
                if (_absenceKind != value)
                {
                    _absenceKind = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateStartAbsence
        {
            get { return _dateStartAbsence; }
            set
            {
                if (_dateStartAbsence != value)
                {
                    _dateStartAbsence = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DateEndAbsence
        {
            get { return _dateEndAbsence; }
            set
            {
                if (_dateEndAbsence != value)
                {
                    _dateEndAbsence = value;
                    OnPropertyChanged();
                }
            }
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

        public string Reason
        {
            get { return _reason; }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
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
