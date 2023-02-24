using HealthCare;
using Model.Appointment;
using Model.DataFiltration;
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
using System.Windows.Shapes;

namespace Sekretar.Pregledi
{
    /// <summary>
    /// Interaction logic for Pregledi.xaml
    /// </summary>
    public partial class Pregledi : Window
    {
        public ObservableCollection<User> Collection { get; set; }


        public User doctor;
        private Pregledi()
        {
            InitializeComponent();
            var app = Application.Current as App;
            StaffFilter staffFilter = new StaffFilter() { Id = -1, Name = "", Surname = "", StaffType = "Doktor" };
            List<User> doctors = app.userController.GetFilteredStaff(staffFilter).ToList();
            Collection = new ObservableCollection<User>();
            Doctor d;
            foreach (User u in doctors)
            {
                d = (Doctor)u;
                if (d.WorkPlace.Name.Equals("Lekar opste prakse"))
                {
                    Collection.Add(u);
                }
            }
            this.DataContext = this;
        }

        private static Pregledi instance = null;
        public static Pregledi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Pregledi();
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
                if (d.WorkPlace.Name.Equals("Lekar opste prakse"))
                {
                    Collection.Add(u);
                }
            }
            this.DataContext = this;
        }
        private void selectOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Doktor.SelectedItem == null) return;
            doctor = (Doctor)Doktor.SelectedItem;
            Console.WriteLine("Doktor " + doctor.Name + " id " + doctor.Id);
        }

        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Application.Current.MainWindow.ShowDialog();
        }
        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (Kalendar.SelectedDates.Count == 0)
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.dateError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Kalendar.SelectedDate.Value.Month == DateTime.Now.Month -1 ||
                Kalendar.SelectedDate.Value.Month == DateTime.Now.Month ||
                Kalendar.SelectedDate.Value.Month == (DateTime.Now.Month + 1) ||
                Kalendar.SelectedDate.Value.Month == (DateTime.Now.Month + 2))
            {
                if (Doktor.SelectedIndex == -1)
                    return;
                String datum = Kalendar.SelectedDate.Value.Date.ToShortDateString();
                PreglediUvid preglediUvid = PreglediUvid.Instance;
        
                preglediUvid.Datum.Content = datum;
                preglediUvid.Doktor.Content = doctor.Name + " " + doctor.Surname;
                preglediUvid.ResetComponents();
               
                preglediUvid.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                preglediUvid.ShowDialog();

            }
            else { 
                MessageBox.Show("Ovaj mesec nije dostupan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        
        }
    }
}
