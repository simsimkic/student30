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
using Model.Communication;
using Model.DataFiltration;
using Model.Users;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for AskQuestion.xaml
    /// </summary>
    public partial class AskQuestion : Page, INotifyPropertyChanged
    {

        public ObservableCollection<User> Collection { get; set; }
        public ObservableCollection<User> SearchCollection { get; set; }
        public ObservableCollection<WorkPlace> WorkPlaceCollection { get; set; }

        private User doctor;

        private string doctorTextBox;

        public string DoctorTextBox { get { return doctorTextBox; } set { doctorTextBox = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public AskQuestion()
        {
            var app = Application.Current as HealthCare.App;
            WorkPlaceCollection = new ObservableCollection<WorkPlace>(app.workPlaceController.GetAll());

            StaffFilter staffFilter = new StaffFilter() { Id = -1, Name = "", Surname = "", StaffType = "Doktor" };
            Collection = new ObservableCollection<User>(app.userController.GetFilteredStaff(staffFilter));
            SearchCollection = new ObservableCollection<User>();
            
            this.DataContext = this;
            InitializeComponent();
            comboBox.SelectedIndex = 0;
            initializeSearchCollection();
        }

        private void PickDoctor_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            doctor = button.DataContext as Doctor;
            DoctorTextBox = "Dr ";
            DoctorTextBox += doctor.Name;
            DoctorTextBox += " ";
            DoctorTextBox += doctor.Surname;
            AskQuestionForm.Visibility = Visibility.Visible;
            AskQuestionForm.BringIntoView();
        }

        private void SearchDoctor_Click(object sender, RoutedEventArgs e)
        {
            initializeSearchCollection();
        }

        private void initializeSearchCollection()
        {
            SearchCollection.Clear();
            WorkPlace workPlace = (WorkPlace)comboBox.SelectedItem;
            foreach (Doctor doctor in Collection)
            {

                if ((doctor.Name.ToLower().Contains(DoctorSearch.Text.ToLower()) || doctor.Surname.ToLower().Contains(DoctorSearch.Text.ToLower())) && workPlace.Name.Equals(doctor.WorkPlace.Name))
                {
                    SearchCollection.Add(doctor);
                }
            }
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as HealthCare.App;
            Question question = new Question() { Title = TitleForm.Text, Questions = QuestionForm.Text, Doctor = new Doctor() { Id = doctor.Id }, Patient = new Patient() { Id = app._currentUser.Id }, PatientName = "Anonimno" }; 
            app.questionController.Create(question);
            this.NavigationService.Navigate(new Uri(@"/View/Patient/SuccessfullyAskedQuestion.xaml", UriKind.Relative));
        }
    }
}
