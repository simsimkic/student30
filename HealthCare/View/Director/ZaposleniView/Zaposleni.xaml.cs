using Controller.DrugController;
using Controller.UserControllers;
using Director.ZaposleniView.Converter;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.HelpWizardView;
using HCIProjekat.View.ZaposleniView.DataView;
using HCIProjekat.ZaposleniView;
using HealthCare;
using Model.DataFiltration;
using Model.Users;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class Zaposleni : Page
    {
        public ObservableCollection<UserControl> Employees { get; set; }
        private readonly IUserController _userController;
        private readonly IDrugController _drugController;

        public Zaposleni(bool filterDissmis = true, StaffFilter staffFilter = null)
        {
            var app = Application.Current as App;
            _userController = app.userController;
            _drugController = app.drugControler;

            InitializeComponent();

            if (!filterDissmis)
            {
                disableFilter.Visibility = Visibility.Visible;
                Employees = new ObservableCollection<UserControl>(EmployeeItemConverter.ConvertEmployeeListToEmployeeViewList(_userController.GetFilteredStaff(staffFilter).ToList()));
            }
            else
            {
                Employees = new ObservableCollection<UserControl>(EmployeeItemConverter.ConvertEmployeeListToEmployeeViewList(_userController.GetAllStaff().ToList()));
            }

            DataContext = this;

        }

        private void listaZaposlenih_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (employeeList.SelectedItem != null)
            {
                setButtonVisibility(Visibility.Visible);
            }
            else
            {
                setButtonVisibility(Visibility.Collapsed);
            }
        }

        private void setButtonVisibility(Visibility state)
        {
            editEmployee.Visibility = state;
            deleteEmployee.Visibility = state;
            detailEmployee.Visibility = state;

            workTime.Visibility = state;
            absence.Visibility = state;
        }

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniAddFirst(), false);
        }

        private void absence_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniOdsustva(), false);
            if (MainWindow.APP_HELP)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                MainWindow.AppWindow.HelpFrame.Navigate(new AbsenceHelpFirst());
            }
        }

        private void workTime_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new RadnoVreme((EmployeeItem)employeeList.SelectedItem), false);
            if (MainWindow.APP_HELP)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                MainWindow.AppWindow.HelpFrame.Navigate(new CalendarHelp());
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniFilter(), false);
        }

        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            dialog.Children.Add(new ConfirmDialog("Brisanje zaposlenog",
                            "Da li ste sigurni da zelite da izbrisete izabranog zaposlenog?",
                            new RoutedEventHandler(withdrawDeleteEvent), new RoutedEventHandler(deleteEmployeeEvent)));
            dialog.Visibility = Visibility.Visible;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawDeleteEvent));
        }
        private void deleteEmployeeEvent(object sender, RoutedEventArgs e)
        {
            User user = _userController.GetFromID(((EmployeeItem)employeeList.SelectedItem).Id);
            if (_userController.Delete(user) == null)
            {
                ShowError("Izabrani doktor ima zakazane preglede! Brisanje je moguce nakon zavrsetka pregleda!");
            }
            else
            {
                dialog.Visibility = Visibility.Collapsed;
                if (_drugController.GetDrugByDoctorWhoApprovesIt(user).Count() > 0)
                {
                    MainWindow.AppWindow.HelpFrame.Navigate(new ChangeWhoApprovesDrugDialog(ChangeWhoApprovesDrugEvent, user));
                    MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
                }
            }
        }

        private void ChangeWhoApprovesDrugEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);

        }

        private void withdrawDeleteEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
                if (((EmployeeItem)employeeList.SelectedItem).WorkPlace.Equals("Doktor"))
                    MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniDetailsFirst((EmployeeItem)employeeList.SelectedItem), false);
                else
                    MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniDetailsSekretar((EmployeeItem)employeeList.SelectedItem), false);
            }
        }

        private void detailEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            if (((EmployeeItem)employeeList.SelectedItem).WorkPlace.Equals("Doktor"))
                MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniDetailsFirst((EmployeeItem)employeeList.SelectedItem), false);
            else
                MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniDetailsSekretar((EmployeeItem)employeeList.SelectedItem), false);
        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniEditFirst((EmployeeItem)employeeList.SelectedItem), false);
        }

        private void disableFilter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }
    }
}
