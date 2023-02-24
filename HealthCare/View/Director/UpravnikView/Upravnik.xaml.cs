using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for Upravnik.xaml
    /// </summary>
    public partial class Upravnik : Page
    {
        private static readonly Regex _birthPlaceRegex = new Regex(@"^[ ]*[A-Z][a-zA-z ]+[ ]*,[ ]*[1-9][0-9]{4}[ ]*,[ ]*[A-Z][a-z]+[ ]*$");
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");

        private const string MANDATORY_NAME_FIELD = "Ime je obavezno polje!";
        private const string MANDATORY_SURNAME_FIELD = "Prezime je obavezno polje!";
        private const string MANDATORY_BIRTHDATE_FIELD = "Datum rodjenja je obavezno polje!";
        private const string INVALID_BIRTHDATE_FIELD = "Unet datum rodjenja nije validan!";
        private const string MANDATORY_BIRTHPLACE_FIELD = "Mesto rodjenja je obavezno polje!";
        private const string INVALID_BIRTHPLACE_FIELD = "Uneto mesto rodjenja nije validno!";


        private string _name;
        private string _surname;
        private string _jmbg;
        private string _birthPlace;

        private readonly IUserController _userController;

        public Staff Staff { get; set; }
        public Upravnik()
        {
            var app = Application.Current as App;
            _userController = app.userController;


            Staff = (Staff)_userController.GetFromID(app._currentUser.Id);

            _name = Staff.Name;
            _surname = Staff.Surname;
            _jmbg = Staff.JMBG;
            _birthPlace = Staff.BirthPlace.Name + ", " + Staff.BirthPlace.PostNumber + ", " + Staff.BirthPlace.Country.Name;


            InitializeComponent();
            datumRodjenja.SelectedDate = Staff.BirthDate.Date;
            if (Staff.Gender == Gender.Male)
                male.IsChecked = true;
            else
                female.IsChecked = true;
            DataContext = this;
        }
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

        private Staff createEmployee()
        {
            Staff retVal = Staff;
            retVal.Name = _name;
            retVal.Surname = _surname;
            retVal.JMBG = _jmbg;
            retVal.BirthDate = ((DateTime)datumRodjenja.SelectedDate).Date;
            retVal.Gender = (male.IsChecked == true) ? Gender.Male : Gender.Female;
            string[] partsBirthPlace = _birthPlace.Split(',');
            string place = partsBirthPlace[0].Trim();
            int number = int.Parse(partsBirthPlace[1].Trim());
            string country = partsBirthPlace[2].Trim();
            City birthPlace = new City() { country = new Country() { Name = country }, Name = place, PostNumber = number };
            retVal.BirthPlace = birthPlace;
            return retVal;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene naloga?",
                                                   new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }
        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEditEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new mainPage(), false);
        }

        private void editNext_Click(object sender, RoutedEventArgs e)
        {
            if (!isNameFulfilled())
            {
                ShowError(MANDATORY_NAME_FIELD);
            }
            else if (!isSurnameFulfilled())
            {
                ShowError(MANDATORY_SURNAME_FIELD);
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
                MainWindow.AppWindow.navigateWithTitleChange(new UpravnikNext(createEmployee()), false);
            }
        }
    }
}
