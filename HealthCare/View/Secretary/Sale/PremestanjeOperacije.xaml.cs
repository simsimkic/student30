using Controller.OtherDataController;
using Controller.RoomControllers;
using HealthCare;
using Model.DataFiltration;
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
using System.Windows.Shapes;

namespace Sekretar.Sale
{
    /// <summary>
    /// Interaction logic for PremestanjeOperacije.xaml
    /// </summary>
    public partial class PremestanjeOperacije : Window
    {
        public Doctor doctor;
        public Room soba;
        public ObservableCollection<User> Collection { get; set; }
        public ObservableCollection<Room> CollectionRoom { get; set; }
        public ObservableCollection<WorkPlace> Workplace { get; set; }
        public ObservableCollection<Sector> Sectors { get; set; }
        private ISectorController _sectorController;
        private IWorkPlaceController workPlaceController;
        private IRenovateController renovateController;

        private PremestanjeOperacije()
        {
            InitializeComponent();
            var app = Application.Current as App;
            renovateController = app.renovateController;
            workPlaceController = app.workPlaceController;
            _sectorController = app.sectorController;
            StaffFilter staffFilter = new StaffFilter() { Id = -1, Name = "", Surname = "", StaffType = "Doktor" };
            Collection = new ObservableCollection<User>(app.userController.GetFilteredStaff(staffFilter));
            RoomFilter roomFilter = new RoomFilter()
            {
                RoomType = (int)Roomtype.Opperationroom,
                Floor = -1,
                RoomNumber = -1,
                Sector = null,
                RoomSizeFrom = -1,
                RoomSizeTo = -1
            };
            List<Room> rooms = app.roomController.GetFilteredRooms(roomFilter).ToList();
            CollectionRoom = new ObservableCollection<Room>(app.roomController.GetFilteredRooms(roomFilter));
            Workplace = new ObservableCollection<WorkPlace>(workPlaceController.GetAll());
            Sectors = new ObservableCollection<Sector>(_sectorController.GetAll());

            this.DataContext = this;
        }
        private static PremestanjeOperacije instance = null;
        public static PremestanjeOperacije Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PremestanjeOperacije();
                }
                return instance;
            }
        }
        public void ResetComponents()
        {
            var app = Application.Current as App;
            StaffFilter staffFilter = new StaffFilter() { Id = -1, Name = "", Surname = "", StaffType = "Doktor" };
            List<User> doctors = app.userController.GetFilteredStaff(staffFilter).ToList();
            Collection = new ObservableCollection<User>();
            Doctor d;
            foreach (User u in doctors)
            {
                d = (Doctor)u;
                //if (!d.WorkPlace.Name.Equals("Lekar opste prakse"))
                {
                    Collection.Add(u);
                }
            }
            this.DataContext = this;
        }

        private void SelectOption_SelectionChangedWorkPlace(object sender, SelectionChangedEventArgs e)
        {
            if (WorkPlace.SelectedItem == null) return;
            WorkPlace workPlace = (WorkPlace)WorkPlace.SelectedItem;

            var app = Application.Current as App;
            Doktor.IsEnabled = true;
            Collection.Clear();
            StaffFilter staffFilter = new StaffFilter() { Id = -1, Name = "", Surname = "", StaffType = "Doktor" };
            List<User> doctors = app.userController.GetFilteredStaff(staffFilter).ToList();
            foreach (Doctor d in doctors)
            {
                if (d.WorkPlace.Name.Equals(workPlace.Name))
                {
                    if (!Collection.Contains(d))
                    {
                        Collection.Add(d);
                        break;
                    }
                }

            }
            this.DataContext = this;

        }

        private void Soba_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Soba.SelectedItem == null) return;
            soba = (Room)Soba.SelectedItem;
        }

        private void SelectOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sektor.SelectedItem == null) return;
            Sector sector = (Sector)Sektor.SelectedItem;
            Soba.IsEnabled = true;

            var app = Application.Current as App;
            RoomFilter roomFilter = new RoomFilter()
            {
                RoomType = (int)Roomtype.Opperationroom,
                Floor = -1,
                RoomNumber = -1,
                Sector = null,
                RoomSizeFrom = -1,
                RoomSizeTo = -1
            };
            List<Room> rooms = app.roomController.GetFilteredRooms(roomFilter).ToList();
            Console.WriteLine("SOba ima" + rooms.Count);
            CollectionRoom.Clear();
            foreach (Room r in rooms)
            {
                if (r.sector.Name.Equals(sector.Name))
                    CollectionRoom.Add(r);
            }

            this.DataContext = this;

        }

        private void SelectOption_SelectionChangedDoctor(object sender, SelectionChangedEventArgs e)
        {
            if (Doktor.SelectedItem == null) return;
            doctor = (Doctor)Doktor.SelectedItem;
        }

        private void ButtonClickOdustani(object sender, RoutedEventArgs e)
        {
            String[] pacijentDeo = Pacijent.Content.ToString().Split('\n');

            Sale sale = Sale.Instance;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da želite da odustanete?", Secretary.Properties.Langs.Lang.cancel, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes) {
                sale.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Visibility = Visibility.Collapsed;
                sale.ShowDialog();
            }
        }

        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (Kalendar.SelectedDates.Count == 0)
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.dateError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            Renovate renovate = renovateController.GetAll().ToList().SingleOrDefault(i => i.room.RoomNumber == soba.RoomNumber);

            foreach (DateTime d in Kalendar.SelectedDates)
                if (renovate != null)
                {
                    if (d >= renovate.DateStart && d <= renovate.DateEnd)
                    {
                        MessageBox.Show("Soba se renovira u ovom periodu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }

            Sale sale = Sale.Instance;
          
            PremestiOperaciju premesti = PremestiOperaciju.Instance;
            premesti.Doktor.Content = Doktor.Text;
            premesti.Sektor.Content = Sektor.Text;
            premesti.Soba.Content = Soba.Text;
            premesti.Datum.Content = Kalendar.SelectedDate.Value.ToShortDateString();
            premesti.Pacijent.Content = this.Pacijent.Content;
            premesti.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            premesti.ResetComponents();
            premesti.ShowDialog();
        }
    }
}
