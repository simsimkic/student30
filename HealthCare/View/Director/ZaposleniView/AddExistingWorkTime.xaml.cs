using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ZaposleniView.DataView;
using HCIProjekat.ZaposleniView;
using HealthCare;
using HealthCare.View.Director.ZaposleniView.Converter;
using Model.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.View.ZaposleniView
{
    /// <summary>
    /// Interaction logic for AddExistingWorkTime.xaml
    /// </summary>
    public partial class AddExistingWorkTime : Page
    {
        public ObservableCollection<UserControl> existingDates { get; set; }
        public UserControl Employee { get; set; }
        private readonly IWorkTimeController _workTimeController;

        public AddExistingWorkTime(UserControl Employee)
        {
            var app = Application.Current as App;
            _workTimeController = app.workTimeController;

            this.Employee = Employee;
            existingDates = new ObservableCollection<UserControl>(ExistingWorkTimItemConverter.ConvertWorkTimeListToWorkTimeViewList
                                                                    (GetUniqueWorkTime(_workTimeController.GetFutureWorkTime().ToList())));

            InitializeComponent();
            DataContext = this;
        }

        private IList<WorkTime> GetUniqueWorkTime(List<WorkTime> workTimes)
        {
            List<WorkTime> retVal = new List<WorkTime>();

            foreach (WorkTime w in workTimes)
            {
                if (retVal.Count == 0)
                    retVal.Add(w);

                bool addForbbiden = false;
                foreach (WorkTime t in retVal)
                {
                    if (t.StartDate.Date == w.StartDate.Date && t.EndDate.Date == w.EndDate.Date)
                    {
                        if (IsSameWorkDays(w.workDay, t.workDay))
                        {
                            addForbbiden = true;
                            break;
                        }
                    }
                }
                if (!addForbbiden)
                    retVal.Add(w);
            }

            return retVal;
        }

        private bool IsSameWorkDays(List<WorkDay> fullList, List<WorkDay> uniqueList)
        {
            foreach (WorkDay wD in fullList)
            {
                bool found = false;
                foreach (WorkDay wDU in uniqueList)
                {
                    if ((int)wD.Day == (int)wDU.Day && wD.StartTime == wDU.StartTime && wD.EndTime == wDU.EndTime)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;
            }

            return true;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.MainFrame.Navigate(MainWindow.undoPages[MainWindow.undoPages.Count - 1]);
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void confirmaAddingWorkTimeEvent(object sender, RoutedEventArgs e)
        {
            WorkTime wt = _workTimeController.GetFromID(((ExistingWorkTimeItem)workTimeList.SelectedItem).Id);
            wt.staff = new Staff() { Id = ((EmployeeItem)Employee).Id };
            if (_workTimeController.Create(wt) == null)
            {
                ShowError("Nije moguce dodati izabrano radno vreme zbog preklapanja sa postojecim ili zbog odsustva zaposlenog!");
            }
            else
            {
                MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
                MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
                MainWindow.AppWindow.navigateWithTitleChange(new RadnoVreme(Employee), false);
            }
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od dodavanja radnog vremena", "Da li ste sigurni da zelite da odustanete od dodavanja radnog vremena?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void addWorkTime_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Dodavanje radnog vremena", "Da li ste sigurni da zelite da dodate radno vreme?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmaAddingWorkTimeEvent)));
            dialog.Visibility = Visibility.Visible;
        }
    }
}
