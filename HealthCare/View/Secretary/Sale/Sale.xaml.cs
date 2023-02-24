using Controller.OtherDataController;
using HealthCare;
using HealthCare.View.Secretary.Model;
using Model.DataFiltration;
using Model.Rooms;
using Model.Users;
using Sekretar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale : Window
    {
        public ObservableCollection<Sector> Sectors { get; set; }

        public ObservableCollection<User> Collection { get; set; }
        public ObservableCollection<Room> CollectionRoom { get; set; }
        private ISectorController _sectorController;
        public Room room;
        private Sale()
        {
            InitializeComponent();
            var app = Application.Current as App;
            _sectorController = app.sectorController;
            RoomFilter roomFilter = new RoomFilter()
            {
                RoomType = (int)Roomtype.Ward,
                Floor = -1,
                RoomNumber = -1,
                Sector = null,
                RoomSizeFrom = -1,
                RoomSizeTo = -1
            };
            CollectionRoom = new ObservableCollection<Room>(app.roomController.GetFilteredRooms(roomFilter));
            Sectors = new ObservableCollection<Sector>(_sectorController.GetAll());

            this.DataContext = this;
        }
        public void ResetComponents()
        {
            var app = Application.Current as App;
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
            CollectionRoom = new ObservableCollection<Room>(app.roomController.GetFilteredRooms(roomFilter));
            Sectors = new ObservableCollection<Sector>(_sectorController.GetAll());

            this.DataContext = this;
        }

        private void SelectOption_SelectionRoomChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Soba.SelectedItem == null) return;
            room = (Room)Soba.SelectedItem;
        }

        private static Sale instance = null;
        public static Sale Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Sale();
                }
                return instance;
            }
        }

        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Application.Current.MainWindow.ShowDialog();
        }
        private void ButtonClickPrikazi(object sender, RoutedEventArgs e)
        {
            if (Kalendar.SelectedDates.Count == 0)
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.dateError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            UvidDan uvidOperacija;
            UvidDanSoba uvidSoba;
            if (VrstaSale.SelectedIndex == 0)
            {
                
                uvidOperacija = UvidDan.Instance;
                uvidOperacija.sektor.Content = Sektor.Text;
                uvidOperacija.sala.Content = Soba.Text;
                uvidOperacija.datum.Content = Kalendar.SelectedDate.Value.ToShortDateString();
                uvidOperacija.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                uvidOperacija.ResetComponents();
                uvidOperacija.ShowDialog();
            }
            else
            {
                if (Sektor.SelectedItem == null || Soba.SelectedItem == null)
                {
                    MessageBox.Show("Morate izabrati sektor i sobu!", Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                uvidSoba = UvidDanSoba.Instance;
                uvidSoba.sektor.Content = Sektor.Text;
                uvidSoba.Sala.Content = Soba.Text;
                uvidSoba.DATUM.Content = Kalendar.SelectedDate.Value.ToShortDateString();
                uvidSoba.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                uvidSoba.ResetComponents();
                uvidSoba.ShowDialog();
            }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChangedSector(object sender, SelectionChangedEventArgs e)
        {
            if (Sektor.SelectedItem == null) return;
            Sector sector = (Sector)Sektor.SelectedItem;

            var app = Application.Current as App;
            RoomFilter roomFilter;
            if (VrstaSale.SelectedIndex == 0)
            {

                Soba.IsEnabled = false;
                roomFilter = new RoomFilter()
                {
                    RoomType = (int)Roomtype.Opperationroom,
                    Floor = -1,
                    RoomNumber = -1,
                    Sector = null,
                    RoomSizeFrom = -1,
                    RoomSizeTo = -1
                };
            }
            else
            {
                Soba.IsEnabled = true;
                roomFilter = new RoomFilter()
                {
                    RoomType = (int)Roomtype.Ward,
                    Floor = -1,
                    RoomNumber = -1,
                    Sector = null,
                    RoomSizeFrom = -1,
                    RoomSizeTo = -1
                };
            }
            List<Room> rooms = app.roomController.GetFilteredRooms(roomFilter).ToList();
            CollectionRoom.Clear();
            foreach (Room r in rooms)
            {
                if (r.sector.Name.Equals(sector.Name))
                    CollectionRoom.Add(r);
            }

            this.DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var app = Application.Current as App;
            if (VrstaSale.SelectedIndex == 0)
            {

                Sektor.IsEnabled = false;
                Soba.IsEnabled = false;
                RoomFilter roomFilter = new RoomFilter()
                {
                    RoomType = (int)Roomtype.Opperationroom,
                    Floor = -1,
                    RoomNumber = -1,
                    Sector = null,
                    RoomSizeFrom = -1,
                    RoomSizeTo = -1
                };
                Sectors.Clear();
                List<Sector> sectors = _sectorController.GetAll().ToList();
                foreach (Sector s in sectors)
                {
                    Sectors.Add(s);
                }
                this.DataContext = this;
            }
            else
            {

                Sektor.IsEnabled = true;
                RoomFilter roomFilter = new RoomFilter()
                {
                    RoomType = (int)Roomtype.Ward,
                    Floor = -1,
                    RoomNumber = -1,
                    Sector = null,
                    RoomSizeFrom = -1,
                    RoomSizeTo = -1
                };
                Sectors.Clear();
                List<Sector> sectors = _sectorController.GetAll().ToList();
                foreach (Sector s in sectors)
                {
                    Sectors.Add(s);
                }
                this.DataContext = this;
            }
        }
    }
}
