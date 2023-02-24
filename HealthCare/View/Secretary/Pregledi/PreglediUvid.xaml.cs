using Controller.AppointmenController;
using Controller.CommunicationControllers;
using Controller.UserControllers;
using HealthCare;
using Model.Appointment;
using Model.Communication;
using Model.MedicalRecords;
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
    /// Interaction logic for PreglediUvid.xaml
    /// </summary>
    public partial class PreglediUvid : Window
    {
        public ObservableCollection<Appointment> preglediDan
        {
            get;
            set;
        }
        private IAppointmentController iAppointnmentController;
        private INotificationController notificationController;
        private IUserController userController;
        public Appointment appForPremesti;
        public Appointment appZaIzmeni;
        private PreglediUvid()
        {
            InitializeComponent();
            var app = Application.Current as App;
            userController = app.userController;
            iAppointnmentController = app.appointmentController;
            notificationController = app.notificationController;
            preglediDan = new ObservableCollection<Appointment>();
           
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        public void ResetComponents()
        {
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;

            preglediDan.Clear();
            User u;
            List<Appointment> appointments = iAppointnmentController.GetAppointmentForDoctorForDate(Pregledi.Instance.doctor ,Convert.ToDateTime(Datum.Content.ToString())).ToList();
            String[] imeDeo = Doktor.Content.ToString().Split(' ');
            foreach (Appointment a in appointments)
            {
                if (a.Type != 0)
                    continue;
                u = userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(a.Patient.Id));
                if (u == null)
                    continue;
                if(a.Patient.Name == null)
                    a.Patient.Name = u.Name;
                if (a.Patient.Surname == null)
                    a.Patient.Surname = u.Surname;
                a.Patient.JMBG = u.JMBG;
                preglediDan.Add(a);
            }
            this.DataContext = this;
        }
       
         private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static PreglediUvid instance = null;
        public static PreglediUvid Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PreglediUvid();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonClickZakazi(object sender, RoutedEventArgs e)
        {
            PreglediZakazi zakazi = PreglediZakazi.Instance;
            zakazi.Smena.Content = this.Smena.Content;
            zakazi.Datum.Content = this.Datum.Content;
            zakazi.Doktor.Content = this.Doktor.Content;
            zakazi.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Model.PacijentPregled dataRowView = (Model.PacijentPregled)((Button)e.Source).DataContext;
            //zakazi.Vreme.Content = dataRowView.Vreme;
            zakazi.ShowDialog();
        }

        private void ButtonClickPremesti(object sender, RoutedEventArgs e)
        {
            Pregledi pregledi = Pregledi.Instance;
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite da premestite ovog pacijenta?", "Odustani", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    return;
                }
                Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname;
                appForPremesti = (Appointment)((Button)e.Source).DataContext;
                PreglediPremesti premesti = PreglediPremesti.Instance;


                premesti.PacijentStarei.Content = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname;
                premesti.VremeLabela.Content = Convert.ToDateTime(dataRowView.StartDateTime).ToShortDateString();
                premesti.DoktorLabela.Content = dataRowView.Doctor.Name + " " + dataRowView.Doctor.Surname;
                premesti.DatumStari.Content = dataRowView.StartDateTime;
                premesti.JmbgStarei.Content = dataRowView.Patient.JMBG;


                premesti.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                premesti.Pacijent.Content= ProductName + "\n" + Datum.Content.ToString();
                this.Visibility = Visibility.Collapsed;
                pregledi.Visibility = Visibility.Collapsed;
                premesti.ResetComponents();
                premesti.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        private void ButtonClickIzmeni(object sender, RoutedEventArgs e)
        {
            Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
            String patientName = dataRowView.Patient.Name + " " + dataRowView.Patient.Surname; 
            String[] ImePrezime = patientName.Split(' ');
            appZaIzmeni = (Appointment)((Button)e.Source).DataContext;
            Console.WriteLine("Kliknuli ste na pacijenta sa id app " + appZaIzmeni.Id);
            PreglediIzmeni.Instance.Datum.Content = this.Datum.Content;
            PreglediIzmeni.Instance.Smena.Content = this.Smena.Content;
            PreglediIzmeni.Instance.Doktor.Content = this.Doktor.Content;
            PreglediIzmeni.Instance.Ime.Text = ImePrezime[0];
            PreglediIzmeni.Instance.Prezime.Text = ImePrezime[1];
            PreglediIzmeni.Instance.JMBG.Text = dataRowView.Patient.JMBG;
            PreglediIzmeni.Instance.Vreme.Content = dataRowView.StartDateTime;
            PreglediIzmeni.Instance.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            PreglediIzmeni.Instance.ShowDialog();
        }

        private void ButtonClickDelete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Ova akcija ce izbrisati pacijenta. Da li ste sigurni da zelite da nastavite?", Secretary.Properties.Langs.Lang.confirmation, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }

            Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
            iAppointnmentController.Delete(dataRowView);

            List<User> users = new List<User>();
            users.Add(dataRowView.Patient);
            var app = Application.Current as App;
            Notification n = new Notification()
            {
                createdBy = (Staff)app._currentUser,
                Date = DateTime.Now,
                Title = "Obavestenje o otkazivanju" +
                " pregleda.",
                Text = "Vas pregled za datum " + dataRowView.StartDateTime + " je otkazan.",
                users = users
            };
            notificationController.Create(n);

            this.ResetComponents();
        }


    }
}
