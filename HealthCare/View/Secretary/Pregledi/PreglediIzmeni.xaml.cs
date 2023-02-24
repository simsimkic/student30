using Controller.AppointmenController;
using HealthCare;
using Model.Appointment;
using Model.Users;
using Sekretar.Pregledi;
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

namespace Sekretar.Pregledi
{
    /// <summary>
    /// Interaction logic for PreglediIzmeni.xaml
    /// </summary>
    public partial class PreglediIzmeni : Window
    {
        private IAppointmentController iAppointnmentController;

        private PreglediIzmeni()
        {
            InitializeComponent();
            var app = Application.Current as App;
            iAppointnmentController = app.appointmentController;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static PreglediIzmeni instance = null;
        public static PreglediIzmeni Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PreglediIzmeni();
                }
                return instance;
            }
        }

        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private Appointment updateAppointment()
        {
            Appointment app = iAppointnmentController.GetAll().Single(i => i.Patient.JMBG.Equals(JMBG.Text));
            iAppointnmentController.Delete(app);
            app.Patient.Name = Ime.Text;
            app.Patient.Surname = Prezime.Text;
            return app;
        }
        
        private void ButtonClickZakazi(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(JMBG.Text, out _) && (JMBG.Text.Length != 13) && (JMBG.Text.Length != 0))
            {
                MessageBox.Show(Secretary.Properties.Langs.Lang.jmbgError, Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Pregledi pregledi = Pregledi.Instance;
            PreglediUvid preglediUvid = PreglediUvid.Instance;
            String datum = Datum.Content.ToString();
            String smena = Smena.Content.ToString();
            String doktor = Doktor.Content.ToString();

            preglediUvid.Datum.Content = datum;
            preglediUvid.Smena.Content = smena;
            preglediUvid.Doktor.Content = doktor;

            if (Ime.Text.Length == 0 || JMBG.Text.Length == 0)
            {
                MessageBox.Show("Morate popuniti sva polja", Secretary.Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Appointment p = PreglediUvid.Instance.appZaIzmeni;
            
            Console.WriteLine("ID appzaizmeni id " + p.Id + " ime " + p.Patient.Name + p.Patient.Surname);
            iAppointnmentController.Delete(p);
            p.Patient.Name = Ime.Text;
            p.Patient.Surname = Prezime.Text;
            iAppointnmentController.Create(p);

            preglediUvid.ResetComponents();

            this.Visibility = Visibility.Collapsed;
            
        }
    }
}
