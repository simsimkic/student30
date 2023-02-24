using Controller.RoomControllers;
using HCIProjekat.OpremaView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.OpremaView.DataView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using HealthCare.View.Director.ProstorijeView.Converter;
using Model.Rooms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ProstorijeView
{
    /// <summary>
    /// Interaction logic for ProstorijePremestanjeOpreme.xaml
    /// </summary>
    public partial class ProstorijePremestanjeOpreme : Page
    {
        private int _equipmentAmount;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE = "Uneta kolicina opreme nije validna!";
        private const string INVALID_RANGE_AMOUNT_ERROR_MESSAGE = "Kolicina opreme za premestanje ne moze biti veca od kolicine u prostoriji!";
        private const string MANDATORY_ROOM_ERROR_MESSAGE = "Broj prostorije je obavezno polje!";

        public ObservableCollection<UserControl> EquipmentInRoom { get; set; }
        public UserControl RoomInformation { get; set; }
        public readonly IInventoryController _inventoryController;
        private IRoomController _roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        private UserControl Inventory;
        public ProstorijePremestanjeOpreme(UserControl RoomItem, UserControl Inventory = null)
        {
            var app = Application.Current as App;
            _inventoryController = app.inventoryController;
            _roomController = app.roomController;

            this.Inventory = Inventory;
            RoomInformation = RoomItem;

            IEnumerable<InventoryAmount> inventoryAmounts = new List<InventoryAmount>(_roomController.GetInventoryFromRoom(createRoom(((RoomItem)RoomInformation).RoomNumber)));
            foreach (InventoryAmount iA in inventoryAmounts)
                iA.inventory = _inventoryController.GetFromID(iA.inventory.Id);

            EquipmentInRoom = new ObservableCollection<UserControl>(EquipmentInRoomConverter.ConvertInventoryAmountListToEquipmentInRoomViewList(inventoryAmounts.ToList()).ToList());
            Rooms = new ObservableCollection<Room>(_roomController.GetAll().Where(x => x.RoomNumber != ((RoomItem)RoomInformation).RoomNumber));
           
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

        private void moveEquipment_Click(object sender, RoutedEventArgs e)
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
                dialog.Children.Add(new ConfirmDialog("Premestanje opreme u prostoriju", "Da li ste sigurni da zelite da premestite unetu kolicinu opreme u prostoriju?",
                                                                new RoutedEventHandler(withdrawMovinvgEquipmentEvent), new RoutedEventHandler(confirmMovinvgEquipmentEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }
        private bool isRoomToMoveSelected()
        {
            return BrojProstorije.SelectedItem != null;
        }
        private bool isInventoryInRange()
        {
            return ((EquipmentInRoomItem)listaOpreme.SelectedItem).EquipmentAmount >= _equipmentAmount;
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
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawMovinvgEquipmentEvent));
        }
        private void confirmMovinvgEquipmentEvent(object sender, RoutedEventArgs e)
        {
            _inventoryController.MoveInventoryFromRoom(createRoom(((RoomItem)RoomInformation).RoomNumber), createInventoryAmount());
            dialog.Visibility = Visibility.Collapsed;
            bool comingFromEquipment = false;
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            if (MainWindow.undoPages[MainWindow.undoPages.Count - 1].GetType().Equals(typeof(OpremaPoProstorijama)))
                comingFromEquipment = true;
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);

            if (comingFromEquipment)
                MainWindow.AppWindow.navigateWithTitleChange(new OpremaPoProstorijama((EquipmentItem)Inventory), false);
            else
                MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeUvidUOpremu(RoomInformation), false);
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijePremestanjeOpreme(RoomInformation), false);
        }

        private Room createRoom(int id)
        {
            return new Room() { RoomNumber = id };
        }

        private InventoryAmount createInventoryAmount()
        {
            return new InventoryAmount() { Amount = _equipmentAmount, inventory = new Inventory() { Id = ((EquipmentInRoomItem)listaOpreme.SelectedItem).Id }, room = new Room() { RoomNumber = ((Room)BrojProstorije.SelectedItem).RoomNumber } };
        }

        private void withdrawMovinvgEquipmentEvent(object sender, RoutedEventArgs e)
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
