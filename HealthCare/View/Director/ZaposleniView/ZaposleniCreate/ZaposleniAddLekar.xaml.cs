using Controller.OtherDataController;
using Controller.RoomControllers;
using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Rooms;
using Model.Users;
using Model.Users.UserBuilder;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAddLekar.xaml
    /// </summary>
    public partial class ZaposleniAddLekar : Page
    {
        private const string MANDATORY_WORKPLACE_FIELD = "Radno mesto je obavezno polje!";
        private const string MANDATORY_WORKROOM_FIELD = "Dodeljena prostorija je obavezno polje!";

        private List<Specialization> _selectedSpecializations;
        private readonly ISpecializationController _specializationController;
        private readonly IWorkPlaceController _workPlaceController;
        private readonly IRoomController _roomController;
        private readonly IUserController _userController;

        public ObservableCollection<Specialization> Specializations { get; set; }
        public ObservableCollection<WorkPlace> WorkPlaces { get; set; }
        public static ObservableCollection<Room> Rooms { get; set; }
        private DoctorBuilder doctorBuilder;

        public ZaposleniAddLekar(DoctorBuilder doctorBuilder)
        {
            _selectedSpecializations = new List<Specialization>();
            var app = Application.Current as App;
            _specializationController = app.specializationController;
            _workPlaceController = app.workPlaceController;
            _roomController = app.roomController;
            _userController = app.userController;

            Specializations = new ObservableCollection<Specialization>(_specializationController.GetAll());
            WorkPlaces = new ObservableCollection<WorkPlace>(_workPlaceController.GetAll());
            Rooms = new ObservableCollection<Room>(_roomController.GetAll());

            this.doctorBuilder = doctorBuilder;
            InitializeComponent();

            DataContext = this;

        }

        private void addDoctor_Click(object sender, RoutedEventArgs e)
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
                dialog.Children.Add(new ConfirmDialog("Dodavanje zaposlenog", "Da li ste sigurni da zelite da dodate zaposlenog?",
                                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
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

        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            fulfillDoctor();
            if (_userController.Create(doctorBuilder.getResult()) == null)
            {
                ShowError("Uneti JMBG nije jedinstven! Vec postoji u sistemu!");
            }
            else
            {
                doctorBuilder.Reset();
                MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
            }
        }

        private void fulfillDoctor()
        {
            doctorBuilder.SetWorkRoom(new Room() { RoomNumber = ((Room)BrojProstorije.SelectedItem).RoomNumber });
            doctorBuilder.SetSpecialization(_selectedSpecializations);
            doctorBuilder.SetWorkPlace((WorkPlace)radnoMesto.SelectedItem);
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od dodavanja", "Da li ste sigurni da zelite da odustanete od dodavanja zaposlenog?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void specialization_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
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
    }
}
