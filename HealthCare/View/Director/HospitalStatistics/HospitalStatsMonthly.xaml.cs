using Controller.AppointmenController;
using Controller.UserControllers;
using HealthCare;
using Model.Appointment;
using Model.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class HospitalStatisticsMonthy : Page
    {
        private IUserController userController;
        private IAppointmentController appointmentController;

        public int BrojZaposlenih { get; set; }
        public int BrojPacijenata { get; set; }
        public int BrojPregleda { get; set; }
        public int BrojOperacija { get; set; }
        public HospitalStatisticsMonthy()
        {
            var app = Application.Current as App;
            userController = app.userController;
            appointmentController = app.appointmentController;

            BrojZaposlenih = userController.GetAllStaff().Count();
            List<Appointment> appointments = appointmentController.GetAll().Where(x => x.StartDateTime.Date >= DateTime.Now.Date.AddMonths(-1) && x.EndDateTime.Date <= DateTime.Now.Date).ToList();
            BrojPacijenata = appointments.Count();
            BrojPregleda = appointments.Where(x => (int)x.Type == (int)TreatmentType.Examination || (int)x.Type == (int)TreatmentType.Specialistexamination).Count();
            BrojOperacija = appointments.Where(x => (int)x.Type == (int)TreatmentType.Surgery).Count();

            InitializeComponent();

            DataContext = this;
        }
        private void nedeljno_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new HospitalStatisticsWeekly(), false);
        }

        private void godisnje_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new HospitalStatisticsForYear(), false);
        }
    }
}
