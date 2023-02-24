using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.HelpWizardView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using HealthCare.View.Director.LekoviView.Converter;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.RenoviranjaView
{
    /// <summary>
    /// Interaction logic for RenoviranjaAddFirst.xaml
    /// </summary>
    public partial class RenoviranjaAddFirst : Page
    {
        private readonly IRoomController _roomController;

        public static ObservableCollection<Room> Rooms { get; set; }
        private const string MANDATORY_ROOM_ERROR_MESSAGE = "Broj prostorije je obavezno polje!";

        public RenoviranjaAddFirst()
        {
            var app = Application.Current as App;
            _roomController = app.roomController;

            Rooms = new ObservableCollection<Room>(_roomController.GetAll());

            InitializeComponent();

            DataContext = this;
        }

        private void addRenovationNext_Click(object sender, RoutedEventArgs e)
        {
            if (!isRoomSelected())
            {
                ShowError(MANDATORY_ROOM_ERROR_MESSAGE);
            }
            else
            {
                RoomItem roomItem = RoomConverter.ConvertRoomToRoomView(_roomController.GetFromID(((Room)BrojProstorije.SelectedItem).RoomNumber));
                MainWindow.AppWindow.navigateWithTitleChange(new RenoviranjaAddKalendar(roomItem), false);
                if (MainWindow.APP_HELP)
                {
                    MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                    MainWindow.AppWindow.HelpFrame.Navigate(new CalendarHelpRenoviranje());
                }
            }
        }
        private bool isRoomSelected()
        {
            return BrojProstorije.SelectedItem != null;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od zakazivanja", "Da li ste sigurni da zelite da odustanete od zakazivanja renoviranja?",
                                                     new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
        }
    }
}
