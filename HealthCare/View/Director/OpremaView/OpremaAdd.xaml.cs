using Controller.OtherDataController;
using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaAdd.xaml
    /// </summary>
    public partial class OpremaAdd : Page
    {
        private const string MANDATORY_NAME_FIELD = "Naziv opreme je obavezno polje!";
        private const string MANDATORY_TYPE_FIELD = "Vrsta opreme je obavezno polje!";
        private string _inventoryName;

        public ObservableCollection<InventoryType> InventoryTypes { get; set; }
        private readonly IInventoryTypeController _inventoryTypeController;
        private readonly IInventoryController _inventoryController;


        public OpremaAdd()
        {
            InitializeComponent();

            var app = Application.Current as App;
            _inventoryTypeController = app.inventoryTypeController;
            _inventoryController = app.inventoryController;

            InventoryTypes = new ObservableCollection<InventoryType>(_inventoryTypeController.GetAll());

            DataContext = this;
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

        private void addEquipment_Click(object sender, RoutedEventArgs e)
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
                dialog.Children.Add(new ConfirmDialog("Dodavanje opreme", "Da li ste sigurni da zelite da dodate unetu opremu?",
                                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
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

        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            _inventoryController.Create(createInventory());
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        private Inventory createInventory()
        {
            return new Inventory() { QuantityStorage = 0, InventoryType = (InventoryType)VrstaOpreme.SelectedItem, Name = _inventoryName, QuantityHospital = 0 };
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od dodavanja", "Da li ste sigurni da zelite da odustanete od dodavanja opreme?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent),
                                                    "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
