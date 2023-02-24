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

namespace HealthCare.View.Patient
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    /// 

    public class Chart
    {
        public string naslov { get; set; }
        public int procenat { get; set; }
    }

    public class UserGrowth
    {
        public String Date { get; set; }
        public int NumberOfUsers { get; set; }
    }

    public partial class HomePage : Page
    {

        public ObservableCollection<string> ImagePathList { get; set; }
        public ObservableCollection<Chart> ChartList { get; set; } 

        public List<UserGrowth> UserGrowthList { get; set; }
        public HomePage()
        {
            UserGrowthList = new List<UserGrowth>();
            UserGrowthList.Add(new UserGrowth() { Date = "januar", NumberOfUsers = 50 });
            UserGrowthList.Add(new UserGrowth() { Date = "februar", NumberOfUsers = 80 });
            UserGrowthList.Add(new UserGrowth() { Date = "mart", NumberOfUsers = 120 });
            UserGrowthList.Add(new UserGrowth() { Date = "april", NumberOfUsers = 140 });
            UserGrowthList.Add(new UserGrowth() { Date = "maj", NumberOfUsers = 200 });
            UserGrowthList.Add(new UserGrowth() { Date = "jun", NumberOfUsers = 250 });
            ChartList = new ObservableCollection<Chart>();
            Chart chart = new Chart() { naslov = "Trenutno je zakazano 456 pregleda, \n 84% je zakazano putem naše aplikacije", procenat = 84 };
            ChartList.Add(chart);
            ImagePathList = new ObservableCollection<string>();
            ImagePathList.Add(@"Resources/homepagegallery1.jpg");
            ImagePathList.Add(@"Resources/homepagegallery3.jpg");
            ImagePathList.Add(@"Resources/homepagegallery4.jpg");
            ImagePathList.Add(@"Resources/homepagegallery5.jpg");
            ImagePathList.Add(@"Resources/homepagegallery6.jpg");
            InitializeComponent();
            this.DataContext = this;
        }

        private void myMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel -= 0.2;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel += 0.2;
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if(app._currentUser == null)
            {
                return;
            }
            this.NavigationService.Navigate(new Uri(@"/View/Patient/MakeAppointment.xaml", UriKind.Relative));
            //scrollViewer.ScrollToTop();
        }
    }
}
