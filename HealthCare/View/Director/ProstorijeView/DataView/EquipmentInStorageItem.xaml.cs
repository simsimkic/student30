using Model.Rooms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.ProstorijeView.DataView
{
    /// <summary>
    /// Interaction logic for EquipmentInRoomItem.xaml
    /// </summary>
    public partial class EquipmentInStorageItem : UserControl
    {
        private int _id;
        private InventoryType _equipmentType;
        private string _equipmentName;
        private int _equipmentAmount;
        public EquipmentInStorageItem()
        {
            InitializeComponent();
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

        public int EquipmentAmount
        {
            get { return _equipmentAmount; }
            set
            {
                if (_equipmentAmount != value)
                {
                    _equipmentAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EquipmentName
        {
            get { return _equipmentName; }
            set
            {
                if (_equipmentName != value)
                {
                    _equipmentName = value;
                    OnPropertyChanged();
                }
            }
        }

        public InventoryType EquipmentType
        {
            get { return _equipmentType; }
            set
            {
                if (_equipmentType != value)
                {
                    _equipmentType = value;
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
