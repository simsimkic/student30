using Controller.DrugController;
using Controller.UserControllers;
using HCIProjekat.LekoviView;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Drug;
using Model.Users;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.LekoviView.LekoviCreate
{
    /// <summary>
    /// Interaction logic for LekoviAddFirst.xaml
    /// </summary>
    public partial class LekoviAddDoctorForValidation : Page
    {
        private const string MANDATORY_DOCTOR_FIELD = "Id doktora se mora izabrati!";
        private const string DRUG_NOT_UNIQUE = "Unet lek vec postoji. Bar jedno polje od 4 (ime, proizvodjac, kolicina supstance i format leka) se mora razlikovati!";
        private Drug Drug { get; set; }
        private readonly IDrugController drugController;
        private readonly IUserController userController;
        public static ObservableCollection<User> Staff { get; set; }


        public LekoviAddDoctorForValidation(Drug Drug)
        {
            this.Drug = Drug;
            var app = Application.Current as App;
            drugController = app.drugControler;
            userController = app.userController;

            Staff = new ObservableCollection<User>(userController.GetAllStaff().Where(x => x.GetType().Equals(typeof(Doctor))));

            InitializeComponent();
            DataContext = this;
        }

        private void redirectToDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!isDoctorSelected())
            {
                ShowError(MANDATORY_DOCTOR_FIELD);
            }
            else
            {

                dialog.Children.Add(new ConfirmDialog("Prosledjivanje leka", "Da li ste sigurni da zelite da prosledite lek na odobravanje kod lekara?",
                                                                new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isDoctorSelected()
        {
            return doctor.SelectedItem != null;
        }

        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            Drug.whoApprovesDrug = new Doctor() { Id = ((Staff)doctor.SelectedItem).Id };
            if (drugController.Create(Drug) == null)
            {
                ShowError(DRUG_NOT_UNIQUE);
            }
            else
            {
                MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
            }
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
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od dodavanja", "Da li ste sigurni da zelite da odustanete od dodavanja leka?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent),
                                                    "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

    }
}
