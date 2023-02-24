using Controller.AppointmenController;
using HealthCare;
using Model.Appointment;
using Model.MedicalRecords;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PremestiOperaciju.xaml
    /// </summary>
    public partial class PremestiOperaciju : Window
    {
        Regex rgx = new Regex(@"^([0-1]?[0-9]|2[0-3])$");
        Regex rgxm = new Regex(@"^([0-5][0-9])$");

        public ObservableCollection<Appointment> operacije
        {
            get;
            set;
        }
        private IAppointmentController iAppointnmentController;

        private PremestiOperaciju()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            operacije = new ObservableCollection<Appointment>();
        }

        public void ResetComponents()
        {
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;

            operacije.Clear();
            TreatmentType treatmentType = TreatmentType.Surgery;
            List<Appointment> freeAppointments = iAppointnmentController.GetFreeAppointments(UvidDan.Instance.appForPremesti.Patient, PremestanjeOperacije.Instance.doctor, Convert.ToDateTime(PremestanjeOperacije.Instance.Kalendar.SelectedDate.Value.ToShortDateString()), treatmentType).ToList();
            foreach (Appointment a in freeAppointments)
            {
                operacije.Add(a);
            }
            this.DataContext = this;
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static PremestiOperaciju instance = null;
        public static PremestiOperaciju Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PremestiOperaciju();
                }
                return instance;
            }
        }


        private void ButtonClickPremesti(object sender, RoutedEventArgs e)
        {
            Sale sale = Sale.Instance;
            iAppointnmentController.Delete(UvidDan.Instance.appForPremesti);
            Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
            dataRowView.Doctor = (Doctor)PremestanjeOperacije.Instance.doctor;
            dataRowView.Type = TreatmentType.Surgery;
            dataRowView.Room = PremestanjeOperacije.Instance.soba;
            dataRowView.Patient = UvidDan.Instance.appForPremesti.Patient;
            iAppointnmentController.Create(dataRowView);

            sale.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            PremestanjeOperacije.Instance.Visibility = Visibility.Collapsed;
            sale.ResetComponents();
            sale.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
