using Controller.DrugController;
using HCIProjekat.LekoviView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView.DataView;
using HealthCare;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviDetails.xaml
    /// </summary>
    public partial class LekoviDetailsOdbijeni : Page
    {
        private UserControl _drug;
        private readonly IDrugController drugController;
        public LekoviDetailsOdbijeni(UserControl Drug)
        {
            var app = Application.Current as App;
            drugController = app.drugControler;
            _drug = Drug;
            InitializeComponent();

            DataContext = this;
        }

        public UserControl Drug
        {
            get { return _drug; }
            set { _drug = value; }
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }
        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void change_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new LekoviEditFirst(Drug), false);
        }

        private void obrisi_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Brisanje leka", "Da li ste sigurni da zelite da obrisete lek?",
                                                    new RoutedEventHandler(withdrawDeleteDrugEvent), new RoutedEventHandler(deleteDrugEvent)));
            dialog.Visibility = Visibility.Visible;
        }

        private void deleteDrugEvent(object sender, RoutedEventArgs e)
        {
            drugController.Delete(drugController.GetFromID(((DrugItem)Drug).Id));
            dialog.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }

        private void withdrawDeleteDrugEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
    }
}
