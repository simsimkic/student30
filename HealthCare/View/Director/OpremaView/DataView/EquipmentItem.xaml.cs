using Model.Rooms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace HCIProjekat.View.OpremaView.DataView
{
    /// <summary>
    /// Interaction logic for EquipmentItem.xaml
    /// </summary>
    public partial class EquipmentItem : UserControl
    {
        private int _id;
        private InventoryType _equipmentType;
        private string _equipmentName;
        private int _amountInStorage;
        private int _amountInHospital;
        public EquipmentItem()
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

        public int AmountInStorage
        {
            get { return _amountInStorage; }
            set
            {
                if (_amountInStorage != value)
                {
                    _amountInStorage = value;
                    OnPropertyChanged();
                }
            }
        }

        public int AmountInHospital
        {
            get { return _amountInHospital; }
            set
            {
                if (_amountInHospital != value)
                {
                    _amountInHospital = value;
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
