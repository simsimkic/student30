using HealthCare;
using HealthCare.View.Secretary.Izvestaj;
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

namespace Sekretar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RoutedCommand newCmd = new RoutedCommand();
        RoutedCommand cmdSale = new RoutedCommand();
        RoutedCommand cmdIzvestaj = new RoutedCommand();
        RoutedCommand cmdZahtev = new RoutedCommand();
        RoutedCommand cmdBlog = new RoutedCommand();
        public static String tema = "svetla";
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newCmd.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            cmdSale.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            cmdIzvestaj.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            cmdZahtev.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
            cmdBlog.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(newCmd, ButtonClickPregledi));
            CommandBindings.Add(new CommandBinding(cmdSale, ButtonClickSale));
            CommandBindings.Add(new CommandBinding(cmdIzvestaj, ButtonClickIzvestaj));
            CommandBindings.Add(new CommandBinding(cmdZahtev, ButtonClickSmestajPacijenata));
            CommandBindings.Add(new CommandBinding(cmdBlog, ButtonClickBlog));
        }

        private void ButtonClickPregledi(object sender, RoutedEventArgs e)
        {
            Pregledi.Pregledi pregledi = Pregledi.Pregledi.Instance;
            pregledi.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            pregledi.ResetComponents();
            pregledi.ShowDialog();
        }

        private void ButtonClickSale(object sender, RoutedEventArgs e)
        {
            Sale.Sale sale = Sale.Sale.Instance;
            sale.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            sale.ResetComponents();
            sale.ShowDialog();
        }

        private void ButtonClickSmestajPacijenata(object sender, RoutedEventArgs e)
        {
            Zahtevi.ZahteviZaSmestanje zahtevi = Zahtevi.ZahteviZaSmestanje.Instance;
            zahtevi.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Visibility = Visibility.Collapsed;
            zahtevi.ResetComponents();
            zahtevi.ShowDialog();
        }

        private void ButtonClickIzvestaj(object sender, RoutedEventArgs e)
        {
            SelektujDan izvestaj = new SelektujDan();
            izvestaj.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            izvestaj.ShowDialog();
        }

        private void ButtonClickBlog(object sender, RoutedEventArgs e)
        {
            Blog.Blog blog = Blog.Blog.Instance;
            blog.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Visibility = Visibility.Collapsed;
            blog.ShowDialog();
        }

        private void ButtonClickObavestenja(object sender, RoutedEventArgs e)
        {
            Obavestenja.Obavestenja obavestenja = Obavestenja.Obavestenja.Instance;
            obavestenja.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            obavestenja.ResetComponents();
            obavestenja.ShowDialog();
        }

        private void ButtonClickMojProfil(object sender, RoutedEventArgs e)
        {

            MojProfil.MojProfil mojProfil = MojProfil.MojProfil.Instance;
            mojProfil.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Visibility = Visibility.Collapsed;
            mojProfil.initializeProfile();
            mojProfil.ShowDialog();

        }

        private void ButtonClickPodesavanja(object sender, RoutedEventArgs e)
        {
            Podesavanja.Podesavanja podesavanja = new Podesavanja.Podesavanja();
            podesavanja.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            podesavanja.ShowDialog();
        }

        private void ButtonClickOdjava(object sender, RoutedEventArgs e)
        {
            (Application.Current as App)._currentUser = null;
            ResolveView.MainWindow window = new ResolveView.MainWindow();
            this.Close();
            window.Show();            
        }

        

        private void OdjavaClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (Application.Current as App)._currentUser = null;
            ResolveView.MainWindow window = new ResolveView.MainWindow();
            window.Show();
            Application.Current.MainWindow = window;
            window.CurrentUser = null;
            
            
        }

    }
}
