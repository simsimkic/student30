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
    /// Interaction logic for LoginStaff.xaml
    /// </summary>
    public partial class LoginStaff : Page
    {
        public LoginStaff()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            var app = Application.Current as App;
            User user = app.userController.LoginStaff(username, password);
            if (user == null)
            {
                return;
            }
            app._currentUser = user;
            if (user.GetType() == typeof(Model.Users.Director))
            {
                var window = Window.GetWindow(this);
                HCIProjekat.MainWindow mainWindow = new HCIProjekat.MainWindow();
                window.Hide();
                mainWindow.Show();
            }
            if (user.GetType() == typeof(Model.Users.Doctor))
            {
                var window = Window.GetWindow(this);
                HealthCareClinic.MainWindow mainWindow = new HealthCareClinic.MainWindow();
                window.Hide();
                mainWindow.Show();
            }
            if (user.GetType() == typeof(Model.Users.Secretary))
            {
                var window = Window.GetWindow(this);
                Sekretar.MainWindow mainWindow = new Sekretar.MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
                window.Close();
                
            }
            MainWindow patientWindow = (MainWindow)Window.GetWindow(this);
            patientWindow.CurrentUser = app._currentUser;
            this.NavigationService.Navigate(new Uri("/View/Patient/HomePage.xaml", UriKind.Relative));
        }
    }
}
