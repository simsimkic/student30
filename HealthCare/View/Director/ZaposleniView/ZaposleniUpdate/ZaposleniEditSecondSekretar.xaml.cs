using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
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

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAddSecond.xaml
    /// </summary>
    public partial class ZaposleniEditSecondSekretar : Page
    {
        private static readonly Regex _phoneRegex = new Regex(@"[^0-9+]+$");
        private static readonly Regex _phoneRegexMatch = new Regex(@"^[+][0-9]{10,14}$");
        private static readonly Regex _residenceRegex = new Regex(@"^[ ]*[A-Z][a-zA-z ]+[ ]*,[ ]*[1-9][0-9]*[ ]*,[ ]*[A-Z][a-zA-z ]+[ ]*,[ ]*[1-9][0-9]{4}[ ]*,[ ]*[A-Z][a-z]+[ ]*$");

        private const string MANDATORY_RESIDENCE_FIELD = "Prebivaliste je obavezno polje!";
        private const string INVALID_RESIDENCE_FIELD = "Uneto prebivaliste nije validno!";
        private const string MANDATORY_EDUCATION_LEVEL_FIELD = "Stepen strucne spreme je obavezno polje!";
        private const string MANDATORY_PHONE_FIELD = "Broj telefona je obavezno polje!";
        private const string INVALID_PHONE_FIELD = "Unet broj telefona nije validan!";
        private const string MANDATORY_EMAIL_FIELD = "E-mail adresa je obavezno polje!";
        private const string INVALID_EMAIL_FIELD = "Uneta e-mail adresa nije validno!";

        private string _residence;
        private string _email;
        private string _phone;
        private Model.Users.Secretary Staff;

        private readonly IUserController _userController;

        public ZaposleniEditSecondSekretar(Staff user)
        {
            Staff = (Model.Users.Secretary)user;

            _residence = Staff.Residence.Street + ", " + Staff.Residence.Number + ", " + Staff.Residence.City.Name + ", " + Staff.Residence.City.PostNumber + ", " +
                            Staff.Residence.City.Country.Name;
            _email = Staff.Contact.Email;
            _phone = Staff.Contact.Number;

            var app = Application.Current as App;
            _userController = app.userController;
            InitializeComponent();
            EducationLevel.SelectedItem = Staff.EducationLevel;

            DataContext = this;
        }
        public string Residence
        {
            get { return _residence; }
            set
            {
                if (_residence != value)
                {
                    _residence = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }
        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene informacija zaposlenog?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEditingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void editSekretar_Click(object sender, RoutedEventArgs e)
        {
            if (!isResidenceFulfilled())
            {
                ShowError(MANDATORY_RESIDENCE_FIELD);
            }
            else if (!isResidenceValid())
            {
                ShowError(INVALID_RESIDENCE_FIELD);
            }
            else if (!isEduLevelSelected())
            {
                ShowError(MANDATORY_EDUCATION_LEVEL_FIELD);
            }
            else if (!isPhoneFulfilled())
            {
                ShowError(MANDATORY_PHONE_FIELD);
            }
            else if (!isPhoneValid())
            {
                ShowError(INVALID_PHONE_FIELD);
            }
            else if (!isMailFulfilled())
            {
                ShowError(MANDATORY_EMAIL_FIELD);
            }
            else if (!isMailValid())
            {
                ShowError(INVALID_EMAIL_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Izmena informacija zaposlenog", "Da li ste sigurni da zelite da izmenite informacije zaposlenom?",
                                                               new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmEditingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private void confirmEditingEvent(object sender, RoutedEventArgs e)
        {        
            fulfillStaff();
            _userController.Update(Staff);
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);

        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private bool validEmail(string input)
        {
            try
            {
                MailAddress mail = new MailAddress(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool isMailValid()
        {
            return validEmail(_email);
        }

        private bool isMailFulfilled()
        {
            return _email != "" && _email != null;
        }
        private bool validPhone(string input) => !_phoneRegexMatch.IsMatch(input);

        private bool isPhoneValid()
        {
            return !validPhone(_phone);
        }

        private bool isPhoneFulfilled()
        {
            return _phone != "" && _phone != null;
        }

        private bool isEduLevelSelected()
        {
            return EducationLevel.SelectedItem != null;
        }

        private bool validResidence(string input) => !_residenceRegex.IsMatch(input);

        private bool isResidenceValid()
        {
            return !validResidence(_residence);
        }

        private bool isResidenceFulfilled()
        {
            return _residence != "" && _residence != null;
        }
        private void telefon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        => e.Handled = !isTextPhone(e.Text);

        private bool isTextPhone(string input) => !_phoneRegex.IsMatch(input);

        private void telefon_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextPhone(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
        private void fulfillStaff()
        {
            Staff.Contact = new Contact() { Email = _email, Number = _phone };
            Staff.EducationLevel = (Educationlevel)EducationLevel.SelectedItem;
            Staff.Password = _userController.GetFromID(Staff.Id).Password;
            try
            {
                string[] partsResidence = _residence.Split(',');
                string street = partsResidence[0].Trim();
                int numberStreet = int.Parse(partsResidence[1].Trim());
                string city = partsResidence[2].Trim();
                int postalCode = int.Parse(partsResidence[3].Trim());
                string country = partsResidence[4].Trim();
                Adress adress = new Adress() { City = new City() { country = new Country() { Name = country }, Name = city, PostNumber = postalCode }, Number = numberStreet, Street = street };
                Staff.Residence = adress;
            }
            catch (Exception)
            {
                ShowError("Uneti nevalidni podaci!");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
