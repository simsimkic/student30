using Controller.OtherDataController;
using Controller.RoomControllers;
using HealthCare;
using Model.DataFiltration;
using Model.Rooms;
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

namespace HCIProjekat.RenoviranjaView
{
    /// <summary>
    /// Interaction logic for RenoviranjaFilter.xaml
    /// </summary>
    public partial class RenoviranjaFilter : Page
    {
        private readonly IRoomController _roomController;
        private ISectorController _sectorController;

        public static ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Sector> Sectors { get; set; }
        public RenoviranjaFilter()
        {
            var app = Application.Current as App;
            _sectorController = app.sectorController;
            _roomController = app.roomController;

            Rooms = new ObservableCollection<Room>(_roomController.GetAll());
            Sectors = new ObservableCollection<Sector>(_sectorController.GetAll());

            InitializeComponent();
            DataContext = this;
        }
        private bool isDateToFulfilled()
        {
            return dateTo.SelectedDate != null;
        }

        private bool isDateFromFulfilled()
        {
            return dateFrom.SelectedDate != null;
        }
        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (anithingFulfilled())
            {

                MainWindow.AppWindow.navigateToRootPage(new Renoviranja(false, createFilter()), false);

            }
            else
            {
                MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
            }
        }

        private RenovateFilter createFilter()
        {
            DateTime start;
            if (!isDateFromFulfilled())
                start = DateTime.MinValue.Date;
            else
                start = dateFrom.SelectedDate.Value.Date;

            DateTime end;
            if (!isDateToFulfilled())
                end = DateTime.MinValue.Date;
            else
                end = dateTo.SelectedDate.Value.Date;

            int prostorija = -1;
            if (BrojProstorije.SelectedItem != null)
                prostorija = ((Room)BrojProstorije.SelectedItem).RoomNumber;

            int roomtype = -1;
            if (roomType.SelectedItem != null)
            {
                if (Roomtype.Examinationroom == (Roomtype)roomType.SelectedItem)
                    roomtype = 2;
                else if (Roomtype.Ward == (Roomtype)roomType.SelectedItem)
                    roomtype = 0;
                else
                    roomtype = 1;

            }

            return new RenovateFilter() { StartDate = start, EndDate = end, RoomNumber = prostorija, RoomType = roomtype, Sector = (Sector)sector.SelectedItem };
        }

        private bool anithingFulfilled()
        {
            return isDateFromFulfilled() || isDateToFulfilled() || BrojProstorije.SelectedItem != null || sector.SelectedItem != null || roomType.SelectedItem != null;
        }
    }
}
