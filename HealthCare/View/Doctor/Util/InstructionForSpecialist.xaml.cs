using Controller.AppointmenController;
using Controller.OtherDataController;
using Controller.UserControllers;
using HealthCare;
using HealthCareClinic.View.Doctor;
using Model.Appointment;
using Model.DataFiltration;
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
    /// Interaction logic for InstructionForSpecialist.xaml
    /// </summary>
    public partial class InstructionForSpecialist : Window
    {
        private const string INPUT_ERROR_SPECIALIZATION= "Morate navesti specijalizaciju";
        private const string INPUT_ERROR_DOCTOR = "Morate navesti lekara";
        private const string INPUT_ERROR_DATE = "Morate navesti datum";
        private const string INPUT_ERROR_TIME = "Morate navesti vreme";
        private const string INPUT_ERROR_NOTE = "Morate navesti napomenu";


        private bool confirm = false;

        private Appointment _newAppointment;
     
        private IAppointmentController _appointmentControler;
        private ISpecializationController _specializationControler;
        private IWorkPlaceController _workPlaceController;
        private readonly IUserController _userController;
        private Patient _patient;

        private List<WorkPlace> _specializations;
        private List<User> _doctorsFromSpecialization = new List<User>();
        private List<String> _times = new List<string>();
        private List<Appointment> _freeAppointment = new List<Appointment>();


        public InstructionForSpecialist(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.dpDateForAppointment.PreviewKeyDown += new KeyEventHandler(HandleDP);
            this.cbDoctor.KeyDown += new KeyEventHandler(HandleCB);
            this.cbTime.KeyDown += new KeyEventHandler(HandleCB);
            this.cbSpecialization.KeyDown += new KeyEventHandler(HandleCB);

            _patient = patient;


            var app = Application.Current as App;

            _specializationControler = app.specializationController;
            _userController = app.userController;
            _workPlaceController = app.workPlaceController;
            _appointmentControler = app.appointmentController;

            _specializations = _workPlaceController.GetAll().ToList();

            foreach (WorkPlace item in _specializations)
            {
                if (item.Name == "Lekar opste prakse")
                {
                    _specializations.Remove(item);
                    break;
                }
            }
            /*
            _times.Add("08:00-08:30");
            _times.Add("08:30-09:00");
            _times.Add("09:00-09:30");
            _times.Add("09:30-10:00");
            _times.Add("10:00-10:30");
            _times.Add("11:00-11:30");
            _times.Add("11:30-12:00");

            _doctorsFromSpecialization.Add(new Doctor() { Name = "Dusan", Surname = "Petrovic" });
            _doctorsFromSpecialization.Add(new Doctor() { Name = "Petar", Surname = "Stefanovic" });
            _doctorsFromSpecialization.Add(new Doctor() { Name = "Lazar", Surname = "Djokic" });
            _doctorsFromSpecialization.Add(new Doctor() { Name = "Mika", Surname = "Mikic" });
            _doctorsFromSpecialization.Add(new Doctor() { Name = "Pera", Surname = "Peric" });*/
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

        private DateTime _dateTo= DateTime.Now.AddDays(App.MaxDaysForSpecialistExamination);
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


        public List<WorkPlace> Specializations
        {
            get { return _specializations; }
            set
            {
                _specializations = value;
            }
        }

        public List<String> Times
        {
            get { return _times; }
        }


        public List<User> Doctors
        {
            get { return _doctorsFromSpecialization; }
            set
            {
                _doctorsFromSpecialization = value;
            }
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

        private void btnSendSpecialistInstruction_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _newAppointment = (Appointment)cbTime.SelectedItem;

                this.DialogResult = true;
                this.confirm = true;
                this.Close();
            }
        }

        public Appointment NewAppointment
        {
            get { return _newAppointment; }
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();


        private bool isValidInputData()
        {
            if (cbSpecialization.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_SPECIALIZATION);
                return false;
            }else if (cbDoctor.SelectedItem == null)
            {
                ShowError(INPUT_ERROR_DOCTOR);
                return false;
            }
            else if (dpDateForAppointment.SelectedDate == null)
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

        private void cbSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorkPlace specialization = (WorkPlace)cbSpecialization.SelectedItem;

            _doctorsFromSpecialization = new List<User>();

            _doctorsFromSpecialization = _userController.GetDoctorFromWorkPlace(specialization).ToList();

            cbDoctor.ItemsSource = null;
            cbDoctor.ItemsSource = _doctorsFromSpecialization;
            cbDoctor.Items.Refresh();
        }

        private void dpDateForAppointment_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = dpDateForAppointment.SelectedDate.Value.Date;
            
            Doctor doctor = (Doctor)cbDoctor.SelectedItem;
            if (doctor != null)
            {
                _freeAppointment = _appointmentControler.GetFreeAppointments(_patient, doctor, dt, TreatmentType.Specialistexamination).ToList();
                cbTime.ItemsSource = null;
                cbTime.ItemsSource = _freeAppointment;
                cbTime.Items.Refresh();
            }
        }
    }
}
