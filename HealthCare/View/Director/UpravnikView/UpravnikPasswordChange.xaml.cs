using Controller.UserControllers;
using HCIProjekat.View.ConfirmationDialogsView;
using HealthCare;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace HCIProjekat.UpravnikView
{
    /// <summary>
    /// Interaction logic for UpravnikPasswordChange.xaml
    /// </summary>
    public partial class UpravnikPasswordChange : Page
    {

        private const string INVALID_OLD_PASSWORD_FIELD = "Stara lozinka nije validna!";
        private const string MANDATORY_NEW_PASSWORD_FIELD = "Nova lozinka je obavezno polje!";
        private const string MANDATORY_NEW_PASSWORD_SECOND_FIELD = "Ponovno unosenje nove lozinke je obavezno!";
        private const string INVALID_NEW_PASSWORD_FIELD = "Nova lozinka nije validna!";
        private const string INVALID_PASSWORD_MISSMATCH_FIELD = "Unete lozinke se ne poklapaju!";

        private readonly IUserController _userController;
        private Model.Users.Director director;
        private string _oldPassword;
        private string _newPasswordFirstTry;
        private string _newPasswordSecondTry;
        public UpravnikPasswordChange()
        {
            var app = Application.Current as App;
            _userController = app.userController;


            director = (Model.Users.Director)_userController.GetFromID(app._currentUser.Id);

            InitializeComponent();
            DataContext = this;
        }
        public string NewPasswordSecondTry
        {
            get { return _newPasswordSecondTry; }
            set
            {
                if (_newPasswordSecondTry != value)
                {
                    _newPasswordSecondTry = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NewPasswordFirstTry
        {
            get { return _newPasswordFirstTry; }
            set
            {
                if (_newPasswordFirstTry != value)
                {
                    _newPasswordFirstTry = value;
                    OnPropertyChanged();
                }
            }
        }
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                if (_oldPassword != value)
                {
                    _oldPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        private void ShowError(string s)
        {
            dialog.Visibility = Visibility.Visible;
            dialog.Children.Add(new MessageDialog(s, withdrawEvent));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void PasswordChange_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.navigateWithTitleChange(MainWindow.undoPages[MainWindow.undoPages.Count - 1], false);
        }

        private void withdrawEdit_Click(object sender, RoutedEventArgs e)
        {
            dialog.Children.Add(new ConfirmDialog("Odustanak od izmene", "Da li ste sigurni da zelite da odustanete od izmene lozinke?",
                                                   new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditEvent), "Nazad", "Odustani"));
            dialog.Visibility = Visibility.Visible;
        }
        private void withdrawEvent(object sender, RoutedEventArgs e)
        {
            dialog.Visibility = Visibility.Collapsed;
        }

        private void withdrawEditEvent(object sender, RoutedEventArgs e)
        {
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.navigateWithTitleChange(MainWindow.undoPages[MainWindow.undoPages.Count - 1], false);
        }

        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!isNewPasswordFirstFulfilled())
            {
                ShowError(MANDATORY_NEW_PASSWORD_FIELD);
            }
            else if (!isNewPasswordSecondFulfilled())
            {
                ShowError(MANDATORY_NEW_PASSWORD_SECOND_FIELD);
            }
            else if (!isOldPasswordValid())
            {
                ShowError(INVALID_OLD_PASSWORD_FIELD);
            }
            else if (!isNewPasswordValid())
            {
                ShowError(INVALID_NEW_PASSWORD_FIELD);
            }
            else if (!isNewPasswordMattched())
            {
                ShowError(INVALID_PASSWORD_MISSMATCH_FIELD);
            }
            else
            {
                dialog.Children.Add(new ConfirmDialog("Izmena lozinke", "Da li ste sigurni da zelite da izmenite lozinku?",
                                                       new RoutedEventHandler(withdrawEvent), new RoutedEventHandler(withdrawEditAccountEvent)));
                dialog.Visibility = Visibility.Visible;
            }
        }

        private bool isNewPasswordFirstFulfilled()
        {
            return NewFirst.Password != null && NewFirst.Password != "";
        }

        private bool isNewPasswordSecondFulfilled()
        {
            return NewSecond.Password != null && NewSecond.Password != "";
        }

        private bool isOldPasswordValid()
        {
            return director.Password == Old.Password;
        }

        private bool isNewPasswordValid()
        {
            return NewFirst.Password.Length >= 8;
        }

        private bool isNewPasswordMattched()
        {
            return NewFirst.Password == NewSecond.Password;
        }

        private void withdrawEditAccountEvent(object sender, RoutedEventArgs e)
        {
            director.Password = NewFirst.Password;
            _userController.Update(director);
            MainWindow.undoPages.RemoveAt(MainWindow.undoPages.Count - 1);
            MainWindow.AppWindow.navigateWithTitleChange(MainWindow.undoPages[MainWindow.undoPages.Count - 1], false);
        }
    }
}
