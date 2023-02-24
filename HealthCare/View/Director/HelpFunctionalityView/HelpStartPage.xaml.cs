using HCIProjekat.BlogView;
using HCIProjekat.LekoviView;
using HCIProjekat.OpremaView;
using HCIProjekat.ProstorijeView;
using HCIProjekat.RenoviranjaView;
using HCIProjekat.View.ObavestenjeView;
using HCIProjekat.View.ReportView;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class HelpStartPage : Page
    {

        public HelpStartPage()
        {
            InitializeComponent();
        }
        

        private void linkZaposleni_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void linkLekovi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Lekovi(), false);
        }

        private void linkProstorije_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Prostorije(), false);
        }

        private void linkOprema_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Oprema(), false);
        }

        private void linkRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Renoviranja(), false);
        }

        private void linkBlog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Blog(), false);
        }

        private void linkObavestenja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Obavestenje(), false);
        }

        private void linkIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new FilterZauzetostiLekara(), false);
        }
    }
}
