using Controller.AppointmenController;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Appointment;
using Model.MedicalRecords;
using Model.Users;
using System;
using System.Collections.Generic;
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

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for InstructionForControl.xaml
    /// </summary>
    public partial class InstructionForControl : Window
    {
        private bool confirm = false;

        private const string INPUT_ERROR_DATE = "Morate navesti datum";
        private const string INPUT_ERROR_TIME = "Morate navesti vreme";
        private const string INPUT_ERROR_NOTE = "Morate navesti napomenu";

        private IAppointmentController _appointmentController;
        private Appointment _appointment = new Appointment();

        private List<String> _times= new List<string>();
        private List<Appointment> _freeAppointment = new List<Appointment>();
        private Patient _patient;
        public InstructionForControl(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            _patient = patient;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.dpDateFor.PreviewKeyDown += new KeyEventHandler(HandleDP);
            this.cbTime.KeyDown += new KeyEventHandler(HandleCB);

            var app = Application.Current as App;


            _appointmentController = app.appointmentController;


            Doctor doctor = (Doctor)(Application.Current as App)._currentUser;
            if (doctor.WorkPlace.Name.Equals("Lekar opste prakse"))
            {
                _dateTo = DateTime.Now.AddDays(App.MaxDaysForControlExamination);
            }
            else
            {
                _dateTo = DateTime.Now.AddDays(App.MaxDaysForControlSpecialistExamination);
            }
        }


        private DateTime _dateFrom = DateTime.Now.AddDays(1);
        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set
            {
                this._dateFrom = value;
            }
        }

        private DateTime _dateTo;
        public DateTime DateTo
        {
            get { return _dateTo; }
            set
            {
                this._dateTo = value;
            }
        }

        private List<Appointment> FreeAppointment
        {
            get { return _freeAppointment; }
        }
        public List<String> Times
        {
            get { return _times; }
        }

        private void HandleCB(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void HandleDP(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                ((DatePicker)sender).IsDropDownOpen = true;
            }
        }

        public Appointment NewAppointment
        {
            get { return _appointment; }
        }

        private void btnSendControlInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _appointment = (Appointment)cbTime.SelectedItem;

                this.DialogResult = true;
                this.confirm = true;
                this.Close();
            }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

        
        private bool isValidInputData()
        {
            if (dpDateFor.SelectedDate == null)
            {
                ShowError(INPUT_ERROR_DATE);
                return false;
            }
            else if (cbTime.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_TIME);
                return false;
            }
            else if (tbNote.Text == "")
            {
                ShowError(INPUT_ERROR_NOTE);
                return false;
            }
            return true;
        }

        public bool isConfirmed()
        {
            return confirm;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void dpDateFor_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = dpDateFor.SelectedDate.Value.Date;

            Doctor doctor = (Doctor)(Application.Current as App)._currentUser;

            if (doctor.WorkPlace.Name.Equals("Lekar opste prakse"))
            {
                _freeAppointment = _appointmentController.GetFreeAppointments(_patient, doctor, dt, TreatmentType.Examination).ToList();
            }
            else
            {
                _freeAppointment = _appointmentController.GetFreeAppointments(_patient, doctor, dt, TreatmentType.Specialistexamination).ToList();
            }

            cbTime.ItemsSource = null;
            cbTime.ItemsSource = _freeAppointment;
            cbTime.Items.Refresh();
        }
    }
}
