using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.RenoviranjaView.DataView
{
    /// <summary>
    /// Interaction logic for RenovationItem.xaml
    /// </summary>
    public partial class RenovationItem : UserControl
    {
        private int _roomNumber;
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _renovationDescription;
        public RenovationItem()
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
        public int RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (_roomNumber != value)
                {
                    _roomNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RenovationDescription
        {
            get { return _renovationDescription; }
            set
            {
                if (_renovationDescription != value)
                {
                    _renovationDescription = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
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
