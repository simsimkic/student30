using Controller.AppointmenController;
using Model.Appointment;
using Model.MedicalRecords;
using Sekretar.Pregledi;
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

namespace HealthCare.View.Secretary.Pregledi
{
    /// <summary>
    /// Interaction logic for PreglediZakaziFree.xaml
    /// </summary>
    public partial class PreglediZakaziFree : Window
    {
        public ObservableCollection<Appointment> preglediDan
        {
            get;
            set;
        }
        private IAppointmentController iAppointnmentController;
        private PreglediZakaziFree()
        {
            InitializeComponent();
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;

            preglediDan = new ObservableCollection<Appointment>();

            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static PreglediZakaziFree instance = null;
        public static PreglediZakaziFree Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PreglediZakaziFree();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        public void ResetComponentsForFreeApp()
        {
            preglediDan.Clear();
            TreatmentType treatmentType = TreatmentType.Examination;
            List<Appointment> freeAppointments = iAppointnmentController.GetFreeAppointments(PreglediZakazi.Instance.p, Sekretar.Pregledi.Pregledi.Instance.doctor, Convert.ToDateTime(Sekretar.Pregledi.Pregledi.Instance.Kalendar.SelectedDate.Value.ToShortDateString()), treatmentType).ToList();
            foreach (Appointment a in freeAppointments)
            {
                preglediDan.Add(a);
            }
        }


        private void ButtonClickZakazi(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Da li ste sigurni da zelite ovaj termin?", "Odustani", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }

            Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
            dataRowView.Type = TreatmentType.Examination;
            dataRowView.Doctor = (global::Model.Users.Doctor)Sekretar.Pregledi.Pregledi.Instance.doctor;
            dataRowView.Patient = PreglediZakazi.Instance.p;
            iAppointnmentController.Create(dataRowView);
            this.Visibility = Visibility.Collapsed;
        }
    }
}
