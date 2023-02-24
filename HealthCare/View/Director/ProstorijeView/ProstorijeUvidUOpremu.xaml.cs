using Controller.RoomControllers;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using HealthCare.View.Director.ProstorijeView.Converter;
using Model.Rooms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ProstorijeView
{
    /// <summary>
    /// Interaction logic for ProstorijeUvidUOpremu.xaml
    /// </summary>
    public partial class ProstorijeUvidUOpremu : Page
    {
        public ObservableCollection<UserControl> EquipmentInRoom { get; set; }
        public UserControl RoomInformation { get; set; }

        private IRoomController _roomController;
        private IInventoryController _inventoryController;

        public ProstorijeUvidUOpremu(UserControl RoomItem)
        {
            var app = Application.Current as App;
            _roomController = app.roomController;
            _inventoryController = app.inventoryController;

            IEnumerable<InventoryAmount> inventoryAmounts = new List<InventoryAmount>(_roomController.GetInventoryFromRoom(new Room() { RoomNumber = ((RoomItem)RoomItem).RoomNumber }));
            foreach (InventoryAmount iA in inventoryAmounts)
                iA.inventory = _inventoryController.GetFromID(iA.inventory.Id);

            RoomInformation = RoomItem;
            EquipmentInRoom = new ObservableCollection<UserControl>(EquipmentInRoomConverter.ConvertInventoryAmountListToEquipmentInRoomViewList(inventoryAmounts.ToList()).ToList());

            InitializeComponent();

            DataContext = this;
        }

        private void movingEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijePremestanjeOpreme((RoomItem)this.RoomInformation), false);
        }

        private void addingEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeDodavanjeOpreme((RoomItem)this.RoomInformation), false);

        }
    }
}
