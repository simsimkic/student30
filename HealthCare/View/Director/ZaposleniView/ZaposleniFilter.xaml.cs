using Controller.OtherDataController;
using Controller.UserControllers;
using HealthCare;
using Model.DataFiltration;
using Model.Users;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniFilter.xaml
    /// </summary>
    public partial class ZaposleniFilter : Page
    {
        private readonly IWorkPlaceController _workPlaceController;
        private readonly IUserController _userController;
        public ObservableCollection<WorkPlace> WorkPlaces { get; set; }
        public ObservableCollection<User> Employees { get; set; }
        public ZaposleniFilter()
        {
            var app = Application.Current as App;
            _workPlaceController = app.workPlaceController;
            _userController = app.userController;

            WorkPlaces = new ObservableCollection<WorkPlace>(_workPlaceController.GetAll());
            Employees = new ObservableCollection<User>(_userController.GetAllStaff());
            InitializeComponent();

            DataContext = this;
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!anythingFulFilled())
            {
                MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
                return;
            }
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(false, createFilter()), false);
        }

        private bool anythingFulFilled()
        {
            return (employeeName.Text != "" && employeeName.Text != null) || (employeeSurname.Text != "" && employeeSurname.Text != null) ||
                    (idZaposlenog.SelectedItem != null) || (tipZaposlenog.SelectedItem != null);
        }

        private StaffFilter createFilter()
        {
            string tip = "";
            if (tipZaposlenog.SelectedItem != null)
                tip = (tipZaposlenog.SelectedIndex == 0) ? "Doktor" : "Sekretar";

            int id = -1;
            if (idZaposlenog.SelectedItem != null)
                id = ((User)idZaposlenog.SelectedItem).Id;

            return new StaffFilter() { Name = employeeName.Text, Surname = employeeSurname.Text, Id = id, StaffType = tip };
        }

    }
}
