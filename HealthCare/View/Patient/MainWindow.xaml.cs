using Model.Users;
using Model.MedicalRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HealthCare;
using System.CodeDom;

namespace ResolveView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private User currentUser;
        public User CurrentUser 
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            
            InitializeComponent();
            this.DataContext = this;
            
            MinimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            MaximizeButton.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            CloseButton.Click += (s, e) => Close();
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/HomePage.xaml", UriKind.Relative));
            var app = Application.Current as App;
            CurrentUser = app._currentUser;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindowFrame.CanGoBack)
            {
                MainWindowFrame.GoBack();
                scrollViewer.ScrollToTop();
            }  
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/HomePage.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/Doctors.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void FAQ_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/FrequentlyAskedQuestions.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/Notifications.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void AskQuestion_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if(app._currentUser == null)
            {
                return;
            }
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/AskQuestion.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            if (app._currentUser == null)
            {
                return;
            }
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/MakeAppointment.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void Blog_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/Blog.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/Login.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = null;
            var app = Application.Current as App;
            app._currentUser = null;
            HomePage_Click(sender, e);
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/EditProfile.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void MyTherapy_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/MyTherapy.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }

        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Navigate(new Uri(@"/View/Patient/Appointments.xaml", UriKind.Relative));
            scrollViewer.ScrollToTop();
        }
    }
}
