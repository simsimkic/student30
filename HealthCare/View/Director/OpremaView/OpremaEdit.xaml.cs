using Controller.OtherDataController;
using Controller.RoomControllers;
using HCIProjekat.OpremaView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.OpremaView.DataView;
using HealthCare;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaEdit.xaml
    /// </summary>
    public partial class OpremaEdit : Page
    {
        private const string MANDATORY_NAME_FIELD = "Naziv opreme je obavezno polje!";
        private const string MANDATORY_TYPE_FIELD = "Vrsta opreme je obavezno polje!";
        private string _inventoryName;
        private UserControl Inventory;
        private Inventory _inventory;

        public ObservableCollection<InventoryType> InventoryTypes { get; set; }
        private readonly IInventoryTypeController _inventoryTypeController;
        public readonly IInventoryController _inventoryController;

        public OpremaEdit(UserControl Inventory)
        {
            this.Inventory = Inventory;
            InitializeComponent();
            _inventoryName = ((EquipmentItem)Inventory).EquipmentName;

            var app = Application.Current as App;
            _inventoryTypeController = app.inventoryTypeController;
            _inventoryController = app.inventoryController;


            _inventory = _inventoryController.GetFromID(((EquipmentItem)Inventory).Id);
            InventoryTypes = new ObservableCollection<InventoryType>(_inventoryTypeController.GetAll());

            DataContext = this;

            selectInventoryType();

        }

        private void selectInventoryType()
        {
            foreach (var item in InventoryTypes)
            {
                if (((EquipmentItem)Inventory).EquipmentType.Name.Equals(item.Name))
                {
                    VrstaOpreme.SelectedItem = item;
                    break;
                }
            }
        }

        public string InventoryName
        {
            get { return _inventoryName; }
            set
            {
                if (_inventoryName != value)
                {
                    _inventoryName = value;
                    OnPropertyChanged();
                }
            }
        }

        private void editEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (!isInventoryNameFulfilled())
            {
                ShowError(MANDATORY_NAME_FIELD);
            }
            else if (!isInventoryTypeSelected())
            {
                ShowError(MANDATORY_TYPE_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Izmena opreme", "Da li ste sigurni da zelite da izmenite opremu?",
                                                               new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmEditingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isInventoryTypeSelected()
        {
            return VrstaOpreme.SelectedItem != null;
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }

        private bool isInventoryNameFulfilled()
        {
            return _inventoryName != "" && _inventoryName != null;
        }

        private void confirmEditingEvent(object sender, RoutedEventArgs e)
        {
            _inventoryController.Update(createEquipment());
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        private Inventory createEquipment()
        {
            return new Inventory() { Id = _inventory.Id, QuantityHospital = _inventory.QuantityHospital, QuantityStorage = _inventory.QuantityStorage, Name = _inventoryName, InventoryType = (InventoryType)VrstaOpreme.SelectedItem };
        }

        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene opreme?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEditingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
