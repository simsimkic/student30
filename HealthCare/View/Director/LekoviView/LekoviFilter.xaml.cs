using HCIProjekat.View.ConfirmationDialogsView;
using Model.DataFiltration;
using Model.Drug;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviFilter.xaml
    /// </summary>
    public partial class LekoviFilter : Page
    {
        private int _amountFrom;
        private int _amountTo;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private const string INVALID_DRUG_AMOUNT_ERROR_MESSAGE = "Uneta kolicina leka nije validna!";
        private const string INVALID_DRUG_AMOUNT_RANGE_ERROR_MESSAGE = "Kolicina od mora biti manja od kolicine do!";

        public LekoviFilter()
        {
            InitializeComponent();
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!anythingFulFilled())
            {
                MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
                return;
            }
            if (isAmountFromFulFilled())
            {
                if (!isAmountFromValid())
                {
                    ShowError(INVALID_DRUG_AMOUNT_ERROR_MESSAGE);
                    return;
                }
            }
            if (isAmountToFulFilled())
            {
                if (!isAmountToValid())
                {
                    ShowError(INVALID_DRUG_AMOUNT_ERROR_MESSAGE);
                    return;
                }
                if (isAmountFromFulFilled())
                {
                    if (_amountFrom > _amountTo)
                    {
                        ShowError(INVALID_DRUG_AMOUNT_RANGE_ERROR_MESSAGE);
                        return;
                    }
                }
            }

            MainWindow.AppWindow.navigateToRootPage(new Lekovi(false, createFilter()), false);
        }

        private DrugFilter createFilter()
        {
            int stat = -1;
            if (status.SelectedItem != null)
            {
                stat = (int)((DrugStatus)status.SelectedItem);
            }
            return new DrugFilter() { AmountFrom = _amountFrom, AmountTo = _amountTo, Manufacturer = drugManufacturer.Text, Name = drugName.Text, Status = stat };
        }

        private bool anythingFulFilled()
        {
            return (amountTo.Text != "" && amountTo.Text != null) || (amountFrom.Text != "" && amountFrom.Text != null) ||
                    (drugManufacturer.Text != "" && drugManufacturer.Text != null) || (drugName.Text != "" && drugName.Text != null)
                    || (status.SelectedItem != null);
        }

        private bool isAmountToFulFilled()
        {
            return amountTo.Text != "" && amountTo.Text != null;
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawPurchaseEvent));
        }

        private void withdrawPurchaseEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private bool isAmountToValid()
        {
            if (!int.TryParse(amountTo.Text, out int drugsAmount))
            {
                return false;
            }
            else
            {
                _amountTo = drugsAmount;
                return true;
            }
        }
        private bool isAmountFromValid()
        {
            if (!int.TryParse(amountFrom.Text, out int drugsAmount))
            {
                return false;
            }
            else
            {
                _amountFrom = drugsAmount;
                return true;
            }
        }

        private bool isAmountFromFulFilled()
        {
            return amountFrom.Text != "" && amountFrom.Text != null;
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
