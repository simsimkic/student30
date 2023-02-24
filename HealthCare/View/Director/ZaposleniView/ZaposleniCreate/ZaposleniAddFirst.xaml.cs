using HCIProjekat.View.ConfirmationDialogsView;
using Model.Users;
using Model.Users.UserBuilder;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAdd.xaml
    /// </summary>
    public partial class ZaposleniAddFirst : Page
    {
        private static readonly Regex _jmbgRegex = new Regex(@"^[0-9]{13}$");
        private static readonly Regex _birthPlaceRegex = new Regex(@"^[ ]*[A-Z][a-zA-z ]+[ ]*,[ ]*[1-9][0-9]{4}[ ]*,[ ]*[A-Z][a-z]+[ ]*$");
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");

        private const string MANDATORY_NAME_FIELD = "Ime je obavezno polje!";
        private const string MANDATORY_SURNAME_FIELD = "Prezime je obavezno polje!";
        private const string MANDATORY_JMBG_FIELD = "JMBG je obavezno polje!";
        private const string INVALID_JMBG_FIELD = "Unet JMBG nije validan!";
        private const string MANDATORY_BIRTHDATE_FIELD = "Datum rodjenja je obavezno polje!";
        private const string INVALID_BIRTHDATE_FIELD = "Unet datum rodjenja nije validan!";
        private const string MANDATORY_BIRTHPLACE_FIELD = "Mesto rodjenja je obavezno polje!";
        private const string INVALID_BIRTHPLACE_FIELD = "Uneto mesto rodjenja nije validno!";


        private string _name;
        private string _surname;
        private string _jmbg;
        private string _birthPlace;

        private SecretaryBuilder secretaryBuilder;
        public ZaposleniAddFirst()
        {
            secretaryBuilder = new SecretaryBuilder();

            InitializeComponent();
            DataContext = this;

        }
        public string EmployeeName
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string JMBG
        {
            get { return _jmbg; }
            set
            {
                if (_jmbg != value)
                {
                    _jmbg = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BirthPlace
        {
            get { return _birthPlace; }
            set
            {
                if (_birthPlace != value)
                {
                    _birthPlace = value;
                    OnPropertyChanged();
                }
            }
        }
        private void AddNext_Click(object sender, RoutedEventArgs e)
        {

            if (!isNameFulfilled())
            {
                ShowError(MANDATORY_NAME_FIELD);
            }
            else if (!isSurnameFulfilled())
            {
                ShowError(MANDATORY_SURNAME_FIELD);
            }
            else if (!isJMBGFulfilled())
            {
                ShowError(MANDATORY_JMBG_FIELD);
            }
            else if (!isJMBGValid())
            {
                ShowError(INVALID_JMBG_FIELD);
            }
            else if (!isBirthDateSelected())
            {
                ShowError(MANDATORY_BIRTHDATE_FIELD);
            }
            else if (!isBirthDateValid())
            {
                ShowError(INVALID_BIRTHDATE_FIELD);
            }
            else if (!isBirthPlaceFulfilled())
            {
                ShowError(MANDATORY_BIRTHPLACE_FIELD);
            }
            else if (!isBirthPlaceValid())
            {
                ShowError(INVALID_BIRTHPLACE_FIELD);
            }
            else
            {
                createUser();
            }

        }

        private void createUser()
        {
            try
            {
                string[] partsBirthPlace = _birthPlace.Split(',');
                string place = partsBirthPlace[0].Trim();
                int number = int.Parse(partsBirthPlace[1].Trim());
                string country = partsBirthPlace[2].Trim();
                City birthPlace = new City() { country = new Country() { Name = country }, Name = place, PostNumber = number };

                secretaryBuilder.SetJMBG(_jmbg);
                secretaryBuilder.SetName(_name);
                secretaryBuilder.SetSurname(_surname);
                secretaryBuilder.SetPassword("");
                secretaryBuilder.SetGender(((bool)male.IsChecked) ? Gender.Male : Gender.Female);
                secretaryBuilder.SetBirthDate(((DateTime)datumRodjenja.SelectedDate).Date);
                secretaryBuilder.SetBirthPlace(birthPlace);

                MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniAddSecond(secretaryBuilder), false);

            }
            catch (Exception)
            {
                ShowError("Uneti nevalidni podaci!");
            }
        }

        private bool validJMBG(string input) => !_jmbgRegex.IsMatch(input);
        private bool validBirthPlace(string input) => !_birthPlaceRegex.IsMatch(input);
        private bool isBirthDateValid()
        {
            return ((DateTime)datumRodjenja.SelectedDate).Date.AddYears(18) <= DateTime.Now.Date;
        }

        private bool isBirthPlaceValid()
        {
            return !validBirthPlace(_birthPlace);
        }

        private bool isBirthPlaceFulfilled()
        {
            return _birthPlace != "" && _birthPlace != null;
        }

        private bool isBirthDateSelected()
        {
            return datumRodjenja.SelectedDate != null;
        }

        private bool isJMBGValid()
        {
            return !validJMBG(_jmbg);
        }

        private bool isJMBGFulfilled()
        {
            return _jmbg != "" && _jmbg != null;
        }

        private bool isSurnameFulfilled()
        {
            return _surname != "" && _surname != null;
        }

        private bool isNameFulfilled()
        {
            return _name != "" && _name != null;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void textBoxJMBG_PreviewTextInput(object sender, TextCompositionEventArgs e)
                     => e.Handled = !isTextInt(e.Text);

        private bool isTextInt(string input) => !_intRegex.IsMatch(input);

        private void textBoxJMBG_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextInt(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

    }
}
