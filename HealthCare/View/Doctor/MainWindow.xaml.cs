using HealthCare;
using HealthCare.View.Doctor.Demo;
using HealthCareClinic.HCI;
using HealthCareClinic.View;
using HealthCareClinic.View.Doctor;
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
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace HealthCareClinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string activePage = "";
        private const string EXIT_DIALOG_MESSAGE = "Da li ste sigurni da zelite da izadjete?";
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(HandleKeyboard);
            btnToolBarExit.Focus();

            MainFrame.Navigate(new DefaultPage());
            var app = Application.Current as App;
            string doctorName= app._currentUser.Name + " " + app._currentUser.Surname;

            lblDoctorName.Content = doctorName;
        }



        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (MainFrame.Content.GetType().ToString() == "HealthCareClinic.View.DefaultPage")
                {
                    this.Close();
                }
                else
                {
                    MainFrame.Navigate(new DefaultPage());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrugApprovalDemo demo = new DrugApprovalDemo();
            demo.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool dialogResult = new SubmitCancelDialog(EXIT_DIALOG_MESSAGE, this).ShowDialog() ?? true;

            if (dialogResult)
            {
                ResolveView.MainWindow window = (ResolveView.MainWindow)Application.Current.MainWindow;
                window.CurrentUser = null;
                (Application.Current as App)._currentUser = null;
                Application.Current.MainWindow.Show();

            }

            if (!dialogResult)
                e.Cancel = true;
        }

        private void btnToolBarExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSoftwareRating_Click(object sender, RoutedEventArgs e)
        {
            SoftwareRating dialog = new SoftwareRating();
            dialog.ShowDialog();
        }

        private void btnToolBarNotification_Click(object sender, RoutedEventArgs e)
        {
            Notifications dialog = new Notifications();
            dialog.ShowDialog();
        }


        private void lblDoctorName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnToolBarExit.Focus();
            MainFrame.Navigate(new DefaultPage());
        }


        private void btnToolBarMyProfile_Click(object sender, RoutedEventArgs e)
        {
            btnToolBarExit.Focus();
            MainFrame.Navigate(new MyProfilePage());
        }

        private void btnToolBarFeedBack_Click(object sender, RoutedEventArgs e)
        {
            FeedBackList dialog = new FeedBackList();
            dialog.ShowDialog();
        }


    }
}
