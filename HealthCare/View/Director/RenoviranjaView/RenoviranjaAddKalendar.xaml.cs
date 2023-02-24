using Controller.AppointmenController;
using Controller.RoomControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HCIProjekat.View.HelpWizardView;
using HCIProjekat.View.ProstorijeView.DataView;
using HCIProjekat.View.RenoviranjaView.DataView;
using HealthCare;
using HealthCare.Util;
using Model.Appointment;
using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HCIProjekat.RenoviranjaView
{
    /// <summary>
    /// Interaction logic for RenoviranjaAddKalendar.xaml
    /// </summary>
    public partial class RenoviranjaAddKalendar : Page
    {
        public int countClick = 0;
        public DateTime datumOd;
        public DateTime datumDo;
        public UserControl RoomItem { get; set; }

        public ObservableCollection<Appointment> appointments;
        public ObservableCollection<Renovate> renovates;
        public ObservableCollection<DateTime> appointmentDates;

        private IAppointmentController appointmentController;
        private IRoomController _roomController;
        private IRenovateController _renovateController;

        public RenoviranjaAddKalendar(UserControl roomItem)
        {

            var app = Application.Current as App;
            appointmentController = app.appointmentController;
            _roomController = app.roomController;
            _renovateController = app.renovateController;

            renovates = new ObservableCollection<Renovate>(_renovateController.GetFutureRenovationsForRoom(_roomController.GetFromID(((RoomItem)roomItem).RoomNumber)));
            appointments = new ObservableCollection<Appointment>(appointmentController.GetAppointmentForRoom(_roomController.GetFromID(((RoomItem)roomItem).RoomNumber)));
            appointmentDates = new ObservableCollection<DateTime>(fulfillAppointments());

            this.RoomItem = roomItem;
            InitializeComponent();

            Kalendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue.Date, DateTime.Now.AddDays(App.MinDaysForRenovationStart).Date));

            DataContext = this;
        }

        private List<DateTime> fulfillAppointments()
        {
            List<DateTime> retVal = new List<DateTime>();
            foreach(Appointment a in appointments)
                retVal.Add(a.StartDateTime.Date);

            foreach (Renovate r in renovates)
                foreach (DateTime time in RenovationConverter.ConvertRenovateToDateTimeList(r))
                    retVal.Add(time.Date);

            return retVal;
        }

        private void Kalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kalendar.SelectedDate != null && countClick < 2)
            {
                DateTime selektovano = Kalendar.SelectedDate.Value;
                showSelectedDateDetails(selektovano);
                if (appointmentDates.Contains(selektovano) && countClick == 0)
                {
                    showErrorMessage("Dan koji ste izabrali nije moguce uzeti za pocetak renoviranja");
                }
                else
                {


                    if (Kalendar.SelectedDates.Count == 1)
                    {
                        countClick++;
                        if (countClick == 1)
                        {
                            resetViewIfNoErrorOnFirstClick("Datum renoviranja: ");
                            PocetakRenoviranja.Text = selektovano.ToShortDateString();
                            ZavrsetakRenoviranja.Text = "";
                        }
                        else
                        {
                            resetViewIfNoErrorOnSecondClick("Datum pocetka renoviranja: ");
                            ZavrsetakRenoviranja.Text = selektovano.ToShortDateString();
                        }

                    }
                    else
                    {
                        if (!checkIfDatesAreValid(Kalendar.SelectedDates[0], Kalendar.SelectedDates[Kalendar.SelectedDates.Count - 1]))
                            showErrorMessage("Renoviranje se moze obaviti samo kada je prostorija slobodna");
                        else
                        {
                            resetViewIfNoErrorOnSecondClick("Datum pocetka renoviranja: ");
                            datumOd = Kalendar.SelectedDates[0];
                            datumDo = Kalendar.SelectedDates[Kalendar.SelectedDates.Count - 1];
                            PocetakRenoviranja.Text = datumOd.ToShortDateString();
                            ZavrsetakRenoviranja.Text = datumDo.ToShortDateString();
                        }
                        countClick = 0;
                    }

                    if (countClick == 1)
                    {
                        if (MainWindow.APP_HELP)
                        {
                            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                            MainWindow.AppWindow.HelpFrame.Navigate(new CalendarHelpSecondRenoviranje());
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
                            selectDatesInCalendar(datumOd, datumDo);
                            if (!checkIfDatesAreValid(datumOd, datumDo))
                                showErrorMessage("Renoviranje se moze obaviti samo kada je prostorija slobodna");
                            else
                                resetViewIfNoErrorOnSecondClick("Datum pocetka renoviranja: ");
                        }
                        else
                        {
                            showErrorMessage("Datum pocetka renoviranja mora biti manji od datuma zavrsetka");
                            Kalendar.SelectedDates.Clear();
                        }

                        countClick = 0;
                    }
                }
            }
        }

        private void resetViewIfNoErrorOnFirstClick(string message)
        {
            PocetakRenoviranja.Visibility = Visibility.Visible;
            TextBlockObjasnjenje.Text = message;
            TextBlockObjasnjenje.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7e8187"));
            DodajRenoviranje.IsEnabled = true;
            TextBlockObjasnjenje.FontSize = 18;
            TextBlockObjasnjenje1.Visibility = Visibility.Collapsed;
            ZavrsetakRenoviranja.Visibility = Visibility.Collapsed;
        }

        private void resetViewIfNoErrorOnSecondClick(string message)
        {
            PocetakRenoviranja.Visibility = Visibility.Visible;
            TextBlockObjasnjenje.Text = message;
            TextBlockObjasnjenje.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7e8187"));
            DodajRenoviranje.IsEnabled = true;
            TextBlockObjasnjenje.FontSize = 18;
            TextBlockObjasnjenje1.Visibility = Visibility.Visible;
            ZavrsetakRenoviranja.Visibility = Visibility.Visible;
        }

        private void showErrorMessage(string message)
        {
            PocetakRenoviranja.Visibility = Visibility.Collapsed;
            DodajRenoviranje.IsEnabled = false;
            TextBlockObjasnjenje.Text = message;
            TextBlockObjasnjenje.Foreground = Brushes.Red;
            TextBlockObjasnjenje.FontSize = 14;
            TextBlockObjasnjenje1.Visibility = Visibility.Collapsed;
            ZavrsetakRenoviranja.Visibility = Visibility.Collapsed;
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

        private bool checkIfDatesAreValid(DateTime dateFrom, DateTime dateTo)
        {
            DateTime start = dateFrom.Date;
            while(start <= dateTo.Date)
            {
                if (appointmentDates.Contains(start))
                    return false;
                start = start.AddDays(1);
            }
            return true;
        }

        private void selectDatesInCalendar(DateTime dateFrom, DateTime dateTo)
        {
            Kalendar.SelectedDates.Clear();
            Kalendar.SelectedDates.AddRange(dateFrom, dateTo);
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
            if (appointmentDates.Contains(date))
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

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }

        private void DodajRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new RenoviranjaAddFinal(datumOd, datumDo, RoomItem), false);
        }

        private void withdrawAdd_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od zakazivanja", "Da li ste sigurni da zelite da odustanete od zakazivanja renoviranja?",
                                                      new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawAddingEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }

        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawAddingEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
        }
    }
}
