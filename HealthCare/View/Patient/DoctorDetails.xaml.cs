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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for DoctorDetails.xaml
    /// </summary>
    public partial class DoctorDetails : Page
    {

        public Doctor DoctorData { get; set; }

        public DoctorDetails(Doctor doctor)
        {
            DoctorData = doctor;
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if (app._currentUser == null)
            {
                return;
            }
            this.NavigationService.Navigate(new Survey(DoctorData));
        }
    }
}
