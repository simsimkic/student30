using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for DodavanjeRadnogVremena.xaml
    /// </summary>
    public partial class DodavanjeRadnogVremena : Page
    {
        public UserControl Employee { get; set; }
        private DateTime _dateFrom;
        private DateTime _dateTo;

        private User user;

        private readonly IUserController _userController;
        private readonly IWorkTimeController _workTimeController;
        private static readonly Regex _intRegex = new Regex(@"[^0-9]+$");
        private static readonly Regex _timeRegex = new Regex(@"^[0-9]{1,2}$");

        private const string MANDATORY_INPUT_FIELD = "Mora se izabrati bar jedan dan!";
        private const string MANDATORY_TIME_FIELD = "Mora se popuniti radno vreme za izabrani dan!";
        private const string INVALID_DAYS_SELECTED = "Za unete dane je vec dodato radno vreme!";
        private const string INVALID_TIME_INPUT = "Uneto vreme nije validno!";
        private const string INVALID_DAYS_INPUT = "Izabrani dani se ne nalaze u izabranom opsegu datuma!";
        public DodavanjeRadnogVremena(DateTime dateFrom, DateTime dateTo, UserControl Employee)
        {
            var app = Application.Current as App;
            _userController = app.userController;
            _workTimeController = app.workTimeController;

            user = _userController.GetFromID(((EmployeeItem)Employee).Id);

            this.Employee = Employee;
            _dateFrom = dateFrom.Date;
            _dateTo = dateTo.Date;

            InitializeComponent();
            datumOd.Text = dateFrom.ToShortDateString();
            datumDo.Text = dateTo.ToShortDateString();

            DataContext = this;
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

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.MainFrame.Navigate(MainWindow.undoPages[MainWindow.undoPages.Count - 1]);
        }

        private void confirmaAddingWorkTimeEvent(object sender, RoutedEventArgs e)
        {
            if (!areSelectedDaysInRange())
            {
                ShowError(INVALID_DAYS_INPUT);
            }
            else
            {
                if (_workTimeController.Create(createWorkTime()) == null)
                    ShowError(INVALID_DAYS_SELECTED);
                else
                {
                    MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
                    MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
                    MainWindow.AppWindow.navigateWithTitleChange(new RadnoVreme(Employee), false);
                }
            }
        }

        private bool areSelectedDaysInRange()
        {
            if (sviDani.IsChecked == true)
                return true;

            DateTime start = _dateFrom.Date;

            while (start <= _dateTo.Date)
            {
                if (ponedeljak.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Monday)
                        return true;
                if (utorak.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Tuesday)
                        return true;
                if (sreda.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Wednesday)
                        return true;
                if (cetvrtak.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Thursday)
                        return true;
                if (petak.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Friday)
                        return true;
                if (subota.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Saturday)
                        return true;
                if (nedelja.IsChecked == true)
                    if (start.DayOfWeek == DayOfWeek.Sunday)
                        return true;

                start = start.Date.AddDays(1);
            }
            return false;
        }

        private WorkTime createWorkTime()
        {
            WorkTime wt = new WorkTime() { StartDate = _dateFrom.Date, EndDate = _dateTo.Date, staff = new Staff() { Id = user.Id } };
            List<WorkDay> workDays = new List<WorkDay>();
            if (sviDani.IsChecked == true)
            {
                workDays.Add(new WorkDay() { Day = Days.Monday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
                workDays.Add(new WorkDay() { Day = Days.Tuesday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
                workDays.Add(new WorkDay() { Day = Days.Wednesday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
                workDays.Add(new WorkDay() { Day = Days.Thursday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
                workDays.Add(new WorkDay() { Day = Days.Friday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
                workDays.Add(new WorkDay() { Day = Days.Saturday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
                workDays.Add(new WorkDay() { Day = Days.Sunday, StartTime = int.Parse(sdOd.Text), EndTime = int.Parse(sdDo.Text) });
            }
            else
            {
                if (ponedeljak.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Monday, StartTime = int.Parse(pOd.Text), EndTime = int.Parse(pDo.Text) });


                if (utorak.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Tuesday, StartTime = int.Parse(uOd.Text), EndTime = int.Parse(uDo.Text) });

                if (sreda.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Wednesday, StartTime = int.Parse(sOd.Text), EndTime = int.Parse(sDo.Text) });

                if (cetvrtak.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Thursday, StartTime = int.Parse(cOd.Text), EndTime = int.Parse(cDo.Text) });

                if (petak.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Friday, StartTime = int.Parse(petOd.Text), EndTime = int.Parse(petDo.Text) });

                if (subota.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Saturday, StartTime = int.Parse(subOd.Text), EndTime = int.Parse(subDo.Text) });

                if (nedelja.IsChecked == true)
                    workDays.Add(new WorkDay() { Day = Days.Sunday, StartTime = int.Parse(nedOd.Text), EndTime = int.Parse(nedDo.Text) });
            }

            wt.workDay = workDays;
            return wt;
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od dodavanja radnog vremena", "Da li ste sigurni da zelite da odustanete od dodavanja radnog vremena?",
                                                    new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void addWorkTime_Click(object sender, RoutedEventArgs e)
        {
            if (!isAnithingChecked())
            {
                ShowError(MANDATORY_INPUT_FIELD);
            }
            else if (!isRequiredFieldFulfilled())
            {
                ShowError(MANDATORY_TIME_FIELD);
            }
            else if (!isFulfilledFieldValid())
            {
                ShowError(INVALID_TIME_INPUT);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Dodavanje radnog vremena", "Da li ste sigurni da zelite da dodate radno vreme?",
                                                        new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmaAddingWorkTimeEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isFulfilledFieldValid()
        {
            if (sviDani.IsChecked == true)
                return _timeRegex.IsMatch(sdOd.Text) && _timeRegex.IsMatch(sdDo.Text) && (int.Parse(sdOd.Text) < int.Parse(sdDo.Text)) && inTimeRange(sdOd.Text, sdDo.Text);

            if (ponedeljak.IsChecked == true && (!_timeRegex.IsMatch(pOd.Text) || !_timeRegex.IsMatch(pDo.Text) || !(int.Parse(pOd.Text) < int.Parse(pDo.Text)) || !inTimeRange(pOd.Text, pDo.Text)))
                return false;

            if (utorak.IsChecked == true && (!_timeRegex.IsMatch(uOd.Text) || !_timeRegex.IsMatch(uDo.Text) || !(int.Parse(uOd.Text) < int.Parse(uDo.Text)) || !inTimeRange(uOd.Text, uDo.Text)))
                return false;

            if (sreda.IsChecked == true && (!_timeRegex.IsMatch(sOd.Text) || !_timeRegex.IsMatch(sOd.Text) || !(int.Parse(sOd.Text) < int.Parse(sDo.Text)) || !inTimeRange(sOd.Text, sDo.Text)))
                return false;

            if (cetvrtak.IsChecked == true && (!_timeRegex.IsMatch(cOd.Text) || !_timeRegex.IsMatch(cDo.Text) || !(int.Parse(cOd.Text) < int.Parse(cDo.Text)) || !inTimeRange(cOd.Text, cDo.Text)))
                return false;

            if (petak.IsChecked == true && (!_timeRegex.IsMatch(petOd.Text) || !_timeRegex.IsMatch(petDo.Text) || !(int.Parse(petOd.Text) < int.Parse(petDo.Text)) || !inTimeRange(petOd.Text, petDo.Text)))
                return false;

            if (subota.IsChecked == true && (!_timeRegex.IsMatch(subOd.Text) || !_timeRegex.IsMatch(subDo.Text) || !(int.Parse(subOd.Text) < int.Parse(subDo.Text)) || !inTimeRange(subOd.Text, subDo.Text)))
                return false;

            if (nedelja.IsChecked == true && (!_timeRegex.IsMatch(nedOd.Text) || !_timeRegex.IsMatch(nedDo.Text) || !(int.Parse(nedOd.Text) < int.Parse(nedDo.Text)) || !inTimeRange(nedOd.Text, nedDo.Text)))
                return false;

            return true;
        }

        private bool inTimeRange(string od, string to)
        {
            return int.Parse(od) >= 0 && int.Parse(od) <= 24 && int.Parse(to) >= 0 && int.Parse(to) <= 24;
        }

        private bool isRequiredFieldFulfilled()
        {
            if (sviDani.IsChecked == true)
                return sdOd.Text != "" && sdDo.Text != "";

            if (ponedeljak.IsChecked == true && (pOd.Text == "" || pDo.Text == ""))
                return false;

            if (utorak.IsChecked == true && (uOd.Text == "" || uDo.Text == ""))
                return false;

            if (sreda.IsChecked == true && (sOd.Text == "" || sDo.Text == ""))
                return false;

            if (cetvrtak.IsChecked == true && (cOd.Text == "" || cDo.Text == ""))
                return false;

            if (petak.IsChecked == true && (petOd.Text == "" || petDo.Text == ""))
                return false;

            if (subota.IsChecked == true && (subOd.Text == "" || subDo.Text == ""))
                return false;

            if (nedelja.IsChecked == true && (nedOd.Text == "" || nedDo.Text == ""))
                return false;

            return true;
        }

        private bool isAnithingChecked()
        {
            return sviDani.IsChecked == true || ponedeljak.IsChecked == true || utorak.IsChecked == true || sreda.IsChecked == true || cetvrtak.IsChecked == true || petak.IsChecked == true ||
                            subota.IsChecked == true || nedelja.IsChecked == true;
        }

        private void sviDani_Checked(object sender, RoutedEventArgs e)
        {
            ponedeljakRow.Visibility = Visibility.Collapsed;
            utorakRow.Visibility = Visibility.Collapsed;
            sredaRow.Visibility = Visibility.Collapsed;
            cetvrtakRow.Visibility = Visibility.Collapsed;
            petakRow.Visibility = Visibility.Collapsed;
            subotaRow.Visibility = Visibility.Collapsed;
            nedeljaRow.Visibility = Visibility.Collapsed;
        }

        private void sviDani_Unchecked(object sender, RoutedEventArgs e)
        {
            ponedeljakRow.Visibility = Visibility.Visible;
            utorakRow.Visibility = Visibility.Visible;
            sredaRow.Visibility = Visibility.Visible;
            cetvrtakRow.Visibility = Visibility.Visible;
            petakRow.Visibility = Visibility.Visible;
            subotaRow.Visibility = Visibility.Visible;
            nedeljaRow.Visibility = Visibility.Visible;
        }
        private bool isTextInt(string input) => !_intRegex.IsMatch(input);

        private void sdOd_PreviewTextInput(object sender, TextCompositionEventArgs e)
                            => e.Handled = !isTextInt(e.Text);
        private void sdOd_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!isTextInt(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
