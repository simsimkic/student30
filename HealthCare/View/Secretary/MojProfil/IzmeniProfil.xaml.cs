using Controller.UserControllers;
using HealthCare;
using HealthCare.View.Secretary.MojProfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using MessageBox = System.Windows.MessageBox;

namespace Sekretar.MojProfil
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class IzmeniProfil : Window
    {
        IUserController userController;
        private IzmeniProfil()
        {
            InitializeComponent();
            var app = System.Windows.Application.Current as App;
            userController = app.userController;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        public void postaviVrednosti()
        {
            var app = System.Windows.Application.Current as App;
            Ime.Text = app._currentUser.Name;
            Prezime.Text = app._currentUser.Surname;
            Telefon.Text = app._currentUser.Contact.Number;
            DatumRodjenja.Text = app._currentUser.BirthDate.ToShortDateString();
            Adresa.Text = app._currentUser.BirthPlace.PostNumber + ", " + app._currentUser.Residence.Street +
                "," + app._currentUser.Residence.Number + ", " + app._currentUser.Residence.City.Name
                + ", " + app._currentUser.Residence.City.country.Name;
            Biografija.Text = MojProfil.Instance.Biografija.Text;
            image.Source = MojProfil.Instance.Slika.Source;
        }
        
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private static IzmeniProfil instance = null;
        public static IzmeniProfil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IzmeniProfil();
                }
                return instance;
            }
        }

        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(Telefon.Text, out _)) {
                MessageBox.Show(Secretary.Properties.Langs.Lang.numberError, Secretary.Properties.Langs.Lang.numberError, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MojProfil mojProfil = MojProfil.Instance;
            PostaviVrednosti(mojProfil);
            this.Visibility = Visibility.Collapsed;
            mojProfil.initializeProfile();
            mojProfil.ShowDialog();
        }

        private void PostaviVrednosti(MojProfil mojProfil)
        {
            String[] adresaDeo = Adresa.Text.Split(',');
            var app = System.Windows.Application.Current as App;
            app._currentUser.Name = Ime.Text;
            app._currentUser.Surname = Prezime.Text;
            app._currentUser.Contact.Number = Telefon.Text;
            app._currentUser.BirthDate = Convert.ToDateTime(DatumRodjenja.Text);
            app._currentUser.Residence.City.PostNumber = Convert.ToInt32(adresaDeo[0]);
            app._currentUser.Residence.Street = adresaDeo[1].Substring(1);
            app._currentUser.Residence.Number = Convert.ToInt32(adresaDeo[2]);
            app._currentUser.Residence.City.Name = adresaDeo[3].Substring(1);
            app._currentUser.Residence.City.Country.Name = adresaDeo[4].Substring(1);
            mojProfil.Biografija.Text = Biografija.Text;
            mojProfil.Slika.Source = image.Source;
            userController.Update(app._currentUser);


        }

        private void ButtonClickPostaviSliku(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            Uri fileUri = new Uri(openFileDialog.FileName);
            image.Source = new BitmapImage(fileUri);
        }

        private void ButtonClickPocetna(object sender, RoutedEventArgs e)
        {
            MojProfil profil = MojProfil.Instance;
            this.Visibility = Visibility.Collapsed;
            profil.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            profil.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonClickIzmeni(object sender, RoutedEventArgs e)
        {
            adresa adresaa = adresa.Instance;
            adresaa.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adresaa.ShowDialog();
        }
    }
}
