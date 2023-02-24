using Controller.UserControllers;
using HealthCare;
using HealthCareClinic.View.Doctor;
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
using System.Windows.Shapes;

namespace HealthCareClinic.HCI
{
    /// <summary>
    /// Interaction logic for EditMyProfilePassword.xaml
    /// </summary>
    public partial class EditMyProfilePassword : Window
    {
        private const string INPUT_ERROR_LENGHT = "Password mora imati minimalno 7 karaktera";
        private const string INPUT_ERROR_DIFFERENT = "Nove sifre se ne poklapaju";
        private const string INPUT_ERROR_CURRENT_PASSWORD = "Stara sifra nije tacna";

        
        private readonly IUserController _userController;
        private Doctor _user;
        public EditMyProfilePassword(Doctor user)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            _user = user;

            var app = Application.Current as App;
            _userController = app.userController;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void btnSaveEditProfilePassword_Click(object sender, RoutedEventArgs e)
        {
            if (isValidInputData())
            {
                _user.Password = pbNewPassword1.Password;
                _userController.Update(_user);
                this.Close();
            }
        }

        private bool isValidInputData()
        {
            if (pbNewPassword1.Password.Length<7 || pbNewPassword2.Password.Length<7)
            {
                ShowError(INPUT_ERROR_LENGHT);
                return false;
            }else if (!pbNewPassword1.Password.Equals(pbNewPassword2.Password))
            {
                ShowError(INPUT_ERROR_DIFFERENT);
                return false;
            }else if (!pbOldPassword.Password.Equals(_user.Password))
            {
                ShowError(INPUT_ERROR_CURRENT_PASSWORD);
                return false;
            }
            return true;
        }

        private void ShowError(string s) => new MessageDialog(s, this).ShowDialog();

    }
}
