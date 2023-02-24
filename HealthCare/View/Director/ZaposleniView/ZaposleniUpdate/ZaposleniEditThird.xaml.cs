using Controller.OtherDataController;
using Controller.RoomControllers;
using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAddThird.xaml
    /// </summary>
    public partial class ZaposleniEditThird : Page
    {
        private const string MANDATORY_WORKPLACE_FIELD = "Radno mesto je obavezno polje!";
        private const string MANDATORY_WORKROOM_FIELD = "Dodeljena prostorija je obavezno polje!";

        private List<Specialization> _selectedSpecializations;
        private readonly IUserController _userController;
        private readonly ISpecializationController _specializationController;
        private readonly IWorkPlaceController _workPlaceController;
        private readonly IRoomController _roomController;
        private Doctor Staff;

        public ObservableCollection<Specialization> Specializations { get; set; }
        public ObservableCollection<WorkPlace> WorkPlaces { get; set; }
        public static ObservableCollection<Room> Rooms { get; set; }
        public ZaposleniEditThird(Doctor staff)
        {
            Staff = staff;

            var app = Application.Current as App;
            _specializationController = app.specializationController;
            _workPlaceController = app.workPlaceController;
            _roomController = app.roomController;
            _userController = app.userController;

            _selectedSpecializations = new List<Specialization>();
            Specializations = new ObservableCollection<Specialization>(_specializationController.GetAll());
            WorkPlaces = new ObservableCollection<WorkPlace>(_workPlaceController.GetAll());
            Rooms = new ObservableCollection<Room>(_roomController.GetAll());


            loadSpecializations();
            InitializeComponent();
            DataContext = this;

            radnoMesto.SelectedValue = Staff.WorkPlace.Name;
            BrojProstorije.SelectedValue = Staff.WorkRoom.RoomNumber;
        }

        private void loadSpecializations()
        {
            foreach (Specialization s in Staff.Specialization)
                _selectedSpecializations.Add(s);
        }
        private bool specializationExist(string name)
        {
            foreach (Specialization s in _selectedSpecializations)
                if (s.Name.Equals(name))
                {
                    return true;
                }
            return false;
        }
        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene informacija zaposlenog?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEditingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void editDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!isWorkPlaceSelected())
            {
                ShowError(MANDATORY_WORKPLACE_FIELD);
            }
            else if (!isWorkRoomSelected())
            {
                ShowError(MANDATORY_WORKROOM_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Izmena informacija zaposlenog", "Da li ste sigurni da zelite da izmenite informacije zaposlenom?",
                                                               new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmEditingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }

        private bool isWorkPlaceSelected()
        {
            return radnoMesto.SelectedItem != null;
        }

        private bool isWorkRoomSelected()
        {
            return BrojProstorije.SelectedItem != null;
        }

        private void confirmEditingEvent(object sender, RoutedEventArgs e)
        {
            fulfillDoctor();
            _userController.Update(Staff);
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void fulfillDoctor()
        {
            Staff.UserType = Usertype.Doctor;
            Staff.Biography = "";
            Staff.WorkRoom = new Room() { RoomNumber = ((Room)BrojProstorije.SelectedItem).RoomNumber };
            Staff.Specialization = _selectedSpecializations;
            Staff.WorkPlace = (WorkPlace)radnoMesto.SelectedItem;
        }

        private void specialization_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (!specializationExist((string)checkBox.Content))
                _selectedSpecializations.Add(new Specialization() { Name = (string)checkBox.Content });
        }

        private void specialization_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            foreach (var v in _selectedSpecializations)
                if (v.Name.Equals((string)checkBox.Content))
                {
                    _selectedSpecializations.Remove(v);
                    break;
                }
        }

        private void specialization_Loaded(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            foreach (var s in _selectedSpecializations)
            {
                if (((string)cb.Content).Equals(s.Name))
                {
                    cb.IsChecked = true;
                    break;
                }
            }
        }
    }
}
