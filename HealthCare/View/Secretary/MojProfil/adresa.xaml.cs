using Sekretar.MojProfil;
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

namespace HealthCare.View.Secretary.MojProfil
{
    /// <summary>
    /// Interaction logic for adresa.xaml
    /// </summary>
    public partial class adresa : Window
    {
        private adresa()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static adresa instance = null;
        public static adresa Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new adresa();
                }
                return instance;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonClickPotvrdi(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ZIP.Text, out _))
            {
                MessageBox.Show(Secretary.Properties2.Langs.Lang.zipError, Secretary.Properties2.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            IzmeniProfil mojProfil = IzmeniProfil.Instance;
            PostaviVrednosti(mojProfil);
            this.Visibility = Visibility.Collapsed;
        }

        private void PostaviVrednosti(IzmeniProfil mojProfil)
        {
            mojProfil.Adresa.Text = ZIP.Text + ", " + Ulica.Text +  "," + Broj.Text + ", " + Grad.Text + ", " + Drzava.Text;
        }

    }
}
