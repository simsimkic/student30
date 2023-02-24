using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.HelpWizardView;
using HCIProjekat.View.ZaposleniView;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using HealthCare.Util;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for RadnoVreme.xaml
    /// </summary>
    public partial class RadnoVremeIzmena : Page
    {
        private int countClick = 0;
        public DateTime datumOd;
        public EmployeeWorkDay datumFrom;
        public EmployeeWorkDay datumTo;
        public DateTime datumDo;
        public UserControl Employee { get; set; }


        public ObservableCollection<EmployeeWorkDay> workingDays;
        public ObservableCollection<DateTime> absentes;
        public ObservableCollection<WorkTime> workTimes;
        public ObservableCollection<Absence> employeesAbsents;

        private User user;
        private WorkTime selected;
        private readonly IUserController _userController;
        private readonly IWorkTimeController _workTimeController;
        private readonly IAbsenceController _absenceController;

        public RadnoVremeIzmena(UserControl Employee)
        {
            var app = Application.Current as App;
            _userController = app.userController;
            _workTimeController = app.workTimeController;
            _absenceController = app.absenceController;


            this.Employee = Employee;
            user = _userController.GetFromID(((EmployeeItem)Employee).Id);
            employeesAbsents = new ObservableCollection<Absence>(_absenceController.GetEmployeesFutureAbsence(user));
            workTimes = new ObservableCollection<WorkTime>(_workTimeController.GetWorkTime(user));

            absentes = new ObservableCollection<DateTime>(fulfillAbsentes());
            workingDays = new ObservableCollection<EmployeeWorkDay>(WorkTimeConverter.ConvertWorkTimeListToEmployeeWorkDayList(workTimes.ToList()));
            InitializeComponent();
            Kalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue.Date, DateTime.Now.AddDays(-1).Date));

            editWorkTime.Visibility = (workingDays.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
        }

        private List<DateTime> fulfillAbsentes()
        {
            List<DateTime> ret = new List<DateTime>();
            foreach (var v in employeesAbsents)
            {
                DateTime st = v.StartDate.Date;
                while (st <= v.EndDate.Date)
                {
                    ret.Add(st);
                    st = st.Date.AddDays(1);
                }
            }
            return ret;
        }

        private void Kalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kalendar.SelectedDate != null && countClick < 2)
            {
                DateTime selektovano = Kalendar.SelectedDate.Value;
                showSelectedDateDetails(selektovano);

                if (absentes.Contains(selektovano) && countClick == 0)
                {
                    showErrorMessage("Zaposleni je na odsustvu za izabrani datum");
                    Kalendar.SelectedDates.Clear();
                }
                else
                {

                    if (Kalendar.SelectedDates.Count == 1)
                    {
                        countClick++;
                        if (countClick == 1)
                        {
                            resetViewIfNoErrorOnFirstClick("Datum radnog vremena: ");
                            pocetakRadnogVremena.Text = selektovano.ToShortDateString();
                            krajRadnogVremena.Text = "";
                        }
                        else
                        {
                            resetViewIfNoErrorOnSecondClick("Datum pocetka radnog vremena: ");
                            krajRadnogVremena.Text = selektovano.ToShortDateString();
                        }

                    }
                    else
                    {
                        if (!checkIfDatesAreInRange(Kalendar.SelectedDates[0], Kalendar.SelectedDates[Kalendar.SelectedDates.Count - 1]))
                        {
                            preklapanjeDatuma();
                            Kalendar.SelectedDates.Clear();
                        }
                        else if (checkIfAbsence(Kalendar.SelectedDates[0], Kalendar.SelectedDates[Kalendar.SelectedDates.Count - 1]))
                        {
                            showErrorMessage("Zaposleni je na odsustvu za izabrani datum");
                            Kalendar.SelectedDates.Clear();
                        }
                        else
                        {
                            resetViewIfNoErrorOnSecondClick("Datum pocetka radnog vremena: ");
                            datumOd = Kalendar.SelectedDates[0];
                            datumDo = Kalendar.SelectedDates[Kalendar.SelectedDates.Count - 1];
                            pocetakRadnogVremena.Text = datumOd.ToShortDateString();
                            krajRadnogVremena.Text = datumDo.ToShortDateString();
                        }
                        countClick = 0;
                    }
                    if (countClick == 1)
                    {
                        if (MainWindow.APP_HELP)
                        {
                            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                            MainWindow.AppWindow.HelpFrame.Navigate(new CalendarHelpSecond());
                        }
                        datumOd = (DateTime)Kalendar.SelectedDate;
                        datumDo = (DateTime)Kalendar.SelectedDate;
                    }
                    if (countClick == 2)
                    {
                        MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
                        datumDo = (DateTime)Kalendar.SelectedDate;
                        if (datumOd <= datumDo)
                        {
                            resetViewIfNoErrorOnSecondClick("Datum pocetka radnog vremena: ");
                            selectDatesInCalendar(datumOd, datumDo);
                            if (!checkIfDatesAreInRange(datumOd, datumDo))
                            {
                                preklapanjeDatuma();
                                Kalendar.SelectedDates.Clear();
                            }
                            else if (checkIfAbsence(datumOd, datumDo))
                            {
                                showErrorMessage("Zaposleni je na odsustvu za izabrani datum");
                                Kalendar.SelectedDates.Clear();
                            }

                        }
                        else
                        {
                            showErrorMessage("Datum pocetka radnog vremena mora biti manji od datuma zavrsetka");
                            Kalendar.SelectedDates.Clear();
                        }

                        countClick = 0;
                    }
                }
            }
        }

        private bool checkIfAbsence(DateTime from, DateTime to)
        {
            DateTime st = from.Date;

            while (st <= to.Date)
            {
                if (absentes.Contains(st.Date))
                    return true;

                st = st.Date.AddDays(1);
            }

            return false;
        }

        private bool workTimesContains(DateTime date)
        {
            foreach (var v in workTimes)
            {
                if (v.StartDate.Date <= date.Date && v.EndDate.Date >= date.Date)
                {
                    if (workTimesCon(date))
                        return true;

                }
            }
            return false;
        }
        private bool workTimesCon(DateTime date)
        {
            foreach (var v in workingDays)
            {
                if (date.Date == v.DateFrom.Date)
                    return true;
            }
            return false;
        }

        private EmployeeWorkDay workTimesCons(DateTime date)
        {
            EmployeeWorkDay retVal = new EmployeeWorkDay();
            foreach (var v in workingDays)
            {
                if (date.Date == v.DateFrom.Date)
                    return v;
            }
            return retVal;
        }

        private void preklapanjeDatuma()
        {
            resetViewIfNoErrorOnSecondClick("Datum pocetka radnog vremena: ");
            pocetakRadnogVremena.Text = "";
            krajRadnogVremena.Text = "";
            krajRadnogVremena.Visibility = Visibility.Collapsed;
            TextBlockObjasnjenje1.Visibility = Visibility.Collapsed;
            TextBlockObjasnjenje.Text = "";
        }

        private bool checkIfDatesAreInRange(DateTime dateFrom, DateTime dateTo)
        {
            WorkTime retVal = new WorkTime();
            foreach (var r in workTimes)
            {
                if (dateFrom == dateTo)
                {
                    if ((dateFrom.Date >= r.StartDate.Date && dateFrom.Date <= r.EndDate.Date) || (dateTo.Date >= r.StartDate.Date && dateTo.Date <= r.EndDate.Date) || (dateFrom.Date <= r.StartDate.Date && dateTo.Date >= r.EndDate.Date))
                    {
                        if (r.workDay.Where(x => (int)x.Day == (int)dateFrom.DayOfWeek).Count() > 0)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if ((dateFrom.Date >= r.StartDate.Date && dateFrom.Date <= r.EndDate.Date) || (dateTo.Date >= r.StartDate.Date && dateTo.Date <= r.EndDate.Date) || (dateFrom.Date <= r.StartDate.Date && dateTo.Date >= r.EndDate.Date))
                    {
                        return true;

                    }
                }
            }
            return false;
        }

        private bool checkIfDatesAreFullfiled(DateTime dateFrom, DateTime dateTo)
        {
            bool retVal = false;
            foreach (var r in workTimes)
            {
                if ((dateFrom.Date >= r.StartDate.Date && dateFrom.Date <= r.EndDate.Date) || (dateTo.Date >= r.StartDate.Date && dateTo.Date <= r.EndDate.Date) || (dateFrom.Date <= r.StartDate.Date && dateTo.Date >= r.EndDate.Date))
                {
                    int a = 0;
                    foreach (var v in r.workDay)
                        a++;
                    if (a == 7)
                    {
                        retVal = true;
                        break;
                    }
                }
            }
            return retVal;
        }

        private void selectDatesInCalendar(DateTime dateFrom, DateTime dateTo)
        {
            Kalendar.SelectedDates.Clear();
            Kalendar.SelectedDates.AddRange(dateFrom, dateTo);
        }

        private void resetViewIfNoErrorOnFirstClick(string message)
        {
            pocetakRadnogVremena.Visibility = Visibility.Visible;
            TextBlockObjasnjenje.Text = message;
            TextBlockObjasnjenje.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7e8187"));
            editWorkTime.IsEnabled = true;
            TextBlockObjasnjenje.FontSize = 18;
            TextBlockObjasnjenje1.Visibility = Visibility.Collapsed;
            krajRadnogVremena.Visibility = Visibility.Collapsed;

        }

        private void resetViewIfNoErrorOnSecondClick(string message)
        {
            pocetakRadnogVremena.Visibility = Visibility.Visible;
            TextBlockObjasnjenje.Text = message;
            TextBlockObjasnjenje.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7e8187"));
            editWorkTime.IsEnabled = true;
            TextBlockObjasnjenje.FontSize = 18;
            TextBlockObjasnjenje1.Visibility = Visibility.Visible;
            krajRadnogVremena.Visibility = Visibility.Visible;

        }

        private void showErrorMessage(string message)
        {
            pocetakRadnogVremena.Visibility = Visibility.Collapsed;
            TextBlockObjasnjenje.Text = message;
            TextBlockObjasnjenje.Foreground = Brushes.Red;
            editWorkTime.IsEnabled = false;
            TextBlockObjasnjenje.FontSize = 13;
            TextBlockObjasnjenje1.Visibility = Visibility.Collapsed;
            krajRadnogVremena.Visibility = Visibility.Collapsed;

        }

        private void showSelectedDateDetails(DateTime value)
        {
            var culture = new System.Globalization.CultureInfo("sr-SP-Latin");
            String day = culture.DateTimeFormat.GetDayName(value.DayOfWeek);
            String month = culture.DateTimeFormat.GetMonthName(value.Month);
            Datum.Text = char.ToUpper(day[0]) + day.Substring(1) + "," + " " + char.ToUpper(month[0]) + month.Substring(1) + " " +
                        value.Day;

            Godina.Text = value.Year.ToString();
        }


        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;


            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);



        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            if (workTimesCon(date))
            {
                button.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#15cc08"));
                button.Foreground = Brushes.White;
                //button.Opacity = 0.5;
            }
            else if (absenceContains(date))
            {
                button.Background = Brushes.Red;
                button.Foreground = Brushes.White;
            }
            else
            {
                button.Background = Brushes.Transparent;
                button.Foreground = Brushes.Black;
            }
        }

        private bool absenceContains(DateTime date)
        {
            foreach (var v in absentes)
            {
                if (v.Date == date.Date)
                    return true;
            }

            return false;
        }


        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }

        private void addWorkTime_Click(object sender, RoutedEventArgs e)
        {
            if (datumOd.Date >= DateTime.Now.Date)
                MainWindow.AppWindow.navigateWithTitleChange(new DodavanjeRadnogVremena(datumOd.Date, datumDo.Date, (EmployeeItem)this.Employee), false);
        }

        private void addExistingWorkTime_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new AddExistingWorkTime((EmployeeItem)this.Employee), false);
        }

        private void CalendarDayButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }
        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        private void confirmaDeletingWorkTimeEvent(object sender, RoutedEventArgs e)
        {
            if (_workTimeController.Delete(selected) == null)
            {
                ShowError("Izabrano radno vreme nije moguce izbrisati jer lekar ima zakazane termine");
            }
            else
            {
                MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
                MainWindow.AppWindow.navigateWithTitleChange(new RadnoVremeIzmena(Employee), false);
            }
        }
        private void deleteWorkTime_Click(object sender, RoutedEventArgs e)
        {

            dialog.Children.Add(new ConfirmDialog("Brisanje radnog vremena", "Da li ste sigurni da zelite da izbrisete radno vreme?",
                                                        new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(confirmaDeletingWorkTimeEvent)));
            dialog.Visibility = Visibility.Visible;

        }

        private void editWorkTime_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new MenjanjeRadnogVremena(datumOd, datumDo, Employee), false);
        }
    }
}
