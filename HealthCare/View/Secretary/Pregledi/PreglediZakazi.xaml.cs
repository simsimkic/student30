using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Controller.AppointmenController;
using HealthCare;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model.Appointment;
using Model.Users;
using Model.Rooms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HealthCare.View.Secretary.Pregledi;
using Controller.UserControllers;
using Model.Users.UserBuilder;

namespace Sekretar.Pregledi
{
    /// <summary>
    /// Interaction logic for PreglediZakazi.xaml
    /// </summary>
    public partial class PreglediZakazi : Window
    {
        public Patient p;
        private IAppointmentController iAppointnmentController;
        private IUserController iUserController;
        private PreglediZakazi()
        {
            InitializeComponent();
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;
            iUserController = app.userController;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static PreglediZakazi instance = null;
        public static PreglediZakazi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PreglediZakazi();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private void ButtonClickZakazi(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(Jmbg.Text, out _) && (Jmbg.Text.Length != 13))
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.jmbgError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Pregledi pregledi = Pregledi.Instance;
            PreglediZakaziFree zakazi = PreglediZakaziFree.Instance;
            String datum = Datum.Content.ToString();
            String smena = Smena.Content.ToString();
            String doktor = Doktor.Content.ToString();
            String[] doktorIme = doktor.Split(' ');

            PatientBuilder patientBuilder = new PatientBuilder();
            patientBuilder.SetName(Ime.Text);
            patientBuilder.SetSurname(Prezime.Text);
            patientBuilder.SetJMBG(Jmbg.Text);
            patientBuilder.SetGuest(true);
            p = patientBuilder.getResult();

            p = (Patient)iUserController.Create(p);


            if (p == null)
                return;
            Console.WriteLine("Kreirani pacijent id " + p.Id + p.Name + p.Surname );
            zakazi.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            PreglediUvid.Instance.Visibility = Visibility.Collapsed;
            zakazi.Doktor.Content = Pregledi.Instance.Doktor.Text;
            zakazi.ResetComponentsForFreeApp();
            zakazi.ShowDialog();

        }

        private void Ime_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
