using HCIProjekat.View.ConfirmationDialogsView;
using Model.Users;
using Model.Users.UserBuilder;
using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAddSecond.xaml
    /// </summary>
    public partial class ZaposleniAddSecond : Page
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

        private SecretaryBuilder secretaryBuilder;

        public ZaposleniAddSecond(SecretaryBuilder secretaryBuilder)
        {
            this.secretaryBuilder = secretaryBuilder;
            InitializeComponent();
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
        private void AddNext_Click(object sender, RoutedEventArgs e)
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
                fulfillUser();
            }
        }

        private void fulfillUser()
        {
            try
            {
                secretaryBuilder.SetContact(new Contact() { Email = _email, Number = _phone });
                secretaryBuilder.SetPicture(@"../../View/Director/Styles/Images/defaultProfilnapng.png");
                secretaryBuilder.SetEducationLevel((Educationlevel)EducationLevel.SelectedItem);
                secretaryBuilder.SetUserType(Usertype.Secretary);

                string[] partsResidence = _residence.Split(',');
                string street = partsResidence[0].Trim();
                int numberStreet = int.Parse(partsResidence[1].Trim());
                string city = partsResidence[2].Trim();
                int postalCode = int.Parse(partsResidence[3].Trim());
                string country = partsResidence[4].Trim();
                Adress adress = new Adress() { City = new City() { country = new Country() { Name = country }, Name = city, PostNumber = postalCode }, Number = numberStreet, Street = street };
                secretaryBuilder.SetResidence(adress);

                MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniAddThird(secretaryBuilder), false);
            }
            catch (Exception)
            {
                ShowError("Uneti nevalidni podaci!");
            }
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

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od dodavanja", "Da li ste sigurni da zelite da odustanete od dodavanja zaposlenog?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
