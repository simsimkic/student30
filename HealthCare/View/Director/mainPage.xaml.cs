using HCIProjekat.LekoviView;
using HCIProjekat.OpremaView;
using HCIProjekat.ProstorijeView;
using HCIProjekat.View.HelpWizardView;
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

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void Zaposleni_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
            if (MainWindow.APP_HELP)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                MainWindow.AppWindow.HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
            if (MainWindow.APP_HELP)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                MainWindow.AppWindow.HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
            if (MainWindow.APP_HELP)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                MainWindow.AppWindow.HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Prostorije_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
            if (MainWindow.APP_HELP)
            {
                MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Visible;
                MainWindow.AppWindow.HelpFrame.Navigate(new EntityMenu());
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
            MainWindow.AppWindow.navigateToRootPage(new HospitalStatisticsMonthy(), false);
        }
    }
}
