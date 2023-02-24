using Controller.RoomControllers;
using Controller.UserControllers;
using HealthCare;
using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProjekat.View.ConfirmationDialogsView
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ChangeEmployeeRoomDialog : Page
    {
        private readonly IRoomController _roomController;
        private readonly IUserController _userController;

        public static ObservableCollection<Room> Rooms { get; set; }
        private Room _oldRoom;
        public ChangeEmployeeRoomDialog(RoutedEventHandler potvrdiAkcija, Room oldRoom)
        {
            var app = Application.Current as App;
            _roomController = app.roomController;
            _userController = app.userController;

            _oldRoom = oldRoom;
            Rooms = new ObservableCollection<Room>(_roomController.GetAll());
            InitializeComponent();

            odustanak.AddHandler(Button.ClickEvent, potvrdiAkcija);
            DataContext = this;
        }

        private void potvrda_Click(object sender, RoutedEventArgs e)
        {
            if (isWorkRoomSelected())
            {
                List<User> doctors = _userController.GetDoctorFromWorkRoom(_oldRoom).ToList();
                foreach(User d in doctors)
                {
                    ((Doctor)d).WorkRoom = new Room() { RoomNumber = ((Room)BrojProstorije.SelectedItem).RoomNumber };
                    _userController.Update(((Doctor)d));
                }

                odustanak.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
        private bool isWorkRoomSelected()
        {
            return BrojProstorije.SelectedItem != null;
        }
    }
}
