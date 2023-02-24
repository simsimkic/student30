using Controller.AppointmenController;
using Controller.MedicalRecordControllers;
using Controller.RoomControllers;
using Controller.UserControllers;
using HealthCare;
using Model.Appointment;
using Model.MedicalRecords;
using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HealthCareClinic.View
{
    /// <summary>
    /// Interaction logic for TreatmentViewPage.xaml
    /// </summary>
    public partial class TreatmentViewPage : Page, INotifyPropertyChanged
    {
        private readonly IAppointmentController _appointmentController;
        private readonly IUserController _userController;
        private readonly IRoomController _roomController;
        private readonly IRecordController _recordController;

        private List<Appointment> _appointments = new List<Appointment>();
        private List<TreatmentReport> _treatment = new List<TreatmentReport>();
        private List<Appointment> _selectedAppointent = new List<Appointment>();
        public TreatmentViewPage()
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleKeyboard);
            this.dpTreatmentView.PreviewKeyDown += new KeyEventHandler(HandleDP);

            var app = Application.Current as App;
            _recordController = app.medicalRecordController;

            /* TreatmentReport newTrr = new TreatmentReport() { PatientId=5, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr2 = new TreatmentReport() { PatientId=6, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr3 = new TreatmentReport() { PatientId=7, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr4 = new TreatmentReport() { PatientId = 8, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr5 = new TreatmentReport() { PatientId = 9, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr6 = new TreatmentReport() { PatientId = 10, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr7 = new TreatmentReport() { PatientId = 11, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             TreatmentReport newTrr8 = new TreatmentReport() { PatientId = 12, Anamnesis = "Pacijent se zali na ..", Id = 125322, Diagnosis = "Dijagnoza je ta i ta", Note = "kontrola za 2 nedelje", Theraphy = "dovicin 2x8h" };
             _treatment.Add(newTrr);
             _treatment.Add(newTrr2);
             _treatment.Add(newTrr3);
             _treatment.Add(newTrr4);
             _treatment.Add(newTrr5);
             _treatment.Add(newTrr6);
             _treatment.Add(newTrr7);
             _treatment.Add(newTrr8);*/

            _appointmentController = app.appointmentController;
            _userController = app.userController;


            _roomController = app.roomController;

            initApp();
            

            dpTreatmentView.SelectedDate = DateTime.Now;

            initSelected();

            btnChooseLastDate.Focus();
        }

        private void initApp()
        {

            _appointments = _appointmentController.GetAppointmentForDoctor((Application.Current as App)._currentUser).ToList();

            _appointments = _appointments.OrderBy(x => x.StartDateTime.TimeOfDay).ToList();

            foreach (Appointment item in _appointments)
            {
                item.Patient = (Patient)_userController.GetFromID(item.Patient.Id);
                item.Room = (Room)_roomController.GetFromID(item.Room.RoomNumber);
            }

            tableTreatmentView.Items.Refresh();

        }

        private void initSelected()
        {
                DateTime date = (DateTime)dpTreatmentView.SelectedDate;

                _selectedAppointent.Clear();

                foreach (Appointment item in _appointments)
                {
                    if (item.StartDateTime.Date == date.Date)
                    {
                        _selectedAppointent.Add(item);
                    }
                }

                tableTreatmentView.Items.Refresh();
        }

        public List<Appointment> Appointments
        {
            get { return _selectedAppointent; }
            set { _selectedAppointent=value;}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }


        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                NavigationService.Navigate(new DefaultPage());
            }
            if (e.Key == Key.Q)
                btnChooseLastDate_Click(sender, e);
            if (e.Key == Key.E)
                btnChooseNextDate_Click(sender, e);
        }

        private void HandleDP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.dpTreatmentView.IsDropDownOpen = true;
            }
        }

        private void btnChooseLastDate_Click(object sender, RoutedEventArgs e)
        {
            _date = _date.AddDays(-1);
            OnPropertyChanged("Date");
        }

        private void btnChooseNextDate_Click(object sender, RoutedEventArgs e)
        {
            _date = _date.AddDays(1);
            OnPropertyChanged("Date");
        }



        private void dpTreatmentView_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            initSelected();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("TEST");
            DateTime date = (DateTime)dpTreatmentView.SelectedDate;

            DateTime dateNow = DateTime.Now;

            if (!(date.Date > dateNow.Date))
            {
                _selectedAppointent.Clear();

                foreach (Appointment item in _appointments)
                {
                    if (item.StartDateTime.Date == date.Date && item.StartDateTime.TimeOfDay > dateNow.TimeOfDay)
                    {
                        _selectedAppointent.Add(item);
                    }
                }
            }

            tableTreatmentView.ItemsSource = null; 
            tableTreatmentView.ItemsSource = _selectedAppointent;
            tableTreatmentView.Items.Refresh();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            initSelected();
        }

        private void tableTreatmentView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                Appointment app = (Appointment)tableTreatmentView.SelectedValue;

                if (app != null)
                {
                    Record dialog = new Record(app.Patient.Id);
                    dialog.ShowDialog();
                    initApp();
                    initSelected();

                }
            }

            if (e.Key == Key.Q)
                btnChooseLastDate_Click(sender, e);
            if (e.Key == Key.E)
                btnChooseNextDate_Click(sender, e);
        }

        private void tableTreatmentView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Appointment app = (Appointment)tableTreatmentView.SelectedValue;

            if (app != null)
            {
                Record dialog = new Record(app.Patient.Id);
                dialog.ShowDialog();
                initApp();
                initSelected();
            }
        }

        private void tableTreatmentView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool postoji = false;
            Appointment app = (Appointment)tableTreatmentView.SelectedValue;

            if (app != null)
            {
                foreach (Treatment item in _recordController.GetRecordForPatient(app.Patient.Id).Treatment)
                {
                    Console.WriteLine(item.TreatmentReport.PatientId.ToString() + " == " + app.Patient.Id.ToString());
                    Console.WriteLine(app.StartDateTime.ToString() + " < " + item.Date.ToString());
                    Console.WriteLine(app.EndDateTime.ToString() + " > " + item.Date.ToString());

                    Console.WriteLine("============================");


                    if (item.TreatmentReport.PatientId == app.Patient.Id && app.StartDateTime<item.Date && app.EndDateTime>item.Date)
                    {
                       
                        tbAnamnesis.Text = item.TreatmentReport.Anamnesis;
                        tbDiagnosis.Text = item.TreatmentReport.Diagnosis;
                        tbNote.Text = item.TreatmentReport.Note;
                        tbTherapy.Text = item.TreatmentReport.Theraphy;
                        postoji = true;

                        lblDatum.Content = app.StartDateTime.ToString("dd MMM yyyy HH:mm");
                        lblType.Content = app.Type;
             
                    }
                }
            }

            if (!postoji)
            {
                tbAnamnesis.Text = "";
                tbDiagnosis.Text = "";
                tbNote.Text = "";
                tbTherapy.Text = "";
                lblDatum.Content = "";
                lblType.Content = "";
            }
        }
    }
}
