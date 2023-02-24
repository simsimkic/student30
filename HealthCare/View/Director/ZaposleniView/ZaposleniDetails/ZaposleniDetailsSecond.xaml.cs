using Controller.UserControllers;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using Model.Users;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAddSecond.xaml
    /// </summary>
    public partial class ZaposleniDetailsSecond : Page
    {
        private readonly IUserController _userController;

        public Doctor Staff { get; set; }
        public UserControl User;
        public ZaposleniDetailsSecond(UserControl user)
        {
            this.User = user;
            var app = Application.Current as App;
            _userController = app.userController;


            Staff = (Doctor)_userController.GetFromID(((EmployeeItem)user).Id);
            InitializeComponent();

            DataContext = this;
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniEditFirst(User), false);
        }
    }
}
