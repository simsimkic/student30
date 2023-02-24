using HealthCare;
using Model.Users;
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
using System.Windows.Shapes;

namespace Sekretar.MojProfil
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MojProfil : Window
    {
        private MojProfil()
        {
            InitializeComponent();
            initializeProfile();
        }
        private static MojProfil instance = null;
        public static MojProfil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MojProfil();
                }
                return instance;
            }
        }

        public void initializeProfile()
        {
            var app = Application.Current as App;
            Ime.Content = app._currentUser.Name;
            Prezime.Content = app._currentUser.Surname;
            Adresa.Content = app._currentUser.Residence.City.PostNumber + ", " + app._currentUser.Residence.Street + 
                "," + app._currentUser.Residence.Number + ", " + app._currentUser.Residence.City.Name
                + ", " + app._currentUser.Residence.City.Country.Name;
            DatumRodjenja.Content = app._currentUser.BirthDate.ToShortDateString();
            Telefon.Content = app._currentUser.Contact.Number;
            Staff s = (Staff)app._currentUser;
            String Picture = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s.Picture);

            Uri fileUri = new Uri(Picture);
            Slika.Source = new BitmapImage(fileUri);

        }

        private void ButtonClickPostaviSliku(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonClickIzmeniProfil(object sender, RoutedEventArgs e)
        {
            IzmeniProfil izmeniProfil = IzmeniProfil.Instance;
            izmeniProfil.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            izmeniProfil.postaviVrednosti();
            izmeniProfil.ShowDialog();
        }
        private void ButtonClickZahtevi(object sender, RoutedEventArgs e)
        {
            ZahteviZaOdsustvo zahteviOdsustvo = ZahteviZaOdsustvo.Instance;
            zahteviOdsustvo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            zahteviOdsustvo.ResetVisibility();
            zahteviOdsustvo.Show();
        }
        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Application.Current.MainWindow.ShowDialog();
 
        }
    }
}
