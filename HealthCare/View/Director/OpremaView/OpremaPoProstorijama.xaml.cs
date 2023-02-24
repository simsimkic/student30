using Controller.RoomControllers;
using HCIProjekat.ProstorijeView;
using HCIProjekat.View.OpremaView.DataView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using HealthCare.View.Director.OpremaView.Converter;
using Model.Rooms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaPoProstorijama.xaml
    /// </summary>
    public partial class OpremaPoProstorijama : Page
    {
        public ObservableCollection<UserControl> EquipmentInRoom { get; set; }
        public readonly IInventoryController _inventoryController;
        private IRoomController _roomController;

        public UserControl Inventory { get; set; }
        public OpremaPoProstorijama(UserControl Inventory)
        {
            var app = Application.Current as App;
            _inventoryController = app.inventoryController;
            _roomController = app.roomController;
            this.Inventory = Inventory;

            IEnumerable<InventoryAmount> inventoryAmounts = new List<InventoryAmount>(_inventoryController.GetInventoryAmountInRooms(new Inventory() { Id = ((EquipmentItem)Inventory).Id }));
            foreach (InventoryAmount iA in inventoryAmounts)
                iA.room = _roomController.GetFromID(iA.room.RoomNumber);
            EquipmentInRoom = new ObservableCollection<UserControl>(RoomsWithEquipmentConverter.ConvertRoomInventoryListToInventoryViewList(inventoryAmounts.ToList()));

            InitializeComponent();
            DataContext = this;
        }

        private void movingEquipment_Click(object sender, RoutedEventArgs e)
        {
            RoomItem roomItem = new RoomItem()
            {
                Floor = ((RoomWithSelectedEquipmentItem)listaProstorija.SelectedItem).Floor,
                RoomNumber = ((RoomWithSelectedEquipmentItem)listaProstorija.SelectedItem).RoomNumber,
                RoomSector = ((RoomWithSelectedEquipmentItem)listaProstorija.SelectedItem).RoomSector,
                RoomType = ((RoomWithSelectedEquipmentItem)listaProstorija.SelectedItem).RoomType
            };
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijePremestanjeOpreme(roomItem, Inventory), false);
        }
    }
}
