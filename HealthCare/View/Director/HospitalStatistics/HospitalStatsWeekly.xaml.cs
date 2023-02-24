using Controller.AppointmenController;
using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.HelpWizardView;
using HCIProjekat.View.ZaposleniView.DataView;
using HCIProjekat.ZaposleniView;
using HealthCare;
using Model.Appointment;
using Model.MedicalRecords;
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

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class HospitalStatisticsWeekly : Page
    {
        private IUserController userController;
        private IAppointmentController appointmentController;

        public int BrojZaposlenih { get; set; }
        public int BrojPacijenata { get; set; }
        public int BrojPregleda { get; set; }
        public int BrojOperacija { get; set; }
        public HospitalStatisticsWeekly()
        {
            var app = Application.Current as App;
            userController = app.userController;
            appointmentController = app.appointmentController;

            BrojZaposlenih = userController.GetAllStaff().Count();
            List<Appointment> appointments = appointmentController.GetAll().Where(x => x.StartDateTime.Date >= DateTime.Now.Date.AddDays(-7) && x.EndDateTime.Date <= DateTime.Now.Date).ToList();
            BrojPacijenata = appointments.Count();
            BrojPregleda = appointments.Where(x => (int)x.Type == (int)TreatmentType.Examination || (int)x.Type == (int)TreatmentType.Specialistexamination).Count();
            BrojOperacija = appointments.Where(x => (int)x.Type == (int)TreatmentType.Surgery).Count();

            InitializeComponent();

            DataContext = this;
        }

        private void mesecno_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new HospitalStatisticsMonthy(), false);
        }

        private void godisnje_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new HospitalStatisticsForYear(), false);
        }
    }
}
