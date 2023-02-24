using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Users;
using Model.Users.UserBuilder;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAddThird.xaml
    /// </summary>
    public partial class ZaposleniAddThird : Page
    {
        private SecretaryBuilder secretaryBuilder;
        private readonly IUserController _userController;

        public ZaposleniAddThird(SecretaryBuilder secretaryBuilder)
        {
            var app = Application.Current as App;
            _userController = app.userController;
            this.secretaryBuilder = secretaryBuilder;
            InitializeComponent();
        }

        private void doctor_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniView.ZaposleniAddLekar(createDoctorBuilder()), false);
        }

        private DoctorBuilder createDoctorBuilder()
        {
            DoctorBuilder retVal = new DoctorBuilder();
            Model.Users.Secretary secretary = secretaryBuilder.getResult();
            retVal.SetName(secretary.Name);
            retVal.SetSurname(secretary.Surname);
            retVal.SetUserType(Usertype.Doctor);
            retVal.SetResidence(secretary.Residence);
            retVal.SetPassword(secretary.Password);
            retVal.SetGender(secretary.Gender);
            retVal.SetEducationLevel(secretary.EducationLevel);
            retVal.SetJMBG(secretary.JMBG);
            retVal.SetContact(secretary.Contact);
            retVal.SetBirthDate(secretary.BirthDate);
            retVal.SetBirthPlace(secretary.BirthPlace);
            retVal.SetBiography("");
            retVal.SetPicture(secretary.Picture);
            return retVal;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void secretary_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Dodavanje zaposlenog", "Da li ste sigurni da zelite da dodate zaposlenog?",
                                                                new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
            dialog.Visibility = Visibility.Visible;
        }

        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            if (_userController.Create(secretaryBuilder.getResult()) == null)
            {
                ShowError("Uneti JMBG nije jedinstven! Vec postoji u sistemu!");
            }
            else
            {
                secretaryBuilder.Reset();
                MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
            }
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
    }
}
