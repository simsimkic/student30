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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/Patient/Register.xaml", UriKind.Relative));
        }

        private void LoginStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/View/Patient/LoginStaff.xaml", UriKind.Relative));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            var app = Application.Current as App;
            User user = app.userController.LoginUser(username, password);
            if(user == null)
            {
                return;
            }
            app._currentUser = user;
            MainWindow patientWindow = (MainWindow)Window.GetWindow(this);
            patientWindow.CurrentUser = app._currentUser;
            this.NavigationService.Navigate(new Uri("/View/Patient/HomePage.xaml", UriKind.Relative));
        }
    }
}
