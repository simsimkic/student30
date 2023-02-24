using Controller.DrugController;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView.DataView;
using HealthCare;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviNabavka.xaml
    /// </summary>
    public partial class LekoviNabavka : Page
    {
        public UserControl Drug { get; set; }

        private int _drugAmount;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_DRUG_AMOUNT_ERROR_MESSAGE = "Uneta kolicina leka nije validna!";
        private const string MANDATORY_DRUG_AMOUNT_ERROR_MESSAGE = "Kolicina leka za nabavku je obavezno polje i ne moze biti 0!";

        private readonly IDrugController drugController;

        public LekoviNabavka(UserControl Drug)
        {
            this.Drug = Drug;

            var app = Application.Current as App;
            drugController = app.drugControler;

            InitializeComponent();
            DataContext = this;
        }

        private void purchaseConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidDrugAmount())
            {
                ShowError(INVALID_DRUG_AMOUNT_ERROR_MESSAGE);
            }
            else if (!isDrugAmoutFulfilled())
            {
                ShowError(MANDATORY_DRUG_AMOUNT_ERROR_MESSAGE);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Nabavka leka", "Da li ste sigurni da zelite da dodate na stanje unetu kolicinu leka?",
                                                    new RoutedEventHandler(withdrawPurchaseEvent), new RoutedEventHandler(applyPurchaseEvent)));
                dialog.Visibility = Visibility.Visible;
            }

        }

        private bool isDrugAmoutFulfilled()
        {
            return _drugAmount != 0;
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawPurchaseEvent));
        }


        private bool isValidDrugAmount()
        {
            if (!int.TryParse(drugAmount.Text, out int drugsAmount))
            {
                return false;
            }
            else
            {
                _drugAmount = drugsAmount;
                return true;
            }
        }

        private void applyPurchaseEvent(object sender, RoutedEventArgs e)
        {
            drugController.AddAmountOfDrug(drugController.GetFromID(((DrugItem)Drug).Id), _drugAmount);
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }

        private void withdrawPurchaseEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void drugAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
             => e.Handled = !isTextInt(e.Text);

        private bool isTextInt(string input) => !_intRegex.IsMatch(input);

        private void drugAmount_Pasting(object sender, DataObjectPastingEventArgs e)
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
