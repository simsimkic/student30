using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for OdbijanjeZahteva.xaml
    /// </summary>
    public partial class OdbijanjeZahteva : Page
    {
        public UserControl Employee { get; set; }
        private readonly IAbsenceController _absenceController;
        private const string MANDATORY_REASON_FIELD = "Razlog odbijanja je obavezno polje!";
        private string _reason;
        public OdbijanjeZahteva(UserControl Employee)
        {
            var app = Application.Current as App;
            _absenceController = app.absenceController;

            this.Employee = Employee;
            InitializeComponent();

            DataContext = this;
        }
        public string Reason
        {
            get { return _reason; }
            set
            {
                if (_reason != value)
                {
                    _reason = value;
                    OnPropertyChanged();
                }
            }
        }
        private void rejectAbsenceEvent(object sender, RoutedEventArgs e)
        {
            _absenceController.RejectAbsence(_absenceController.GetFromID(((EmployeeAbsenceRequestItem)Employee).IdAbsence), _reason);
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniOdsustva(), false);
        }


        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void rejectAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (!isReasonFulfilled())
            {
                ShowError(MANDATORY_REASON_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Odbijanje zahteva za odsutvo", "Da li ste sigurni da zelite da odbijete zahtev za odsustvo?",
                                new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(rejectAbsenceEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private bool isReasonFulfilled()
        {
            return _reason != "" && _reason != null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
