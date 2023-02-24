using Controller.AppointmenController;
using Controller.CommunicationControllers;
using HealthCare;
using Model.Appointment;
using Model.Communication;
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

namespace Sekretar.Pregledi
{
    /// <summary>
    /// Interaction logic for PremestiPacijentaVreme.xaml
    /// </summary>
    public partial class PremestiPacijentaVreme : Window
    {

        private INotificationController notificationController;
        private IAppointmentController iAppointnmentController;
        public ObservableCollection<Appointment> pregledi
        {
            get;
            set;
        }
        private PremestiPacijentaVreme()
        {
            InitializeComponent();
            this.DataContext = this;
            var app = Application.Current as App;
            notificationController = app.notificationController;
            iAppointnmentController = app.appointmentController;

            pregledi = new ObservableCollection<Appointment>();

            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        public void ResetComponents()
        {
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;

            pregledi.Clear();
            TreatmentType treatmentType = TreatmentType.Examination;
            List<Appointment> freeAppointments = iAppointnmentController.GetFreeAppointments(PreglediUvid.Instance.appForPremesti.Patient, Sekretar.Pregledi.PreglediPremesti.Instance.doctor, Convert.ToDateTime(Sekretar.Pregledi.PreglediPremesti.Instance.Kalendar.SelectedDate.Value.ToShortDateString()), treatmentType).ToList();
            foreach (Appointment a in freeAppointments)
            {
                pregledi.Add(a);
            }
            this.DataContext = this;
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static PremestiPacijentaVreme instance = null;
        public static PremestiPacijentaVreme Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PremestiPacijentaVreme();
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
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(Secretary.Properties.Langs.Lang.appointmentChooseDate, Secretary.Properties.Langs.Lang.confirmation, System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Pregledi pregled = Pregledi.Instance;
                PreglediPremesti premesti = PreglediPremesti.Instance;

                iAppointnmentController.Delete(PreglediUvid.Instance.appForPremesti);
                Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
                dataRowView.Doctor = (Doctor)premesti.doctor;
                dataRowView.Type = TreatmentType.Examination;
                dataRowView.Patient = PreglediUvid.Instance.appForPremesti.Patient;
                iAppointnmentController.Create(dataRowView);

                List<User> users = new List<User>();
                users.Add(dataRowView.Patient);
                users.Add(dataRowView.Doctor);
                var app = Application.Current as App;
                Notification n = new Notification()
                {
                    createdBy = (Staff)app._currentUser,
                    Date = DateTime.Now,
                    Title = "Obavestenje o premestanju" +
                    " pregleda.",
                    Text = "Vas pregled je premesten sa datuma " + PreglediUvid.Instance.appForPremesti.StartDateTime +  " na datum " + dataRowView.StartDateTime,
                    users = users
                };
                notificationController.Create(n);

                pregled.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Visibility = Visibility.Collapsed;
                premesti.Visibility = Visibility.Collapsed;
                pregled.ResetComponents();
                pregled.ShowDialog();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

