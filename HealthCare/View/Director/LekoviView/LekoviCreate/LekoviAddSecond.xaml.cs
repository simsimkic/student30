using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.LekoviView.LekoviCreate;
using Model.Drug;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.LekoviView
{
    /// <summary>
    /// Interaction logic for LekoviAddSecond.xaml
    /// </summary>
    public partial class LekoviAddSecond : Page
    {

        private const string MANDATORY_PURPOSE_FIELD = "Namena leka je obavezno polje!";
        private const string MANDATORY_GUIDE_FIELD = "Uputstvo za upotrebu je obavezno polje!";
        private const string MANDATORY_INDICATION_FIELD = "Indikacije i mere opreza su obavezno polje!";
        private Drug Drug { get; set; }

        private string _purpose;
        private string _guide;
        private string _indications;


        public LekoviAddSecond(Drug Drug)
        {
            this.Drug = Drug;
            InitializeComponent();
            DataContext = this;
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

        private Drug fulfullRestOfDrugInfo()
        {
            this.Drug.Instruction = _guide;
            this.Drug.Purpose = _purpose;
            this.Drug.SideEffect = _indications;
            this.Drug.Status = DrugStatus.Waiting;
            return this.Drug;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void addNext_Click(object sender, RoutedEventArgs e)
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
                MainWindow.AppWindow.navigateWithTitleChange(new LekoviAddDoctorForValidation(fulfullRestOfDrugInfo()), false);
            }
        }
    }
}
