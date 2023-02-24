using Controller.OtherDataController;
using Controller.ReportController;
using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using Model.DataReport;
using Model.Users;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.ReportView
{
    /// <summary>
    /// Interaction logic for FilterZauzetostiLekara.xaml
    /// </summary>
    public partial class FilterZauzetostiLekara : Page
    {
        private const string MANDATORY_DATE_FIELD = "Datum od i datum do su obavezna polja!";
        private const string INVALID_DATE_RANGE = "Datum od mora biti manji od datuma do!";


        private readonly IUserController userController;
        private readonly IWorkPlaceController _workPlaceController;
        private readonly IReportController _reportController;

        public static ObservableCollection<User> Staff { get; set; }
        public ObservableCollection<WorkPlace> WorkPlaces { get; set; }

        public FilterZauzetostiLekara()
        {
            var app = Application.Current as App;
            userController = app.userController;
            _workPlaceController = app.workPlaceController;
            _reportController = app.reportController;

            WorkPlaces = new ObservableCollection<WorkPlace>(_workPlaceController.GetAll());
            Staff = new ObservableCollection<User>(userController.GetAllStaff().Where(x => x.GetType().Equals(typeof(Doctor))));
            InitializeComponent();

            DataContext = this;
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {

            if (!isDateFromFulfilled())
            {
                ShowError(MANDATORY_DATE_FIELD);
            }
            else if (!isDateToFulfilled())
            {
                ShowError(MANDATORY_DATE_FIELD);
            }
            else if (!validDatesRange())
            {
                ShowError(INVALID_DATE_RANGE);
            }
            else
            {
                DoctorOccupationReport report = new DoctorOccupationReport() { DateFrom = dateFrom.SelectedDate.Value.Date, DateTo = dateTo.SelectedDate.Value.Date };
                if (radnoMesto.SelectedItem == null)
                    report.WorkPlace = null;
                else
                    report.WorkPlace = (WorkPlace)radnoMesto.SelectedItem;
                if (doctor.SelectedItem == null)
                    report.Id = -1;
                else
                    report.Id = ((Doctor)doctor.SelectedItem).Id;

                MainWindow.AppWindow.navigateToRootPage(new ZauzetostLekara(_reportController.GetReportForDoctorOccupation(report)), false);
            }
        }

        private bool validDatesRange()
        {
            return dateFrom.SelectedDate.Value.Date < dateTo.SelectedDate.Value.Date;
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
        private bool isDateToFulfilled()
        {
            return dateTo.SelectedDate != null;
        }

        private bool isDateFromFulfilled()
        {
            return dateFrom.SelectedDate != null;
        }
    }
}
