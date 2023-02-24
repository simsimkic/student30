using Controller.UserControllers;
using HealthCare;
using Model.Users;
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

namespace Sekretar.MojProfil
{
    /// <summary>
    /// Interaction logic for ZahteviZaOdsustvo.xaml
    /// </summary>
    public partial class ZahteviZaOdsustvo : Window
    {
        PosaljiZahtev posaljiZahtev = null;
        private IAbsenceController absenceController;

        public ObservableCollection<Absence> Zahtevi
        {
            get;
            set;
        }

        private ZahteviZaOdsustvo()
        {
            InitializeComponent();
            this.DataContext = this;
            Zahtevi = new ObservableCollection<Absence>();
            var app = Application.Current as App;
            absenceController = app.absenceController;

            Zahtevi = new ObservableCollection<Absence>();
            List<Absence> abscences = absenceController.GetAll().ToList();
            foreach (Absence a in abscences)
                if(a.staff.Id.Equals(app._currentUser.Id))
                Zahtevi.Add(a);

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        public void ResetVisibility()
        {
            var app = Application.Current as App;
            absenceController = app.absenceController;

            Zahtevi.Clear();
            List<Absence> abscences = absenceController.GetAll().ToList();
            foreach (Absence a in abscences)
                if (a.staff.Id.Equals(app._currentUser.Id))
                    Zahtevi.Add(a);

            this.DataContext = this;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private static ZahteviZaOdsustvo instance = null;
        public static ZahteviZaOdsustvo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZahteviZaOdsustvo();
                }
                return instance;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void DataGridStudenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonClickPosaljiZahtev(object sender, RoutedEventArgs e)
        {
            PosaljiZahtev posaljiZahtev = Sekretar.MojProfil.PosaljiZahtev.Instance;
            posaljiZahtev.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            posaljiZahtev.ShowDialog();
        }

    }
}
