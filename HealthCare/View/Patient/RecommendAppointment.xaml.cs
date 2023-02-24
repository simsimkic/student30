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
using HealthCare;
using HealthCare.Model.Appointment;
using Model.Appointment;
using Model.Users;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for RecommendAppointment.xaml
    /// </summary>
    public partial class RecommendAppointment : Page, INotifyPropertyChanged
    {
        
        public DateTime LowerLimitDateTime { get; set; }

        public ObservableCollection<User> Collection { get; set; }
        public ObservableCollection<User> SearchCollection { get; set; }
        public string DoctorTextBox { get { return doctorTextBox; } set { doctorTextBox = value; OnPropertyChanged(); } }
        private User doctor;
        private string doctorTextBox;

        public string DateTimeTextBox { get { return dateTimeTextBox; } set { dateTimeTextBox = value; OnPropertyChanged(); } }
        private string dateTimeTextBox;

        private string text;
        public string Text 
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        

        private DateTime dateFrom;
        private DateTime dateTo;

        private Appointment appointment;
        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                if(value == null)
                {
                    Text = "Nije pronadjen termin za dati unos";
                    DoctorTextBox = "";
                    DateTimeTextBox = "";
                    return;
                }
                Text = "Uspešno smo Vam pronašli termin ako Vam isti odgovara potvrdite izbor";
                var app = Application.Current as App;
                Doctor doctorObject = (Doctor)app.userController.GetFromID(value.Doctor.Id);
                DoctorTextBox = "kod lekara opšte prakse Dr ";
                DoctorTextBox += doctorObject.Name;
                DoctorTextBox += " ";
                DoctorTextBox += doctorObject.Surname;
                DateTimeTextBox = "datuma ";
                DateTimeTextBox += value.StartDateTime.Date.ToString("d");
                DateTimeTextBox += " u terminu ";
                DateTimeTextBox += value.StartDateTime.ToString("t");
                DateTimeTextBox += " ";
                DateTimeTextBox += value.EndDateTime.ToString("t");
                OnPropertyChanged();
            }
        }

        private RecommendAppointmentParameters recommendAppointmentParameters;

        private IAppointmentController appointmentController;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RecommendAppointment()
        {
            LowerLimitDateTime = DateTime.Now.AddDays(1);
            Appointment = null;
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
            doctor = button.DataContext as Doctor;    
            calendar.Visibility = Visibility.Visible;
            calendar.BringIntoView();
        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            var DateCollection = calendarControl.SelectedDates;
            if (DateCollection.Count == 0)
            {
                return;
            }
            List<DateTime> dateTimeList = DateCollection.ToList();
            dateFrom = dateTimeList.First().Date;
            dateTo = dateTimeList.Last().Date;
            priority.Visibility = Visibility.Visible;
            priority.BringIntoView();

        }
        private void DoctorPriority_Click(object sender, RoutedEventArgs e)
        {
            recommendAppointmentParameters = new RecommendAppointmentParameters(doctor,dateFrom,dateTo,AppointmentPriority.DoctorPriority);
            var app = Application.Current as App;
            Appointment = app.appointmentController.RecommendAppointment(recommendAppointmentParameters);
            
            appointmentV.Visibility = Visibility.Visible;
            appointmentV.BringIntoView();
        }
        private void PeriodPriority_Click(object sender, RoutedEventArgs e)
        {
            recommendAppointmentParameters = new RecommendAppointmentParameters(doctor,dateFrom,dateTo,AppointmentPriority.DatePriority);
            var app = Application.Current as App;
            Appointment = app.appointmentController.RecommendAppointment(recommendAppointmentParameters);
         
            appointmentV.Visibility = Visibility.Visible;
            appointmentV.BringIntoView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Appointment != null)
            {
                appointmentController.Create(Appointment);
                this.NavigationService.Navigate(new Uri(@"/View/Patient/SuccessfullyMadeAppointment.xaml", UriKind.Relative));
            }
            
        }
    }
}
