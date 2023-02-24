using Controller.DrugController;
using HCIProjekat.LekoviView;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView.DataView;
using HealthCare;
using Model.Drug;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviEditSecond.xaml
    /// </summary>
    public partial class LekoviEditSecond : Page
    {
        private const string MANDATORY_PURPOSE_FIELD = "Namena leka je obavezno polje!";
        private const string MANDATORY_GUIDE_FIELD = "Uputstvo za upotrebu je obavezno polje!";
        private const string MANDATORY_INDICATION_FIELD = "Indikacije i mere opreza su obavezno polje!";
        private const string DRUG_NOT_UNIQUE = "Unet lek vec postoji. Bar jedno polje od 4 (ime, proizvodjac, kolicina supstance i format leka) se mora razlikovati!";


        private UserControl _drug;
        private Drug _newDrug;

        private string _purpose;
        private string _guide;
        private string _indications;

        private readonly IDrugController drugController;

        public LekoviEditSecond(UserControl Drug, Drug newDrug)
        {
            this._drug = Drug;
            this._newDrug = newDrug;
            var app = Application.Current as App;
            drugController = app.drugControler;

            InitializeComponent();

            _purpose = ((DrugItem)Drug).Purpose;
            _guide = ((DrugItem)Drug).Instruction;
            _indications = ((DrugItem)Drug).SideEffect;

            DataContext = this;
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
        public string Purpose
        {
            get { return _purpose; }
            set
            {
                if (_purpose != value)
                {
                    _purpose = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Guide
        {
            get { return _guide; }
            set
            {
                if (_guide != value)
                {
                    _guide = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Indications
        {
            get { return _indications; }
            set
            {
                if (_indications != value)
                {
                    _indications = value;
                    OnPropertyChanged();
                }
            }
        }



        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }

        private void redirectToDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!isPurposeFulfilled())
            {
                ShowError(MANDATORY_PURPOSE_FIELD);
            }
            else if (!isIndicationFulfilled())
            {
                ShowError(MANDATORY_INDICATION_FIELD);
            }
            else if (!isGuideFulfilled())
            {
                ShowError(MANDATORY_GUIDE_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Prosledjivanje leka", "Da li ste sigurni da zelite da prosledite izmene leka na odobravanje kod lekara?",
                                                                new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmAddingEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private void fulfullRestOfDrugInfo()
        {
            this._newDrug.Instruction = _guide;
            this._newDrug.Purpose = _purpose;
            this._newDrug.SideEffect = _indications;
        }

        private bool isGuideFulfilled()
        {
            return _guide != "" && _guide != null;
        }

        private bool isIndicationFulfilled()
        {
            return _indications != "" && _indications != null;
        }

        private bool isPurposeFulfilled()
        {
            return _purpose != "" && _purpose != null;
        }

        private void confirmAddingEvent(object sender, RoutedEventArgs e)
        {
            fulfullRestOfDrugInfo();

            if (drugController.Update(this._newDrug) == null)
            {
                ShowError(DRUG_NOT_UNIQUE);
            }
            else
            {
                MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
