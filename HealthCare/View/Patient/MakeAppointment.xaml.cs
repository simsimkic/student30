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
    /// Interaction logic for MakeAppointment.xaml
    /// </summary>
    public partial class MakeAppointment : Page
    {
        public MakeAppointment()
        {
            InitializeComponent();
        }

        private void ChooseAppointment_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri(@"/View/Patient/ChooseAppointment.xaml", UriKind.Relative));
        }

        private void RecommendAppointment_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri(@"/View/Patient/RecommendAppointment.xaml", UriKind.Relative));
        }
    }
}
