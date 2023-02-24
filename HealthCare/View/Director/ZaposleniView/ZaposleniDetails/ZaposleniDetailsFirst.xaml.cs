using Controller.UserControllers;
using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare;
using Model.Users;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.ZaposleniView
{
    /// <summary>
    /// Interaction logic for ZaposleniAdd.xaml
    /// </summary>
    public partial class ZaposleniDetailsFirst : Page
    {
        private readonly IUserController _userController;
        public Staff Staff { get; set; }
        public UserControl User { get; set; }
        public ZaposleniDetailsFirst(UserControl user)
        {
            this.User = user;
            var app = Application.Current as App;
            _userController = app.userController;


            Staff = (Staff)_userController.GetFromID(((EmployeeItem)user).Id);
            Staff.Picture = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Staff.Picture);
            InitializeComponent();

            DataContext = this;
        }

        private void withdraw_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateToRootPage(new Zaposleni(), false);
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniEditFirst(User), false);
        }

        private void viewNext_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ZaposleniDetailsSecond(User), false);
        }


    }
}
