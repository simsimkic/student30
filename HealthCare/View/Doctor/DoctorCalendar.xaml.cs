using Controller.UserControllers;
using HCIProjekat.View.ZaposleniView;
using HealthCare.Util;
using HealthCare.View.Doctor;
using HealthCareClinic.View;
using Model.Users;
using Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthCare.HCI
{
    /// <summary>
    /// Interaction logic for DoctorCalendar.xaml
    /// </summary>
    public partial class DoctorCalendar : UserControl
    {
        public static int previousMonth; // prethodni mesec
        public static int currentMonth = DateTime.Today.Month; // trenutni mesec
        public static int currentYear = DateTime.Today.Year; // trenutna godina
        public static DateTime selectedDay = DateTime.Today;
        private Doctor _doctor;
        private static List<CalendarData> calendarDataList = new List<CalendarData>();

        private readonly IWorkTimeController _workTimeController;
        private readonly IAbsenceController _absenceController;

        public class Days
        {
            public Visibility visible { get; set; }  // vidljivost
            public DateTime date { get; set; } // dan na koji se odnosi
            public string name { get; set; } // broj
            public string text { get; set; } // tekst koji ce stojati
            public FontWeight weight { get; set; } // velicina slova
            public SolidColorBrush dayColor { get; set; } // posebne boje za odmore, radno vreme i slicno 
        }

        private IList<EmployeeWorkDay> addWorkTime(List<WorkTime> wtl)
        {
            return WorkTimeConverter.ConvertWorkTimeListToEmployeeWorkDayList(wtl);
        }

        private IList<EmployeeAbsence> addAbsence(List<Absence> absence)
        {
            return AbsenceConverter.ConvertAbsenceListToEmployeeAbsenceList(absence);
        }

        public DoctorCalendar()
        {
            InitializeComponent();
            btnLeftCalendar.Focus();

            this.PreviewKeyDown += new KeyEventHandler(HandleKeyboard);
            if (currentMonth == 1)
            {
                previousMonth = 12;
            }
            else
            {
                previousMonth = currentMonth - 1;
            }

            calendarDataList = new List<CalendarData>();


            _doctor = (Doctor)(Application.Current as App)._currentUser;

            _workTimeController = (Application.Current as App).workTimeController;
            _absenceController = (Application.Current as App).absenceController;
            IList<EmployeeWorkDay> workTimes = new List<EmployeeWorkDay>();


            List<Absence> absences = _absenceController.GetEmployeesAbsenceHistory(_doctor).ToList();
            absences.AddRange(_absenceController.GetEmployeesFutureAbsence(_doctor).ToList());

            IList<EmployeeAbsence> absencesList = new List<EmployeeAbsence>();

            absencesList = addAbsence(absences);

            workTimes = addWorkTime(_workTimeController.GetWorkTime(_doctor).ToList());

            foreach (EmployeeWorkDay item in workTimes)
            {
                DateTime dt1 = DateTime.Parse(item.DateFrom.ToString());
                DateTime dt2 = DateTime.Parse(item.DateTo.ToString());

                string dataMessage = dt1.ToString("HH:mm") + " - " + dt2.ToString("HH:mm");

                calendarDataList.Add(new CalendarData { Date = new DateTime(item.DateFrom.Year, item.DateFrom.Month, item.DateFrom.Day), DataMessage = dataMessage });
            }

            foreach (EmployeeAbsence item in absencesList)
            {
                string dataMessage = item.Type.ToString();

                if(item.Type== AbsenceType.Conference)
                {
                    calendarDataList.Add(new CalendarData { Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day), DataMessage = "Konferencija", color = new SolidColorBrush(Colors.YellowGreen) });
                }else if(item.Type == AbsenceType.Vacation)
                {
                    calendarDataList.Add(new CalendarData { Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day), DataMessage = "Odmor", color = new SolidColorBrush(Colors.LightBlue) });
                }
                else if (item.Type == AbsenceType.WorkObligations)
                {
                    calendarDataList.Add(new CalendarData { Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day), DataMessage = "Poslovne obaveze", color = new SolidColorBrush(Colors.LightPink) });
                }
                else if (item.Type == AbsenceType.PrivateObligations)
                {
                    calendarDataList.Add(new CalendarData { Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day), DataMessage = "Privatne obaveze", color = new SolidColorBrush(Colors.LightPink) });
                }
                else if (item.Type == AbsenceType.Other)
                {
                    calendarDataList.Add(new CalendarData { Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day), DataMessage = "Ostalo", color = new SolidColorBrush(Colors.LightCyan) });
                }
            }

            SetDays();

        }

        public void SetDays()
        {
            DateTime firstDate = new DateTime(currentYear, currentMonth, 1);
            SetText();

            if ((int)firstDate.DayOfWeek == 1)
            {
                SetDaysFromMonday();
            }
            else if ((int)firstDate.DayOfWeek == 2)
            {
                SetDaysFromTuesday();
            }

            else if ((int)firstDate.DayOfWeek == 3)
            {
                SetDaysFromWednesday();
            }
            else if ((int)firstDate.DayOfWeek == 4)
            {
                SetDaysFromThrusday();
            }
            else if ((int)firstDate.DayOfWeek == 5)
            {
                SetDaysFromFriday();
            }
            else if ((int)firstDate.DayOfWeek == 6)
            {
                SetDaysFromSaturday();
            }
            else if ((int)firstDate.DayOfWeek == 0)
            {
                SetDaysFromSunday();
            }
        }
        private void SetDaysFromMonday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);

            int i = 0;
            dayList.Add(new Days() { name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, date = firstDate.AddDays(-1), weight = FontWeights.Light });
            while (i < daysInMonth)
            {
                Days day = null;
                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }



                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (41 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }



            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromTuesday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                Days day = null;

                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }



                dayList.Add(day);


            }
            int j = 0;
            for (i = 0; i < (40 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }


            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromWednesday()
        {

            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                Days day = null;

                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }



                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (39 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromThrusday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 3), name = (daysInPreviousMonth - 3).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                Days day = null;

                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }



                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (38 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }

        private void SetDaysFromFriday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 4), name = (daysInPreviousMonth - 4).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 3), name = (daysInPreviousMonth - 3).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            int i = 0;
            while (i < daysInMonth)
            {
                Days day = null;

                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }



                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (37 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }
        private void SetDaysFromSaturday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 5), name = (daysInPreviousMonth - 5).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 4), name = (daysInPreviousMonth - 4).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 3), name = (daysInPreviousMonth - 3).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 2), name = (daysInPreviousMonth - 2).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth - 1), name = (daysInPreviousMonth - 1).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            dayList.Add(new Days() { date = new DateTime(currentYear, previousMonth, daysInPreviousMonth), name = daysInPreviousMonth.ToString(), visible = Visibility.Visible, weight = FontWeights.Light });

            while (i < daysInMonth)
            {
                Days day = null;

                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }


                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (36 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);

        }
        private void SetDaysFromSunday()
        {
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int daysInPreviousMonth = DateTime.DaysInMonth(currentYear, previousMonth);
            List<Days> dayList = new List<Days>();
            DateTime firstDate = new DateTime(currentYear, previousMonth, daysInPreviousMonth);
            int i = 0;

            while (i < daysInMonth)
            {
                Days day = null;

                firstDate = firstDate.AddDays(1);

                foreach (CalendarData data in calendarDataList)
                {
                    if (DateTime.Compare(data.Date, firstDate) == 0)
                    {
                        day = new Days() { dayColor = data.color, text = data.DataMessage, date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };
                    }
                }

                if (day == null)
                {
                    day = new Days() { date = firstDate, name = (++i).ToString(), visible = Visibility.Visible, weight = FontWeights.ExtraBold };

                }



                dayList.Add(day);
            }
            int j = 0;
            for (i = 0; i < (42 - daysInMonth); i++)
            {
                firstDate = firstDate.AddDays(1);
                dayList.Add(new Days() { date = firstDate.AddDays(1), name = (++j).ToString(), visible = Visibility.Visible, weight = FontWeights.Light });
            }
            Week1.ItemsSource = new List<Days>();
            Week1.ItemsSource = dayList.GetRange(0, 7);
            Week2.ItemsSource = new List<Days>();
            Week2.ItemsSource = dayList.GetRange(7, 7);
            Week3.ItemsSource = new List<Days>();
            Week3.ItemsSource = dayList.GetRange(14, 7);
            Week4.ItemsSource = new List<Days>();
            Week4.ItemsSource = dayList.GetRange(21, 7);
            Week5.ItemsSource = new List<Days>();
            Week5.ItemsSource = dayList.GetRange(28, 7);
            Week6.ItemsSource = new List<Days>();
            Week6.ItemsSource = dayList.GetRange(35, 7);
        }



        private void SetText()
        {
            string text = "";
            if (currentMonth == 1)
            {
                text = "Januar " + currentYear.ToString();
            }
            if (currentMonth == 2)
            {
                text = "Februar " + currentYear.ToString();
            }
            if (currentMonth == 3)
            {
                text = "Mart " + currentYear.ToString();
            }
            if (currentMonth == 4)
            {
                text = "April " + currentYear.ToString();
            }
            if (currentMonth == 5)
            {
                text = "Maj " + currentYear.ToString();
            }
            if (currentMonth == 6)
            {
                text = "Jun " + currentYear.ToString();
            }
            if (currentMonth == 7)
            {
                text = "Jul " + currentYear.ToString();
            }
            if (currentMonth == 8)
            {
                text = "Avgust " + currentYear.ToString();
            }
            if (currentMonth == 9)
            {
                text = "Septembar " + currentYear.ToString();
            }
            if (currentMonth == 10)
            {
                text = "Oktobar " + currentYear.ToString();
            }
            if (currentMonth == 11)
            {
                text = "Novembar " + currentYear.ToString();
            }
            if (currentMonth == 12)
            {
                text = "Decembar " + currentYear.ToString();
            }
            CurrentMonth.Text = text;
        }


        private void btnLeftCalendar_Click(object sender, RoutedEventArgs e)
        {
            currentMonth--;
            previousMonth--;
            if (currentMonth == 13)
            {
                currentMonth = 1;
                previousMonth = 12;
                currentYear++;
            }
            else if (currentMonth == 0)
            {
                currentMonth = 12;
                previousMonth = 11;
                currentYear--;
            }
            else if (previousMonth == 0)
            {
                currentMonth = 1;
                previousMonth = 12;
            }

            Console.WriteLine(currentMonth);
            Console.WriteLine(previousMonth);


            Week1.ItemsSource = new List<Days>();
            Week2.ItemsSource = new List<Days>();
            Week3.ItemsSource = new List<Days>();
            Week4.ItemsSource = new List<Days>();
            Week5.ItemsSource = new List<Days>();
            Week6.ItemsSource = new List<Days>();
            SetDays();
        }

        private void btnRightCalendar_Click(object sender, RoutedEventArgs e)
        {
            currentMonth++;
            previousMonth++;
            if (currentMonth == 13)
            {
                currentMonth = 1;
                previousMonth = 12;
                currentYear++;
            }
            else if (currentMonth == 0)
            {
                currentMonth = 12;
                currentYear--;
            }
            else if (previousMonth == 13)
            {
                previousMonth = 1;
                currentMonth = 2;
            }



            Week1.ItemsSource = new List<Days>();
            Week2.ItemsSource = new List<Days>();
            Week3.ItemsSource = new List<Days>();
            Week4.ItemsSource = new List<Days>();
            Week5.ItemsSource = new List<Days>();
            Week6.ItemsSource = new List<Days>();
            SetDays();
        }
        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
                btnLeftCalendar_Click(sender, e);
            if (e.Key == Key.E)
                btnRightCalendar_Click(sender, e);
        }
    }
}
