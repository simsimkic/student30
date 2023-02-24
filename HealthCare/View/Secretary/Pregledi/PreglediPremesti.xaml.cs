using Controller.AppointmenController;
using Controller.CommunicationControllers;
using HealthCare;
using Model.Appointment;
using Model.Communication;
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

namespace Sekretar.Pregledi
{
    /// <summary>
    /// Interaction logic for PreglediPremesti.xaml
    /// </summary>
    public partial class PreglediPremesti : Window
    {
        public ObservableCollection<User> Collection { get; set; }
        private INotificationController notificationController;
        public User doctor;
        private PreglediPremesti()
        {
            InitializeComponent();
            var app = Application.Current as App;
            notificationController = app.notificationController;
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
        private static PreglediPremesti instance = null;
        public static PreglediPremesti Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PreglediPremesti();
                }
                return instance;
            }
        }

        private void ButtonClickOdustani(object sender, RoutedEventArgs e)
        {
            Pregledi pregledi = Pregledi.Instance;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da želite da odustanete?", Secretary.Properties.Langs.Lang.cancel, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                pregledi.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Visibility = Visibility.Collapsed;
                pregledi.ShowDialog();
            }
        }

        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (Kalendar.SelectedDates.Count == 0)
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.dateError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Kalendar.SelectedDate.Value.Month == DateTime.Now.Month - 1 ||
                Kalendar.SelectedDate.Value.Month == DateTime.Now.Month ||
                Kalendar.SelectedDate.Value.Month == (DateTime.Now.Month + 1) ||
                Kalendar.SelectedDate.Value.Month == (DateTime.Now.Month + 2))
            {
                if (Doktor.SelectedItem == null)
                    return;

                String datum = Kalendar.SelectedDate.Value.Date.ToShortDateString();
                PremestiPacijentaVreme premesti = PremestiPacijentaVreme.Instance;
                Pregledi pregledi = Pregledi.Instance;
                premesti.DatumNovi.Content = datum;
                premesti.Doktor.Content = Doktor.Text;
                premesti.jmbg.Content = JmbgStarei.Content.ToString();
                premesti.DatumNovi.Content = Kalendar.SelectedDate.Value.ToShortDateString();
                premesti.Pacijent.Content = Pacijent.Content;
                premesti.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                premesti.ResetComponents();
                premesti.ShowDialog();

            }
            else
            {
                MessageBox.Show("Ovaj mesec nije dostupan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Information);
            }


            
        }
    }
}
