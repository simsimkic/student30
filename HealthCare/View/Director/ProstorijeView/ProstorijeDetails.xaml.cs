using Controller.RoomControllers;
using HCIProjekat.ProstorijeView;
using HCIProjekat.View.ProstorijeView.DataView;
using HealthCare;
using Model.Rooms;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.ProstorijeView
{
    /// <summary>
    /// Interaction logic for ProstorijeDetails.xaml
    /// </summary>
    public partial class ProstorijeDetails : Page
    {
        public Room room { get; set; }

        private UserControl _room;
        private IRoomController _roomController;

        public ProstorijeDetails(UserControl Room)
        {
            this._room = Room;
            var app = Application.Current as App;
            _roomController = app.roomController;
            this.room = _roomController.GetFromID(((RoomItem)Room).RoomNumber);

            InitializeComponent();

            DataContext = this;
        }

        private void editRoom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ProstorijeEdit(_room), false);
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
        }
    }
}
