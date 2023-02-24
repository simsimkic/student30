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
    /// Interaction logic for EditProfile.xaml
    /// </summary>
    public partial class EditProfile : Page
    {

        public Patient UserData { get; set; }

        public EditProfile()
        {
            var app = Application.Current as HealthCare.App;
            UserData = (Patient)app._currentUser;
            

            this.DataContext = this;
            InitializeComponent();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            successfullyEditedTextBlock.Visibility = Visibility.Visible;
        }

        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            successfullyEditedPasswordTextBlock.Visibility = Visibility.Visible;
        }
    }
}
