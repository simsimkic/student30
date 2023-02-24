using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Controller.AppointmenController;
using Controller.UserControllers;
using HealthCare.View.Secretary.Izvestaj;
using Model.Appointment;
using Model.MedicalRecords;
using Model.Users;
using Sekretar.Model;
using Sekretar.Pregledi;
using Sekretar.Sale;

namespace HealthCare.View.Secretary.Izvestaj
{
    /// <summary>
    /// Interaction logic for SelektujDan.xaml
    /// </summary>
    /// 

    public static class DateTimeExtensions
    {

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

    public partial class SelektujDan : Window
    {
        private IUserController userController;

        private IAppointmentController iAppointnmentController;
        public SelektujDan()
        {
            InitializeComponent();
            var app = Application.Current as App;
            userController = app.userController;
            iAppointnmentController = app.appointmentController;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonPotvrdi(object sender, RoutedEventArgs e)
        {
            if (!Kalendar.SelectedDate.HasValue)
            {
                MessageBox.Show(Secretary.Properties2.Langs.Lang.dateError, Secretary.Properties2.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if ((bool)!AppointmentCheck.IsChecked &&
                (bool)!SurgeryCheck.IsChecked)
            {
                MessageBox.Show(Secretary.Properties2.Langs.Lang.reportError, Secretary.Properties2.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DateTime selected = Kalendar.SelectedDate.Value;
            DateTime dt = selected.StartOfWeek(DayOfWeek.Monday);
            List<DateTime> dates = new List<DateTime>();
            dates.Add(dt);
            DateTime dt2 = dt.AddDays(1);
            dates.Add(dt2);
            DateTime dt3 = dt2.AddDays(1);
            dates.Add(dt3);
            DateTime dt4 = dt3.AddDays(1);
            dates.Add(dt4);
            DateTime dt5 = dt4.AddDays(1);
            dates.Add(dt5);
            DateTime dt6 = dt5.AddDays(1);
            dates.Add(dt6);
            DateTime dt7 = dt6.AddDays(1);
            dates.Add(dt7);

            Sale sale = Sale.Instance;
            Sekretar.Pregledi.Pregledi pregledi = Sekretar.Pregledi.Pregledi.Instance;
            User user;
            User lekar;
            if ((bool)AppointmentCheck.IsChecked && !(bool)SurgeryCheck.IsChecked)
            {
                ObservableCollection<Appointment> preglediModelO = new ObservableCollection<Appointment>();
                foreach (DateTime date in dates)
                {
                    List<Appointment> appointments = iAppointnmentController.GetAppointmentForDate(date).ToList();

                    foreach (Appointment sob in appointments)
                    {
                        user = (User)userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(sob.Patient.Id));
                        lekar = (User)userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(sob.Doctor.Id));
                        if (user != null && sob.Type == TreatmentType.Examination)
                        {
                            if (sob.Patient.Name == null )
                                sob.Patient.Name = user.Name;
                            if (sob.Patient.Surname == null)
                                sob.Patient.Surname = user.Surname;
                            sob.Doctor.Name = lekar.Name;
                            preglediModelO.Add(sob);
                        }
                    }
                }
                Izvestaj.IzvestajApp izvestaj = new Izvestaj.IzvestajApp();
                izvestaj.izvestaj = preglediModelO;
                izvestaj.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Application.Current.MainWindow.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
                izvestaj.ShowDialog();
            }
            if ((bool)SurgeryCheck.IsChecked && !(bool)AppointmentCheck.IsChecked)
            {
                ObservableCollection<Appointment> sobeModelO = new ObservableCollection<Appointment>();

                foreach (DateTime date in dates)
                {
                    List<Appointment> appointments = iAppointnmentController.GetAppointmentForDate(date).ToList();

                    foreach (Appointment sob in appointments)
                    {
                        user = (User)userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(sob.Patient.Id));
                        lekar = (User)userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(sob.Doctor.Id));
                        if (user != null && sob.Type == TreatmentType.Surgery)
                        {
                            if (sob.Patient.Name == null)
                                sob.Patient.Name = user.Name;
                            if (sob.Patient.Surname == null)
                                sob.Patient.Surname = user.Surname;
                            sob.Doctor.Name = lekar.Name;
                            sobeModelO.Add(sob);
                        }
                    }
                }
                Izvestaj.IzvestajSur izvestaj = new Izvestaj.IzvestajSur();
                izvestaj.izvestaj = sobeModelO;
                izvestaj.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Application.Current.MainWindow.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
                izvestaj.ShowDialog();
            }
            if ((bool)AppointmentCheck.IsChecked && (bool)SurgeryCheck.IsChecked)
            {
               ObservableCollection<Appointment> sobeModelO = new ObservableCollection<Appointment>();

                ObservableCollection<Appointment> preglediModelO = new ObservableCollection<Appointment>();
                foreach (DateTime date in dates)
                {
                    List<Appointment> appointments = iAppointnmentController.GetAppointmentForDate(date).ToList();

                    foreach (Appointment sob in appointments)
                    {
                        user = (User)userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(sob.Patient.Id));
                        lekar = (User)userController.GetAll().ToList().SingleOrDefault(i => i.Id.Equals(sob.Doctor.Id));
                        if (user != null && sob.Type == TreatmentType.Examination)
                        {
                            if (sob.Patient.Name == null)
                                sob.Patient.Name = user.Name;
                            if (sob.Patient.Surname == null)
                                sob.Patient.Surname = user.Surname;
                            sob.Doctor.Name = lekar.Name;
                            preglediModelO.Add(sob);
                        }
                        if (user != null && sob.Type == TreatmentType.Surgery)
                        {
                            if (sob.Patient.Name == null)
                                sob.Patient.Name = user.Name;
                            if (sob.Patient.Surname == null)
                                sob.Patient.Surname = user.Surname;
                            sob.Doctor.Name = lekar.Name;
                            sobeModelO.Add(sob);
                        }
                    }
                }
                Sekretar.Izvestaj.Izvestaj izvestaj = new Sekretar.Izvestaj.Izvestaj();
                izvestaj.izvestaj = sobeModelO;
                izvestaj.preglediDan = preglediModelO;
                izvestaj.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Application.Current.MainWindow.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
                izvestaj.ShowDialog();
            }

        }

    }
}
