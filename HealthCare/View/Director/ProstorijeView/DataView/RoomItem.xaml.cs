using Model.Rooms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ProstorijeView.DataView
{
    /// <summary>
    /// Interaction logic for RoomItem.xaml
    /// </summary>
    public partial class RoomItem : UserControl
    {
        private int _roomNumber;
        private int _floor;
        private Roomtype _roomType;
        private Sector _roomSector;
        public RoomItem()
        {
            InitializeComponent();
            DataContext = this;
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

        public int Floor
        {
            get { return _floor; }
            set
            {
                if (_floor != value)
                {
                    _floor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Roomtype RoomType
        {
            get { return _roomType; }
            set
            {
                if (_roomType != value)
                {
                    _roomType = value;
                    OnPropertyChanged();
                }
            }
        }

        public Sector RoomSector
        {
            get { return _roomSector; }
            set
            {
                if (_roomSector != value)
                {
                    _roomSector = value;
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
