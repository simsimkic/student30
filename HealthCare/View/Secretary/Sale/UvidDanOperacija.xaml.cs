using Controller.AppointmenController;
using Controller.UserControllers;
using HealthCare;
using Model.Appointment;
using Model.MedicalRecords;
using Model.Users;
using Model.Users.UserBuilder;
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
    /// Interaction logic for UvidDan.xaml
    /// </summary>
    public partial class UvidDan : Window
    {
        public ObservableCollection<Appointment> operacije
        {
            get;
            set;
        }
        private IAppointmentController iAppointnmentController;
        public Appointment appForPremesti;
        private IUserController userController;
        private UvidDan()
        {
            InitializeComponent();
            this.DataContext = this;
            operacije = new ObservableCollection<Appointment>();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        public void ResetComponents()
        {
            var app = Application.Current as App;
            userController = app.userController;
            iAppointnmentController = app.appointmentController;
            User u;
            operacije.Clear();
            List<Appointment> appointments = iAppointnmentController.GetAppointmentForDate(Convert.ToDateTime(datum.Content.ToString())).ToList();

            foreach (Appointment a in appointments)
            {
                a.Patient = (Patient)userController.GetFromID(a.Patient.Id);
                a.Doctor = (Doctor)userController.GetFromID(a.Doctor.Id);
            }


            foreach (Appointment a in appointments)
            {
                if ((int)a.Type != 2)
                    continue;
                u = userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(a.Patient.Id));
                if (u == null)
                    continue;
                if (a.Patient.Name.Equals(""))
                    a.Patient.Name = u.Name;
                if (a.Patient.Surname.Equals(""))
                    a.Patient.Surname = u.Surname;
                operacije.Add(a);
            }
            this.DataContext = this;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static UvidDan instance = null;
        public static UvidDan Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UvidDan();
                }
                return instance;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }


        private void ButtonClickPremesti(object sender, RoutedEventArgs e)
        {
            Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
            Sale sale = Sale.Instance;
            appForPremesti = (Appointment)((Button)e.Source).DataContext;
            String pacijent = dataRowView.Patient.Name + "\n" + dataRowView.Patient.Surname;
            PremestanjeOperacije premestanje = PremestanjeOperacije.Instance;
            premestanje.Pacijent.Content = pacijent;
            premestanje.VremeLabela.Content = dataRowView.StartDateTime.ToString();

            premestanje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            Sale.Instance.Visibility = Visibility.Collapsed;
            premestanje.ResetComponents();
            premestanje.ShowDialog();
        }

    }
}
