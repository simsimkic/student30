using Controller.OtherDataController;
using Controller.RoomControllers;
using HCIProjekat.ProstorijeView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using Model.Rooms;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.View.ProstorijeView
{
    /// <summary>
    /// Interaction logic for ProstorijeEdit.xaml
    /// </summary>
    public partial class ProstorijeEdit : Page
    {
        private static readonly Regex _decimalRegex = new Regex(@"[^0-9,]+$");
        private const string INVALID_ROOM_SIZE_ERROR_MESSAGE = "Uneta velicina prostorije nije validna!";
        private const string MANDATORY_FLOOR_FIELD = "Sprat prostorije je obavezno polje!";
        private const string MANDATORY_SECTOR_FIELD = "Odeljenje prostorije je obavezno polje!";
        private const string MANDATORY_TYPE_FIELD = "Vrsta prostorije je obavezno polje!";

        private double _roomSize;
        private Room room;
        public ObservableCollection<Sector> Sectors { get; set; }
        private IRoomController _roomController;
        private ISectorController _sectorController;
        public ProstorijeEdit(UserControl Room)
        {
            InitializeComponent();

            DataContext = this;
            var app = Application.Current as App;
            _sectorController = app.sectorController;
            _roomController = app.roomController;

            this.room = _roomController.GetFromID(((RoomItem)Room).RoomNumber);
            Sectors = new ObservableCollection<Sector>(_sectorController.GetAll());

            roomSize.Text = room.RoomSize.ToString();
            roomType.SelectedItem = room.RoomType;
            roomNumber.Text = room.RoomNumber.ToString();
            Floor.SelectedIndex = room.Floor - 1;
            sector.SelectedValue = room.RoomSector.Name;

        }
        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene prostorije?",
                                                   new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditingEvent),
                                                   "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void confirmEditingEvent(object sender, RoutedEventArgs e)
        {
            _roomController.Update(createRoom());
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
        }

        private Room createRoom()
        {
            return new Room() { Floor = room.Floor, patients = room.patients, RoomNumber = room.RoomNumber, RoomSector = (Sector)sector.SelectedItem, RoomType = (Roomtype)roomType.SelectedItem, RoomSize = _roomSize };
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }


        private void withdrawEditingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void editRoom_Click(object sender, RoutedEventArgs e)
        {
            if (!isFloorSelected())
            {
                ShowError(MANDATORY_FLOOR_FIELD);
            }
            else if (!isSectorSelected())
            {
                ShowError(MANDATORY_SECTOR_FIELD);
            }
            else if (!isRoomTypeSelected())
            {
                ShowError(MANDATORY_TYPE_FIELD);
            }
            else if (!isRoomSizeValid())
            {
                ShowError(INVALID_ROOM_SIZE_ERROR_MESSAGE);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Izmena prostorije", "Da li ste sigurni da zelite da izmenite prostoriju?",
                                                                 new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmEditingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }
        private bool isRoomSizeValid()
        {
            if (!Double.TryParse(roomSize.Text, out double size))
            {
                return false;
            }
            else
            {
                _roomSize = size;
                return true;
            }
        }

        private bool isRoomTypeSelected()
        {
            return roomType.SelectedItem != null;
        }

        private bool isSectorSelected()
        {
            return sector.SelectedItem != null;
        }

        private bool isFloorSelected()
        {
            return Floor.SelectedItem != null;
        }
        private void roomSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
            => e.Handled = !isTextDouble(e.Text);

        private bool isTextDouble(string input) => !_decimalRegex.IsMatch(input);

        private void roomSize_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextDouble(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

    }
}
