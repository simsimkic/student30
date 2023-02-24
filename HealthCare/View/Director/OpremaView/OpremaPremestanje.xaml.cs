using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.OpremaView.DataView;
using HealthCare;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaPremestanje.xaml
    /// </summary>
    public partial class OpremaPremestanje : Page
    {
        public UserControl Inventory { get; set; }
        private int _equipmentAmount;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE = "Uneta kolicina opreme nije validna!";
        private const string INVALID_RANGE_AMOUNT_ERROR_MESSAGE = "Kolicina opreme za premestanje ne moze biti veca od kolicine u magacinu!";
        private const string MANDATORY_ROOM_ERROR_MESSAGE = "Broj prostorije je obavezno polje!";
        private readonly IRoomController _roomController;
        public readonly IInventoryController _inventoryController;

        public static ObservableCollection<Room> Rooms { get; set; }
        public OpremaPremestanje(UserControl Inventory)
        {
            var app = Application.Current as App;
            _roomController = app.roomController;
            _inventoryController = app.inventoryController;

            Rooms = new ObservableCollection<Room>(_roomController.GetAll());

            this.Inventory = Inventory;
            InitializeComponent();
            DataContext = this;
        }

        private void confirmMoving_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidInventoryAmount())
            {
                ShowError(INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE);
            }
            else if (!isInventoryInRange())
            {
                ShowError(INVALID_RANGE_AMOUNT_ERROR_MESSAGE);
            }
            else if (!isRoomToMoveSelected())
            {
                ShowError(MANDATORY_ROOM_ERROR_MESSAGE);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Premestanje opreme", "Da li ste sigurni da zelite da premestite unetu opremu?",
                                                        new RoutedEventHandler(withdrawMovingEquipmentEvent), new RoutedEventHandler(applyMovingEquipmentEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isRoomToMoveSelected()
        {
            return BrojProstorije.SelectedItem != null;
        }

        private bool isInventoryInRange()
        {
            return ((EquipmentItem)Inventory).AmountInStorage >= _equipmentAmount;
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawMovingEquipmentEvent));
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
        private void applyMovingEquipmentEvent(object sender, RoutedEventArgs e)
        {
            _inventoryController.MoveInventoryFromStorage(createInventoryAmount());
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        private InventoryAmount createInventoryAmount()
        {
            return new InventoryAmount() { Amount = _equipmentAmount, inventory = new Inventory() { Id = ((EquipmentItem)Inventory).Id }, room = new Room() { RoomNumber = ((Room)BrojProstorije.SelectedItem).RoomNumber } };
        }

        private void withdrawMovingEquipmentEvent(object sender, RoutedEventArgs e)
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
