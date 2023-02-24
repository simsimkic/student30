using Secretary;
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

namespace Sekretar.Podesavanja
{
    /// <summary>
    /// Interaction logic for Podesavanja.xaml
    /// </summary>
    public partial class Podesavanja : Window
    {

        public Podesavanja()
        {
            InitializeComponent();
            if (MainWindow.tema.Equals("svetla"))
            {
                Svetla.IsChecked = true;
            }
            else if (MainWindow.tema.Equals("siva"))
                Siva.IsChecked = true;
            else
                Tamna.IsChecked = true;

            if (Secretary.Properties.Settings.Default.languageCodes.Equals("en-US"))
                Engleski.IsChecked = true;
            else
                Srpski.IsChecked = true;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (Secretary.Properties.Settings.Default.languageCodes.Equals("sr-RS"))
            {
                Srpski.IsChecked = true;
            }
            else
                Engleski.IsChecked = true;

            if (e.Key == Key.Escape)
                Close();
        }
        private static Podesavanja instance = null;
        public static Podesavanja Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Podesavanja();
                }
                return instance;
            }
        }
        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
        private void LightBlueChecked(object sender, RoutedEventArgs e)
        {
        }
        private void grayChecked(object sender, RoutedEventArgs e)
        {
            
           
        }
        private void yellowChecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void SrpskiChecked(object sender, RoutedEventArgs e)
        {
        }

        private void EngleskiChecked(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonPotvrdi(object sender, RoutedEventArgs e)
        {

            if ((bool)Siva.IsChecked) {
                Style style = new Style
                {
                    TargetType = typeof(Window)
                };
                Style style1 = new Style
                {
                    TargetType = typeof(Button)
                };

                style.Setters.Add(new Setter(Window.BackgroundProperty, Brushes.Gray));
                Application.Current.Resources["MyStyle"] = style;
                MainWindow.tema = "siva";
                style1.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightGray));
                style1.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                Application.Current.Resources["MyStyleButton"] = style1;
                this.Visibility = Visibility.Collapsed;
            }
            if ((bool)Svetla.IsChecked) {
                Style style = new Style
                {
                    TargetType = typeof(Window)
                };
                Style style1 = new Style
                {
                    TargetType = typeof(Button)
                };
                style.Setters.Add(new Setter(Window.BackgroundProperty, Brushes.FloralWhite));
                Application.Current.Resources["MyStyle"] = style;
                MainWindow.tema = "svetla";
                style1.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.SteelBlue));
                style1.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
                Application.Current.Resources["MyStyleButton"] = style1;
                this.Visibility = Visibility.Collapsed;
            }
            if ((bool)Tamna.IsChecked)
            {
                Style style = new Style
                {
                    TargetType = typeof(Window)
                };
                Style style1 = new Style
                {
                    TargetType = typeof(Button)
                };

                Style style2 = new Style
                {
                    TargetType = typeof(Label)
                };

                Style style3 = new Style
                {
                    TargetType = typeof(RadioButton)
                };
                style.Setters.Add(new Setter(Window.BackgroundProperty, Brushes.DimGray));
                Application.Current.Resources["MyStyle"] = style;
                MainWindow.tema = "tamna";

                style1.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Black));
                style1.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
                Application.Current.Resources["MyStyleButton"] = style1;
                this.Visibility = Visibility.Collapsed;
            }

            if ((Secretary.Properties.Settings.Default.languageCodes.Equals("sr-RS") && Engleski.IsChecked == true)
                || (Secretary.Properties.Settings.Default.languageCodes.Equals("en-US") && Srpski.IsChecked == true))
            {
                if (Engleski.IsChecked == true)
                {
                    Secretary.Properties.Settings.Default.languageCodes = "en-US";
                    Secretary.Properties.Settings.Default.Save();
                }
                else
                {
                    Secretary.Properties.Settings.Default.languageCodes = "sr-RS";
                    Secretary.Properties.Settings.Default.Save();
                }
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(Secretary.Properties.Langs.Lang.settingsQuestion, Secretary.Properties.Langs.Lang.confirmation, System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            }
            else
                this.Visibility = Visibility.Collapsed;
        }
        private void ButtonOdustani(object sender, RoutedEventArgs e)
        {
            if (Secretary.Properties.Settings.Default.languageCodes.Equals("sr-RS"))
            {
                Srpski.IsChecked = true;
            }
            else
                Engleski.IsChecked = true;

            this.Visibility = Visibility.Collapsed;
        }
    }
}
