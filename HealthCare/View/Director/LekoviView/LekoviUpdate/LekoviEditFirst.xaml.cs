using Controller.DrugController;
using Controller.OtherDataController;
using HCIProjekat.LekoviView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView.DataView;
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

namespace HCIProjekat.View.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviEditFirst.xaml
    /// </summary>
    public partial class LekoviEditFirst : Page
    {
        private UserControl _drug;

        private string _drugName;
        private string _drugManufacturer;
        private double _drugAmount;

        private static readonly Regex _decimalRegex = new Regex(@"[^0-9,.-]+$");
        private const string INVALID_DRUG_QUANTITY_ERROR_MESSAGE = "Uneta kolicina supstance nije validna!";
        private const string MANDATORY_NAME_FIELD = "Naziv leka je obavezno polje!";
        private const string MANDATORY_MANUFACTURER_FIELD = "Naziv proizvodjaca je obavezno polje!";
        private const string MANDATORY_DRUGFORMAT_FIELD = "Format leka je obavezno polje!";
        private const string MANDATORY_INGREDIENTS_FIELD = "Sastojci leka se moraju uneti!";

        private readonly IAllergyController _allergyController;
        private readonly IIngredientController _ingredientController;
        private readonly IDrugController drugController;

        public ObservableCollection<Allergen> Allergens { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public List<Allergen> _selectedAllergens;
        public List<Ingredient> _selectedIngredients;

        public LekoviEditFirst(UserControl Drug)
        {
            _drug = Drug;
            _selectedIngredients = new List<Ingredient>();
            _selectedAllergens = new List<Allergen>();
            loadIngredients();
            loadAllergens();

            InitializeComponent();

            drugAmount.Text = ((DrugItem)Drug).DrugQuantity.ToString();
            _drugName = ((DrugItem)Drug).DrugName;
            _drugManufacturer = ((DrugItem)Drug).Manufacturer;
            drugFormat.SelectedItem = ((DrugItem)Drug).Format;

            DataContext = this;
            var app = Application.Current as App;
            _allergyController = app.allergyController;
            _ingredientController = app.ingredientController;
            drugController = app.drugControler;

            Allergens = new ObservableCollection<Allergen>(_allergyController.GetAll());
            Ingredients = new ObservableCollection<Ingredient>(_ingredientController.GetAll());
        }

        private void loadAllergens()
        {
            foreach (Allergen allergen in ((DrugItem)Drug).Allergens)
                _selectedAllergens.Add(allergen);
        }

        private void loadIngredients()
        {
            foreach (Ingredient ingredient in ((DrugItem)Drug).Ingredients)
                _selectedIngredients.Add(ingredient);
        }

        public UserControl Drug
        {
            get { return _drug; }
            set
            {
                if (_drug != value)
                {
                    _drug = value;
                    OnPropertyChanged();
                }
            }
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

        private void editNext_Click(object sender, RoutedEventArgs e)
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
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviEditSecond(Drug, createDrug()), false);
            }
        }
        private bool isDrugIngredientsSelected()
        {
            return _selectedIngredients.Count > 0;
        }
        private Drug createDrug()
            => new Drug() { whoApprovesDrug = drugController.GetFromID(((DrugItem)Drug).Id).whoApprovesDrug, 
                                Status = DrugStatus.Waiting,
                                Id = ((DrugItem)Drug).Id,
                                Amount = ((DrugItem)Drug).Amount,
                                ingredients = _selectedIngredients,
                                allergens = _selectedAllergens,
                                Name = _drugName,
                                Manufacturer = _drugManufacturer,
                                Quantity = _drugAmount,
                                Format = (Formatdrug)drugFormat.SelectedItem };
        

        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }

        private bool isDrugFormatSelected()
        {
            return drugFormat.SelectedItem != null;
        }

        private bool isDrugManufacturerFulfilled()
        {
            return _drugManufacturer != "" && _drugManufacturer != null;
        }

        private bool isDrugNameFulfilled()
        {
            return _drugName != "" && _drugName != null;
        }

        private bool isValidDrugQuantity()
        {
            if (!Double.TryParse(drugAmount.Text, out double drugsAmount))
            {
                return false;
            }
            else
            {
                _drugAmount = drugsAmount;
                return true;
            }
        }

        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene leka?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEditingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
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

        private void Sastojak_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (!ingredientExist((int)checkBox.CommandParameter))
                _selectedIngredients.Add(new Ingredient() { Id = (int)checkBox.CommandParameter, Name = (string)checkBox.Content });
        }

        private bool ingredientExist(int id)
        {
            foreach (Ingredient ingredient in _selectedIngredients)
                if (ingredient.Id == id)
                {
                    return true;
                }
            return false;
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

        private void Alergen_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (!alergenExist((int)checkBox.CommandParameter))
                _selectedAllergens.Add(new Allergen() { Code = (int)checkBox.CommandParameter, Name = (string)checkBox.Content });
        }

        private bool alergenExist(int id)
        {
            foreach (Allergen allergen in _selectedAllergens)
                if (allergen.Code == id)
                {
                    return true;
                }
            return false;
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

        private void Sastojak_Loaded(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            foreach (var sastojak in _selectedIngredients)
            {
                if ((int)cb.CommandParameter == sastojak.Id)
                {
                    cb.IsChecked = true;
                    break;
                }
            }
        }

        private void Alergen_Loaded(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            foreach (var alergen in _selectedAllergens)
            {
                if ((int)cb.CommandParameter == alergen.Code)
                {
                    cb.IsChecked = true;
                    break;
                }
            }
        }
    }
}
