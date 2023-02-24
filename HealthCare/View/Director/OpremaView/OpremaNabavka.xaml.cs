using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.OpremaView.DataView;
using HealthCare;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaNabavka.xaml
    /// </summary>
    public partial class OpremaNabavka : Page
    {
        public UserControl EquipmentItem { get; set; }

        private int _equipmentAmount;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE = "Uneta kolicina opreme nije validna!";
        private const string MANDATORY_INVENTORY_AMOUNT_ERROR_MESSAGE = "Kolicina opreme za nabavku je obavezno polje i ne moze biti 0!";
        public readonly IInventoryController _inventoryController;

        public OpremaNabavka(UserControl EquipmentItem)
        {
            var app = Application.Current as App;
            _inventoryController = app.inventoryController;

            this.EquipmentItem = EquipmentItem;
            InitializeComponent();
            DataContext = this;
        }

        private void purchaseConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidInventoryAmount())
            {
                ShowError(INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE);
            }
            else if (!isInventoryAmountFulfilled())
            {
                ShowError(MANDATORY_INVENTORY_AMOUNT_ERROR_MESSAGE);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Nabavka opreme", "Da li ste sigurni da zelite da dodate na stanje unetu kolicinu opreme?",
                                                    new RoutedEventHandler(withdrawPurchaseEvent), new RoutedEventHandler(applyPurchaseEvent)));
                dialog.Visibility = Visibility.Visible;
            }

        }

        private bool isInventoryAmountFulfilled()
        {
            return _equipmentAmount != 0;
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawPurchaseEvent));
        }


        private bool isValidInventoryAmount()
        {
            if (!int.TryParse(amountInventory.Text, out int invAmount))
            {
                return false;
            }
            else
            {
                _equipmentAmount = invAmount;
                return true;
            }
        }
        private void applyPurchaseEvent(object sender, RoutedEventArgs e)
        {
            _inventoryController.AddAmountOfInventory(_inventoryController.GetFromID(((EquipmentItem)EquipmentItem).Id), _equipmentAmount);
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        private void withdrawPurchaseEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void amountInventory_PreviewTextInput(object sender, TextCompositionEventArgs e)
                     => e.Handled = !isTextInt(e.Text);

        private bool isTextInt(string input) => !_intRegex.IsMatch(input);
        private void amountInventory_Pasting(object sender, DataObjectPastingEventArgs e)
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
