using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Controller.AppointmenController;
using Controller.UserControllers;
using HealthCare;
using Model.Appointment;
using Model.DataFiltration;
using Model.MedicalRecords;
using Model.Users;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for ChooseAppointment.xaml
    /// </summary>
    /// 

    public partial class ChooseAppointment : Page, INotifyPropertyChanged
    {

        public DateTime LowerLimitDateTime { get; set; }
        
        public ObservableCollection<User> Collection { get; set; }
        public ObservableCollection<User> SearchCollection { get; set; }

        private IAppointmentController appointmentController;

        public ObservableCollection<Appointment> AppointmentCollection { get; set; }

        private string doctorTextBox;
        private string dateTimeTextBox;

        public string DoctorTextBox { get { return doctorTextBox; } set { doctorTextBox = value; OnPropertyChanged(); } }
        public string DateTimeTextBox { get { return dateTimeTextBox; } set { dateTimeTextBox = value; OnPropertyChanged(); } }

        private User doctor;
        private DateTime date;
        private Appointment period;
        public Appointment Period
        {
            get { return period; }
            set
            {
                period = value;
                DoctorTextBox = "kod lekara opšte prakse Dr ";
                DoctorTextBox += period.Doctor.Name;
                DoctorTextBox += " ";
                DoctorTextBox += period.Doctor.Surname;
                DateTimeTextBox = "datuma ";
                DateTimeTextBox += period.StartDateTime.ToString("d");
                DateTimeTextBox += " u terminu ";
                DateTimeTextBox += period.StartDateTime.ToString("t");
                DateTimeTextBox += " ";
                DateTimeTextBox += period.EndDateTime.ToString("t");
                OnPropertyChanged();
            }
        }

        public User Doctor 
        { 
            get { return doctor; } 
            set 
            {
                doctor = value;
                OnPropertyChanged();
            } 
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ChooseAppointment()
        {
            LowerLimitDateTime = DateTime.Now.AddDays(1);
            AppointmentCollection = new ObservableCollection<Appointment>();
            var app = Application.Current as App;
            appointmentController = app.appointmentController;
            WorkPlace workPlace = new WorkPlace() { Name = "Lekar opste prakse" };
            Collection = new ObservableCollection<User>(app.userController.GetDoctorFromWorkPlace(workPlace));
            SearchCollection = new ObservableCollection<User>();
            this.DataContext = this;
            InitializeComponent();
            initializeSearchCollection();
        }

        private void SearchDoctor_Click(object sender, RoutedEventArgs e)
        {
            initializeSearchCollection();
        }

        private void initializeSearchCollection()
        {
            SearchCollection.Clear();
            foreach (User doctor in Collection)
            {
                if (doctor.Name.ToLower().Contains(DoctorSearch.Text.ToLower()) || doctor.Surname.ToLower().Contains(DoctorSearch.Text.ToLower()))
                {
                    SearchCollection.Add(doctor);
                }
            }
        }

        private void PickDoctor_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Doctor = button.DataContext as User;
            calendar.Visibility = Visibility.Visible;
            calendar.BringIntoView();


        }
        private void PickPeriod_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            period = button.DataContext as Appointment;
            Period = period;
            appointment.Visibility = Visibility.Visible;
            appointment.BringIntoView();
        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            var ddate = calendarControl.SelectedDate;
            if (!ddate.HasValue)
            {
                return;
            }
            AppointmentCollection.Clear();
            Date = ddate.Value;
            var app = Application.Current as App;
            User patient = app._currentUser;
            TreatmentType treatmentType = TreatmentType.Examination;
            List<Appointment> freeAppointments = appointmentController.GetFreeAppointments(patient, doctor, date, treatmentType).ToList();
            foreach(Appointment appointment in freeAppointments)
            {
                AppointmentCollection.Add(appointment);
            }
            periodView.Visibility = Visibility.Visible;
            periodView.BringIntoView();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appointmentController.Create(period);
            this.NavigationService.Navigate(new Uri(@"/View/Patient/SuccessfullyMadeAppointment.xaml", UriKind.Relative));
        }
    }
}
