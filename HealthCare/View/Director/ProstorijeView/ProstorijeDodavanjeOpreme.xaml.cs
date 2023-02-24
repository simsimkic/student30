using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using HealthCare.View.Director.ProstorijeView.Converter;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ProstorijeView
{
    /// <summary>
    /// Interaction logic for ProstorijeDodavanjeOpreme.xaml
    /// </summary>
    public partial class ProstorijeDodavanjeOpreme : Page
    {
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE = "Uneta kolicina opreme nije validna!";
        private const string INVALID_RANGE_AMOUNT_ERROR_MESSAGE = "Kolicina opreme za premestanje ne moze biti veca od kolicine u magacinu!";

        private int _equipmentAmount;
        public ObservableCollection<UserControl> EquipmentInStorage { get; set; }
        public UserControl RoomInformation { get; set; }
        public readonly IInventoryController _inventoryController;

        public ProstorijeDodavanjeOpreme(UserControl RoomItem)
        {
            var app = Application.Current as App;
            _inventoryController = app.inventoryController;

            RoomInformation = RoomItem;
            EquipmentInStorage = new ObservableCollection<UserControl>(EquipmentInStorageConverter.ConvertInventoryAmountListToEquipmentInRoomViewList(_inventoryController.GetAll().Where(x => x.QuantityStorage > 0).ToList()));

            InitializeComponent();

            DataContext = this;

        }

        private void listaOpreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaOpreme.SelectedItem != null)
            {
                movingEquipment.Visibility = Visibility.Visible;
            }
            else
            {
                movingEquipment.Visibility = Visibility.Collapsed;
            }
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void addEquipmentInRoom_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidInventoryAmount())
            {
                ShowError(INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE);
            }
            else if (!isInventoryInRange())
            {
                ShowError(INVALID_RANGE_AMOUNT_ERROR_MESSAGE);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Dodavanje opreme u prostoriju", "Da li ste sigurni da zelite da dodate unetu kolicinu opreme u prostoriju?",
                                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }
        private bool isInventoryInRange()
        {
            return ((EquipmentInStorageItem)listaOpreme.SelectedItem).EquipmentAmount >= _equipmentAmount;
        }

        private bool isValidInventoryAmount()
        {
            if (!int.TryParse(amountInventory.Text, out int invAmount))
            {
                return false;
            }
            else
            {
                _equipmentAmount = invAmount;
                return true;
            }
        }
        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            _inventoryController.MoveInventoryFromStorage(createInventoryAmount());
            dialog.Visibility = Visibility.Collapsed;
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeUvidUOpremu(RoomInformation), false);
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeDodavanjeOpreme(RoomInformation), false);
        }

        private InventoryAmount createInventoryAmount()
        {
            return new InventoryAmount() { Amount = _equipmentAmount, inventory = new Inventory() { Id = ((EquipmentInStorageItem)listaOpreme.SelectedItem).Id }, room = new Room() { RoomNumber = ((RoomItem)RoomInformation).RoomNumber } };
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void amountInventory_PreviewTextInput(object sender, TextCompositionEventArgs e)
                     => e.Handled = !isTextInt(e.Text);

        private bool isTextInt(string input) => !_intRegex.IsMatch(input);
        private void amountInventory_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextInt(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
