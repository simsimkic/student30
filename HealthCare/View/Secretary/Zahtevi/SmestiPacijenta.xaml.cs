using Controller.MedicalRecordControllers;
using Controller.OtherDataController;
using Controller.RoomControllers;
using HealthCare;
using Model.DataFiltration;
using Model.MedicalRecords;
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

namespace Sekretar.Zahtevi
{
    /// <summary>
    /// Interaction logic for SmestiSoba.xaml
    /// </summary>
    public partial class SmestiSoba : Window
    {
        public ObservableCollection<Room> CollectionRoom { get; set; }
        public ObservableCollection<Inventory> Inventory { get; set; }
        public ObservableCollection<Sector> Sectors { get; set; }
        private ISectorController _sectorController;
        private IInventoryController inventoryController;
        private IRoomController roomController;
        private Room room;
        private IRenovateController renovateController;
        private IRefferalController iRefferalController;
        private SmestiSoba()
        {
            InitializeComponent();
            var app = Application.Current as App;
            renovateController = app.renovateController;
            inventoryController = app.inventoryController;
            roomController = app.roomController;
            iRefferalController = app.refferalControler;
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
            Inventory = new ObservableCollection<Inventory>();
            this.DataContext = this;
        }
        private static SmestiSoba instance = null;
        public static SmestiSoba Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SmestiSoba();
                }
                return instance;
            }
        }

        private void SelectOption_SelectionRoomChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Soba.SelectedItem == null) return;
            room = (Room)Soba.SelectedItem;

            Inventory inv;
            Inventory.Clear();
            foreach (InventoryAmount ia in roomController.GetInventoryFromRoom(room).ToList())
            {
                inv = inventoryController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(ia.Inventory.Id));
                if (ia.inventory.Name == null)
                    ia.inventory.Name = inv.Name;
                Inventory.Add(ia.inventory);
            }
        }

        private void SelectOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sektor.SelectedItem == null) return;
            Sector sector = (Sector)Sektor.SelectedItem;
            Soba.IsEnabled = true;

            var app = Application.Current as App;
            RoomFilter roomFilter = new RoomFilter()
            {
                RoomType = (int)Roomtype.Ward,
                Floor = -1,
                RoomNumber = -1,
                Sector = null,
                RoomSizeFrom = -1,
                RoomSizeTo = -1
            };
            List<Room> rooms = app.roomController.GetFilteredRooms(roomFilter).ToList();
            CollectionRoom.Clear();
            foreach (Room r in rooms)
            {
                if (r.sector.Name.Equals(sector.Name))
                    CollectionRoom.Add(r);
            }
           
            this.DataContext = this;

        }

        public void ResetComponents()
        {
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
        private void ButtonClickOdustani(object sender, RoutedEventArgs e)
        {
            ZahteviZaSmestanje zahteviSmestanje = ZahteviZaSmestanje.Instance;
            zahteviSmestanje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            zahteviSmestanje.ShowDialog();
        }
        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (Kalendar.SelectedDates.Count == 0)
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.dateError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Kalendar.SelectedDate.Value.Month != DateTime.Now.Month - 1 &&
               Kalendar.SelectedDate.Value.Month != DateTime.Now.Month &&
               Kalendar.SelectedDate.Value.Month != (DateTime.Now.Month + 1) &&
               Kalendar.SelectedDate.Value.Month != (DateTime.Now.Month + 2))
            {
                MessageBox.Show("Mozete selektovati samo dva meseca unapred!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(Pacijent.Content.ToString() + "\n" +
                                        Kalendar.SelectedDates.First().Date.ToShortDateString() + " - " +
                                        Kalendar.SelectedDates.Last().Date.ToShortDateString() + " ?", "Potvrdi", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Renovate renovate = renovateController.GetAll().ToList().SingleOrDefault(i => i.room.RoomNumber == room.RoomNumber);

                foreach (DateTime d in Kalendar.SelectedDates)
                    if (renovate != null)
                    {
                        if (d >= renovate.DateStart && d <= renovate.DateEnd)
                        {
                            MessageBox.Show("Soba se renovira u ovom periodu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }

                List<InventoryAmount> inventoryAmount = roomController.GetInventoryFromRoom(room).ToList();
                List<Inventory> inventory = inventoryController.GetAll().ToList();
                int brKreveta = 0;
                foreach (Inventory ia in inventory)
                {
                    if(ia.Name.Equals("Krevet"))
                    {
                        brKreveta++;
                    }
                }
                HospitalTreatment ht;
                List<Refferal> referals = iRefferalController.GetAllRefferals().ToList();
                List<HospitalTreatment> hospitalTr = new List<HospitalTreatment>();
                foreach (Refferal r in referals)
                {
                    if (r.GetType().Equals(typeof(HospitalTreatment)))
                    {
                        ht = (HospitalTreatment)r;
                        if (ht.Completed)
                        {
                            if (ht.Room.RoomNumber.Equals(room.RoomNumber))
                                hospitalTr.Add(ht);
                        }
                    }
                }
                int brLjudiUSobi = 0;
                foreach (DateTime d in Kalendar.SelectedDates)
                {
                    foreach (HospitalTreatment h in hospitalTr)
                    {
                        if (d >= h.Date && d <= h.DateTo)
                        {
                            brLjudiUSobi++;
                        }
                    }
                    if (brKreveta < brLjudiUSobi + 1)
                    {
                        MessageBox.Show("Nema dovoljno kreveta u sobi!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    brLjudiUSobi = 0;
                }

                /*
                if (brKreveta < room.patients.Count + 1)
                {
                    MessageBox.Show("Nema dovoljno kreveta u sobi!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }*/

                room.patients.Add(ZahteviZaSmestanje.Instance.pacijentTreatment);

                roomController.Update(room);

                ZahteviZaSmestanje smestanje = ZahteviZaSmestanje.Instance;
                smestanje.hospitalTreatmentKojiSmestamo.Date = Kalendar.SelectedDates.First().Date;
                smestanje.hospitalTreatmentKojiSmestamo.DateTo = Kalendar.SelectedDates.Last().Date;
                smestanje.hospitalTreatmentKojiSmestamo.Completed = true;
                smestanje.hospitalTreatmentKojiSmestamo.Room = room;
                smestanje.hospitalTreatmentKojiSmestamo.Patient = ZahteviZaSmestanje.Instance.pacijentTreatment;
                iRefferalController.Update(smestanje.hospitalTreatmentKojiSmestamo);
                smestanje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                SmestiOperacija operacija = SmestiOperacija.Instance;

                this.Visibility = Visibility.Collapsed;
                operacija.Visibility = Visibility.Collapsed;
                smestanje.ResetComponents();
                smestanje.ShowDialog();
            }
        }
    }
}
