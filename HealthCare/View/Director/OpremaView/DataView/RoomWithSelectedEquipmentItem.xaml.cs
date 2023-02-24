using Model.Rooms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.OpremaView.DataView
{
    /// <summary>
    /// Interaction logic for RoomWithSelectedEquipmentItem.xaml
    /// </summary>
    public partial class RoomWithSelectedEquipmentItem : UserControl
    {
        private int _roomNumber;
        private int _amountInRoom;
        private int _floor;
        private Roomtype _roomType;
        private Sector _roomSector;
        public RoomWithSelectedEquipmentItem()
        {
            InitializeComponent();
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

        public int AmountInRoom
        {
            get { return _amountInRoom; }
            set
            {
                if (_amountInRoom != value)
                {
                    _amountInRoom = value;
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
