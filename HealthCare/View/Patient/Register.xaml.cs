using HealthCare;
using Model.MedicalRecords;
using Model.Users;
using Model.Users.UserBuilder;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private static readonly Regex _jmbgRegex = new Regex(@"^[0-9]{13}$");
        private static readonly Regex _residenceRegex = new Regex(@"^[ ]*[A-Z][a-zA-z ]+[ ]*,[ ]*[1-9][0-9]*[ ]*,[ ]*[A-Z][a-zA-z ]+[ ]*,[ ]*[1-9][0-9]{4}[ ]*,[ ]*[A-Z][a-z]+[ ]*$");

        public ObservableCollection<Gender> Genders { get; set; }

        public Register()
        {
            Genders = new ObservableCollection<Gender>()
            {
                Gender.Male,
                Gender.Female
            };
            this.DataContext = this;
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordPasswordBox.Password != PasswordRepeatPasswordBox.Password)
            {
                return;
            }
            if(NameTextBox.Text == "" || SurnameTextBox.Text == "" || BirthdateDatePicker.SelectedDate == null || GenderComboBox.SelectedItem == null || !_jmbgRegex.IsMatch(JMBGTextBox.Text) || !_residenceRegex.IsMatch(AdressTextBox.Text))
            {
                return;
            }
            PatientBuilder patientBuilder = new PatientBuilder();
            patientBuilder.SetName(NameTextBox.Text);
            patientBuilder.SetSurname(SurnameTextBox.Text);
            patientBuilder.SetGender((Gender)GenderComboBox.SelectedItem);
            patientBuilder.SetJMBG(JMBGTextBox.Text);
            patientBuilder.SetBirthDate(BirthdateDatePicker.SelectedDate.Value);
            Contact contact = new Contact() { Number = PhoneTextBox.Text, Email = EmailTextBox.Text };
            patientBuilder.SetContact(contact);

            string[] partsResidence = AdressTextBox.Text.Split(',');
            string street = partsResidence[0].Trim();
            int numberStreet = int.Parse(partsResidence[1].Trim());
            string city = partsResidence[2].Trim();
            int postalCode = int.Parse(partsResidence[3].Trim());
            string country = partsResidence[4].Trim();
            Adress adress = new Adress() { City = new City() { country = new Country() { Name = country }, Name = city, PostNumber = postalCode }, Number = numberStreet, Street = street };

            patientBuilder.SetResidence(adress);
            patientBuilder.SetPassword(PasswordPasswordBox.Password);
            User patient = patientBuilder.getResult();
            var app = Application.Current as App;
            patient = app.userController.Create(patient);
            MedicalRecord medicalRecord = new MedicalRecord() { PatientID = patient.Id };
            medicalRecord = app.medicalRecordController.Create(medicalRecord);
            Patient patientCast = patient as Patient;
            patientCast.MedicalRecord = new MedicalRecord() { Id = medicalRecord.Id };
            app.userController.Update(patientCast);
            this.NavigationService.Navigate(new Uri("/View/Patient/Login.xaml", UriKind.Relative));
        }
    }
}
