using Controller.AppointmenController;
using Controller.MedicalRecordControllers;
using Controller.RoomControllers;
using HealthCare;
using HealthCare.View.Secretary.Model;
using Model.Appointment;
using Model.MedicalRecords;
using Model.Rooms;
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

namespace Sekretar.Zahtevi
{
    /// <summary>
    /// Interaction logic for ZakaziOperacijuVreme.xaml
    /// </summary>
    public partial class ZakaziOperacijuVreme : Window
    {
        Regex rgx = new Regex(@"^([0-1]?[0-9]|2[0-3])$");
        Regex rgxm = new Regex(@"^([0-5][0-9])$");
        private IRefferalController iRefferalController;
        private IAppointmentController iAppointnmentController;
        private IRoomController roomController;
        public ObservableCollection<Appointment> operacije
        {
            get;
            set;
        }

        private ZakaziOperacijuVreme()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            var app = Application.Current as App;
            roomController = app.roomController;
            operacije = new ObservableCollection<Appointment>();
            iRefferalController = app.refferalControler;
        }

        public void ResetComponents()
        { 
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;

            operacije.Clear();
            TreatmentType treatmentType = TreatmentType.Surgery;
            List<Appointment> freeAppointments = iAppointnmentController.GetFreeAppointments(ZahteviZaSmestanje.Instance.pacijentSurgery, SmestiOperacija.Instance.doctor, Convert.ToDateTime(SmestiOperacija.Instance.Kalendar.SelectedDate.Value.ToShortDateString()), treatmentType).ToList();
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
        private static ZakaziOperacijuVreme instance = null;
        public static ZakaziOperacijuVreme Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZakaziOperacijuVreme();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        
        private void ButtonClickPremesti(object sender, RoutedEventArgs e)
        {
            ZahteviZaSmestanje smestanje = ZahteviZaSmestanje.Instance;
            Appointment dataRowView = (Appointment)((Button)e.Source).DataContext;
            dataRowView.Doctor = (Doctor)SmestiOperacija.Instance.doctor;
            dataRowView.Type = TreatmentType.Surgery;
            dataRowView.Room = SmestiOperacija.Instance.soba;
            dataRowView.Patient = ZahteviZaSmestanje.Instance.pacijentSurgery;
            iAppointnmentController.Create(dataRowView);
            
            smestanje.urgentSurgeryKojiSmestamo.Approved = true;
            smestanje.urgentSurgeryKojiSmestamo.Doctor = (Doctor)SmestiOperacija.Instance.doctor;
            iRefferalController.Update(smestanje.urgentSurgeryKojiSmestamo);

            smestanje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SmestiOperacija operacija = SmestiOperacija.Instance;
            this.Visibility = Visibility.Collapsed;
            operacija.Visibility = Visibility.Collapsed;
            smestanje.ResetComponents();
            smestanje.ShowDialog();
        }

        private void Sati_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
