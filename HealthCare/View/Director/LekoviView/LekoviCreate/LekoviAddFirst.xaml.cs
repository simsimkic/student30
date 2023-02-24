using Controller.OtherDataController;
using HCIProjekat.LekoviView;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.Drug;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.View.LekoviView.LekoviCreate
{
    /// <summary>
    /// Interaction logic for LekoviAddFirst.xaml
    /// </summary>
    public partial class LekoviAddFirst : Page
    {
        private static readonly Regex _decimalRegex = new Regex(@"[^0-9,]+$");
        private const string INVALID_DRUG_QUANTITY_ERROR_MESSAGE = "Uneta kolicina supstance nije validna!";
        private const string MANDATORY_NAME_FIELD = "Naziv leka je obavezno polje!";
        private const string MANDATORY_MANUFACTURER_FIELD = "Naziv proizvodjaca je obavezno polje!";
        private const string MANDATORY_DRUGFORMAT_FIELD = "Format leka je obavezno polje!";
        private const string MANDATORY_INGREDIENTS_FIELD = "Sastojci leka se moraju uneti!";

        private string _drugName;
        private string _drugManufacturer;
        private double _drugAmount;

        private readonly IAllergyController _allergyController;
        private readonly IIngredientController _ingredientController;
        public ObservableCollection<Allergen> Allergens { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        private List<Allergen> _selectedAllergens;
        private List<Ingredient> _selectedIngredients;
        public LekoviAddFirst()
        {
            InitializeComponent();
            DataContext = this;

            _selectedAllergens = new List<Allergen>();
            _selectedIngredients = new List<Ingredient>();

            var app = Application.Current as App;
            _allergyController = app.allergyController;
            _ingredientController = app.ingredientController;

            Allergens = new ObservableCollection<Allergen>(_allergyController.GetAll());
            Ingredients = new ObservableCollection<Ingredient>(_ingredientController.GetAll());
        }

        public string DrugName
        {
            get { return _drugName; }
            set
            {
                if (_drugName != value)
                {
                    _drugName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DrugManufacturer
        {
            get { return _drugManufacturer; }
            set
            {
                if (_drugManufacturer != value)
                {
                    _drugManufacturer = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }


        private void addNext_Click(object sender, RoutedEventArgs e)
        {

            if (!isDrugNameFulfilled())
            {
                ShowError(MANDATORY_NAME_FIELD);
            }
            else if (!isDrugManufacturerFulfilled())
            {
                ShowError(MANDATORY_MANUFACTURER_FIELD);
            }
            else if (!isValidDrugQuantity())
            {
                ShowError(INVALID_DRUG_QUANTITY_ERROR_MESSAGE);
            }
            else if (!isDrugFormatSelected())
            {
                ShowError(MANDATORY_DRUGFORMAT_FIELD);
            }
            else if (!isDrugIngredientsSelected())
            {
                ShowError(MANDATORY_INGREDIENTS_FIELD);
            }
            else
            {
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviAddSecond(CreateDrug()), false);
            }

        }

        private bool isDrugFormatSelected()
        {
            return drugFormat.SelectedItem != null;
        }

        private bool isDrugIngredientsSelected()
        {
            return _selectedIngredients.Count > 0;
        }

        private bool isDrugManufacturerFulfilled()
        {
            return _drugManufacturer != "" && _drugManufacturer != null;
        }

        private bool isDrugNameFulfilled()
        {
            return _drugName != "" && _drugName != null;
        }

        private Drug CreateDrug()
        {
            return new Drug() { Name = _drugName, Manufacturer = _drugManufacturer, allergens = _selectedAllergens, ingredients = _selectedIngredients, Quantity = _drugAmount, Format = (Formatdrug)drugFormat.SelectedItem };
        }

        private bool isValidDrugQuantity()
        {
            if (!Double.TryParse(DrugAmount.Text, out double drugAmount))
            {
                return false;
            }
            else
            {
                _drugAmount = drugAmount;
                return true;
            }
        }

        private void Sastojici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void drugAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
             => e.Handled = !isTextDouble(e.Text);

        private bool isTextDouble(string input) => !_decimalRegex.IsMatch(input);

        private void drugAmount_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextDouble(input)) e.CancelCommand();
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

        private void Alergen_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            _selectedAllergens.Add(new Allergen() { Code = (int)checkBox.CommandParameter, Name = (string)checkBox.Content });
        }

        private void Alergen_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            foreach (var v in _selectedAllergens)
                if (v.Code == (int)checkBox.CommandParameter)
                {
                    _selectedAllergens.Remove(v);
                    break;
                }
        }

        private void Sastojak_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            _selectedIngredients.Add(new Ingredient() { Id = (int)checkBox.CommandParameter, Name = (string)checkBox.Content });

        }

        private void Sastojak_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            foreach (var v in _selectedIngredients)
                if (v.Id == (int)checkBox.CommandParameter)
                {
                    _selectedIngredients.Remove(v);
                    break;
                }
        }
    }
}
