using Controller.OtherDataController;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.DataFiltration;
using Model.Rooms;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.OpremaView
{
    /// <summary>
    /// Interaction logic for OpremaFilter.xaml
    /// </summary>
    public partial class OpremaFilter : Page
    {

        private int _amountFrom;
        private int _amountTo;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE = "Uneta kolicina opreme nije validna!";
        private const string INVALID_INVENTORY_AMOUNT_RANGE_ERROR_MESSAGE = "Kolicina od mora biti manja od kolicine do!";
        public ObservableCollection<InventoryType> InventoryTypes { get; set; }
        private readonly IInventoryTypeController _inventoryTypeController;
        public OpremaFilter()
        {
            var app = Application.Current as App;
            _inventoryTypeController = app.inventoryTypeController;

            InventoryTypes = new ObservableCollection<InventoryType>(_inventoryTypeController.GetAll());
            InitializeComponent();

            DataContext = this;
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!anythingFulFilled())
            {
                MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
                return;
            }
            if (isAmountFromFulFilled())
            {
                if (!isAmountFromValid())
                {
                    ShowError(INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE);
                    return;
                }

            }
            if (isAmountToFulFilled())
            {
                if (!isAmountToValid())
                {
                    ShowError(INVALID_INVENTORY_AMOUNT_ERROR_MESSAGE);
                    return;
                }
                if (isAmountFromFulFilled())
                {
                    if (_amountFrom > _amountTo)
                    {
                        ShowError(INVALID_INVENTORY_AMOUNT_RANGE_ERROR_MESSAGE);
                        return;
                    }
                }
            }

            MainWindow.AppWindow.navigateToRootPage(new Oprema(false, createFilter()), false);
        }

        private InventoryFilter createFilter()
        {
            return new InventoryFilter() { AmountFrom = _amountFrom, AmountTo = _amountTo, InventoryType = (InventoryType)VrstaOpreme.SelectedItem, Name = inventoryName.Text };
        }

        private bool anythingFulFilled()
        {
            return (amountTo.Text != "" && amountTo.Text != null) || (amountFrom.Text != "" && amountFrom.Text != null) ||
                    (inventoryName.Text != "" && inventoryName.Text != null) || VrstaOpreme.SelectedItem != null;
        }

        private bool isAmountToValid()
        {
            if (!int.TryParse(amountTo.Text, out int invAmount))
            {
                return false;
            }
            else
            {
                _amountTo = invAmount;
                return true;
            }
        }
        private bool isAmountFromValid()
        {
            if (!int.TryParse(amountFrom.Text, out int invAmount))
            {
                return false;
            }
            else
            {
                _amountFrom = invAmount;
                return true;
            }
        }

        private bool isAmountFromFulFilled()
        {
            return amountFrom.Text != "" && amountFrom.Text != null;
        }

        private bool isAmountToFulFilled()
        {
            return amountTo.Text != "" && amountTo.Text != null;
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
