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

namespace HealthCare.View.Patient
{
    /// <summary>
    /// Interaction logic for SuccessfullyAskedQuestion.xaml
    /// </summary>
    public partial class SuccessfullyAskedQuestion : Page
    {
        public SuccessfullyAskedQuestion()
        {
            InitializeComponent();
            this.DataContext = this;
            content.BringIntoView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri(@"/View/Patient/HomePage.xaml", UriKind.Relative));
        }
    }
}
