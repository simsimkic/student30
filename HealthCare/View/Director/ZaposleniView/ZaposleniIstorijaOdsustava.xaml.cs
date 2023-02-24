using Controller.UserControllers;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using HealthCare.View.Director.ZaposleniView.Converter;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniIstorijaOdsustava.xaml
    /// </summary>
    public partial class ZaposleniIstorijaOdsustava : Page
    {
        public ObservableCollection<UserControl> employeeAbsence { get; set; }
        private readonly IAbsenceController _absenceController;
        private readonly IUserController _userController;

        public UserControl Employee { get; set; }
        public ZaposleniIstorijaOdsustava(UserControl Employee)
        {
            var app = Application.Current as App;
            _userController = app.userController;
            _absenceController = app.absenceController;

            this.Employee = Employee;
            employeeAbsence = new ObservableCollection<UserControl>(AbsenceHistoryItemConverter.ConvertAbsenceListToAbsenceViewList
                                    (_absenceController.GetEmployeesAbsenceHistory(_userController.GetFromID(((EmployeeAbsenceRequestItem)Employee).Id)).ToList()));

            InitializeComponent();

            DataContext = this;
        }
    }
}
