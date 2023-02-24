using Controller.OtherDataController;
using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.DataFiltration;
using Model.Rooms;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ProstorijeView
{
    /// <summary>
    /// Interaction logic for ProstorijeFilter.xaml
    /// </summary>
    public partial class ProstorijeFilter : Page
    {
        private static readonly Regex _decimalRegex = new Regex(@"[^0-9,]+$");

        private const string INVALID_ROOM_SIZE_ERROR_MESSAGE = "Uneta kvadratura nije validna!";
        private const string INVALID_ROOM_SIZE_RANGE_ERROR_MESSAGE = "Kvadratura od mora biti manja od kvadrature do!";
        private double _sizeFrom;
        private double _sizeTo;
        public ObservableCollection<Sector> Sectors { get; set; }
        public static ObservableCollection<Room> Rooms { get; set; }

        private IRoomController _roomController;
        private ISectorController _sectorController;
        public ProstorijeFilter()
        {
            var app = Application.Current as App;
            _sectorController = app.sectorController;
            _roomController = app.roomController;
            Rooms = new ObservableCollection<Room>(_roomController.GetAll());
            Sectors = new ObservableCollection<Sector>(_sectorController.GetAll());

            InitializeComponent();

            DataContext = this;
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!anythingFulFilled())
            {
                MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
                return;
            }
            if (isAmountFromFulFilled())
            {
                if (!isRoomSizeFromValid())
                {
                    ShowError(INVALID_ROOM_SIZE_ERROR_MESSAGE);
                    return;
                }
            }
            if (isAmountToFulFilled())
            {
                if (!isRoomSizeToValid())
                {
                    ShowError(INVALID_ROOM_SIZE_ERROR_MESSAGE);
                    return;
                }
                if (isAmountFromFulFilled())
                {
                    if (!isRoomSizeToValid())
                    {
                        ShowError(INVALID_ROOM_SIZE_ERROR_MESSAGE);
                        return;
                    }

                    if (_sizeFrom > _sizeTo)
                    {
                        ShowError(INVALID_ROOM_SIZE_RANGE_ERROR_MESSAGE);
                        return;
                    }
                }
            }
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(false, createFilter()), false);
        }

        private RoomFilter createFilter()
        {
            int roomNumber = -1;
            if (BrojProstorije.SelectedItem != null)
                roomNumber = ((Room)BrojProstorije.SelectedItem).RoomNumber;

            int Floor = -1;
            if (floor.SelectedItem != null)
                Floor = floor.SelectedIndex + 1;

            int roomtype = -1;
            if (roomType.SelectedItem != null)
            {
                if (Roomtype.Examinationroom == (Roomtype)roomType.SelectedItem)
                    roomtype = 2;
                else if (Roomtype.Ward == (Roomtype)roomType.SelectedItem)
                    roomtype = 0;
                else
                    roomtype = 1;

            }

            if (!isAmountFromFulFilled())
                _sizeFrom = 0;
            if (!isAmountToFulFilled())
                _sizeTo = 0;
            return new RoomFilter() { RoomNumber = roomNumber, Floor = Floor, RoomType = roomtype, Sector = (Sector)sector.SelectedItem, RoomSizeFrom = _sizeFrom, RoomSizeTo = _sizeTo };
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private bool isAmountToFulFilled()
        {
            return amountTo.Text != "" && amountTo.Text != null;
        }

        private bool isAmountFromFulFilled()
        {
            return amountFrom.Text != "" && amountFrom.Text != null;
        }

        private bool anythingFulFilled()
        {
            return (amountTo.Text != "" && amountTo.Text != null) || (amountFrom.Text != "" && amountFrom.Text != null) ||
                    (BrojProstorije.SelectedItem != null) || roomType.SelectedItem != null || sector.SelectedItem != null || floor.SelectedItem != null;
        }


        private bool isRoomSizeFromValid()
        {
            if (!Double.TryParse(amountFrom.Text, out double size))
            {
                return false;
            }
            else
            {
                _sizeFrom = size;
                return true;
            }
        }
        private bool isRoomSizeToValid()
        {
            if (!Double.TryParse(amountTo.Text, out double size))
            {
                return false;
            }
            else
            {
                _sizeTo = size;
                return true;
            }
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
