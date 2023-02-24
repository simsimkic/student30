using Controller.RoomControllers;
using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ProstorijeView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using HealthCare.View.Director.LekoviView.Converter;
using Model.DataFiltration;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ProstorijeView
{
    /// <summary>
    /// Interaction logic for Prostorije.xaml
    /// </summary>
    public partial class Prostorije : Page
    {
        public ObservableCollection<UserControl> Rooms { get; set; }

        private IRoomController _roomController;
        private readonly IUserController _userController;

        public Prostorije(bool filterDissmis = true, RoomFilter roomFilter = null)
        {

            var app = Application.Current as App;
            _roomController = app.roomController;
            _userController = app.userController;

            InitializeComponent();

            DataContext = this;

            if (!filterDissmis)
            {
                disableFilter.Visibility = Visibility.Visible;
                Rooms = new ObservableCollection<UserControl>(RoomConverter.ConvertRoomListToRoomViewList(_roomController.GetFilteredRooms(roomFilter).ToList()));
            }
            else
            {
                Rooms = new ObservableCollection<UserControl>(RoomConverter.ConvertRoomListToRoomViewList(_roomController.GetAll().ToList()));
            }
        }

        private void equipmentInRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeUvidUOpremu((RoomItem)listaProstorija.SelectedItem), false);
        }

        private void listaProstorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaProstorija.SelectedItem != null)
            {
                setButtonVisibility(Visibility.Visible);
            }
            else
            {
                setButtonVisibility(Visibility.Collapsed);
            }
        }
        private void setButtonVisibility(Visibility state)
        {
            editRoom.Visibility = state;
            deleteRoom.Visibility = state;
            detailRoom.Visibility = state;
            equipmentInRoom.Visibility = state;
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeFilter(), false);
        }

        private void addRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeAdd(), false);
        }

        private void deleteRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            dialog.Children.Add(new ConfirmDialog("Brisanje prostorije", "Da li ste sigurni da zelite da obrisete izabranu prostoriju?",
                                                    new RoutedEventHandler(withdrawDeleteRoomEvent), new RoutedEventHandler(deleteRoomEvent)));
            dialog.Visibility = Visibility.Visible;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawDeleteRoomEvent));
        }
        private void deleteRoomEvent(object sender, RoutedEventArgs e)
        {
            Room forDelete = _roomController.GetFromID(((RoomItem)listaProstorija.SelectedItem).RoomNumber);
            if (_roomController.Delete(forDelete) == null)
            {
                ShowError("Nije moguce obrisati prostoriju u kojoj ima pacijenata");
            }
            else
            {
                dialog.Visibility = Visibility.Collapsed;
                if (_userController.GetDoctorFromWorkRoom(forDelete).Count() > 0)
                {
                    MainWindow.AppWindow.HelpFrame.Navigate(new ChangeEmployeeRoomDialog(DeleteRoomChangeEmployeeRoomEvent, forDelete));
                    MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
                }

            }
        }
        private void DeleteRoomChangeEmployeeRoomEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);

        }
        private void withdrawDeleteRoomEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
                MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeDetails((RoomItem)listaProstorija.SelectedItem), false);
            }
        }

        private void detailRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeDetails((RoomItem)listaProstorija.SelectedItem), false);
        }

        private void editRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeEdit((RoomItem)listaProstorija.SelectedItem), false);
        }

        private void disableFilter_Click(object sender, RoutedEventArgs e)
        {
            disableFilter.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
        }
    }
}
