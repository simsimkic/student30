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

namespace HCIProjekat.View.HelpWizardView
{
    /// <summary>
    /// Interaction logic for AditionalOptionsHelp.xaml
    /// </summary>
    public partial class CalendarHelpSecond : Page
    {
        public CalendarHelpSecond()
        {
            InitializeComponent();

        }

        private void nastavak_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
        }

        private void odustanak_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.HelpFrame.Visibility = Visibility.Collapsed;
        }
    }
}
