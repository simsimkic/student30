using Controller.MedicalRecordControllers;
using Controller.OtherDataController;
using Controller.RoomControllers;
using HealthCare;
using Model.DataFiltration;
using Model.MedicalRecords;
using Model.Rooms;
using Model.Users;
using Sekretar.Model;
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
    /// Interaction logic for PremestanjePacijenta.xaml
    /// </summary>
    public partial class PremestanjePacijenta : Window
    {
        public ObservableCollection<Room> CollectionRoom { get; set; }
        public ObservableCollection<Inventory> Inventory { get; set; }

        private IInventoryController inventoryController;
        private IRoomController roomController;
        private IRenovateController renovateController;
        public ObservableCollection<Sector> Sectors { get; set; }
        private ISectorController _sectorController;
        private Room room;
        private IRefferalController iRefferalController;
        private PremestanjePacijenta()
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
        private static PremestanjePacijenta instance = null;
        public static PremestanjePacijenta Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PremestanjePacijenta();
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
                if(ia.inventory.Name == null)
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

        public static IEnumerable<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            while (startDate <= endDate)
            {
                yield return startDate;
                startDate = startDate.AddDays(1);
            }
        }
        private void ButtonClickOdustani(object sender, RoutedEventArgs e)
        {
            String[] pacijentDeo = Pacijent.Content.ToString().Split('\n');

            Sale sale = Sale.Instance;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da želite da odustanete?", Secretary.Properties.Langs.Lang.cancel, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
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
            Sale sale = Sale.Instance;
            String[] pacijnet = Pacijent.Content.ToString().Split('\n');
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(pacijnet[0] + "\n" +
                                        Kalendar.SelectedDates.First().Date.ToShortDateString() + " - " +
                                        Kalendar.SelectedDates.Last().Date.ToShortDateString() + " ?", "Potvrdi", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Renovate renovate = renovateController.GetAll().ToList().SingleOrDefault( i => i.room.RoomNumber == room.RoomNumber);
                
                foreach(DateTime d in Kalendar.SelectedDates)
                    if (renovate != null)
                    {
                        if (d >= renovate.DateStart && d <= renovate.DateEnd)
                        {
                            MessageBox.Show("Soba se renovira u ovom periodu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                   
                Room sobica = (Room)roomController.GetAll().SingleOrDefault( i=>i.RoomNumber.Equals(UvidDanSoba.Instance.hospitalTreatmentZaPremestanje.Room.RoomNumber));
                foreach (Patient p in sobica.patients)
                {
                    if (p.Id.Equals(UvidDanSoba.Instance.hospitalTreatmentZaPremestanje.Patient.Id))
                    {
                        Console.WriteLine(p.Id + " ID svakog u  listi");
                        Console.WriteLine("Id naseg" + UvidDanSoba.Instance.hospitalTreatmentZaPremestanje.Patient.Id);
                        Console.WriteLine("BRISANJE USPESNO" + sobica.patients.Remove(p));
                        break;
                    }
                }
                
                roomController.Update(sobica);
                Console.WriteLine("ID sobice " + sobica.RoomNumber);
                Console.WriteLine("ID sobe " + room.RoomNumber);
                Room roomUpdateovana = roomController.GetFromID(room.RoomNumber);
                List<InventoryAmount> inventoryAmount = roomController.GetInventoryFromRoom(room).ToList();
                List<Inventory> inventory = inventoryController.GetAll().ToList();
                int brKreveta = 0;
                foreach (Inventory ia in inventory)
                {
                    if (ia.Name.Equals("Krevet"))
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
                            if (ht.Room.RoomNumber.Equals(room.RoomNumber) && (ht.Id != UvidDanSoba.Instance.hospitalTreatmentZaPremestanje.Id))
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
                Console.WriteLine("Broj kreveta u sobi" + brKreveta);
                Console.WriteLine("Broj pacijenata u sobi" + roomUpdateovana.patients.Count);
                /*
                if (brKreveta < roomUpdateovana.patients.Count + 1)
                {
                    MessageBox.Show("Nema dovoljno kreveta u sobi!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }*/

                roomUpdateovana.patients.Add(UvidDanSoba.Instance.hospitalTreatmentZaPremestanje.Patient);

                roomController.Update(roomUpdateovana);


                UvidDanSoba smestanje = UvidDanSoba.Instance;
                smestanje.hospitalTreatmentZaPremestanje.Date = Kalendar.SelectedDates.First().Date;
                smestanje.hospitalTreatmentZaPremestanje.DateTo = Kalendar.SelectedDates.Last().Date;
                smestanje.hospitalTreatmentZaPremestanje.Completed = true;
                smestanje.hospitalTreatmentZaPremestanje.Room = room;
                iRefferalController.Update(smestanje.hospitalTreatmentZaPremestanje);


                sale.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Visibility = Visibility.Collapsed;
                sale.ResetComponents();
                sale.ShowDialog();
            }
        }
    }
}
