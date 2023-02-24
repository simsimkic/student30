using Controller.CommunicationControllers;
using Controller.UserControllers;
using HealthCare;
using HealthCareClinic.View.Converter;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Sekretar.Obavestenja
{
    /// <summary>
    /// Interaction logic for Obavestenja.xaml
    /// </summary>
    public partial class Obavestenja : Window
    {
        public ObservableCollection<Notification> obavestenja
        {
            get;
            set;
        }
        private readonly IUserController userController;

        private readonly INotificationController _notificationController;

        private Obavestenja()
        {
            InitializeComponent();
            // obavestenja = new ObservableCollection<Notification>();

            var app = Application.Current as App;
            _notificationController = app.notificationController;
            userController = app.userController;

            obavestenja = new ObservableCollection<Notification>();
            List<Notification> notifications = _notificationController.GetNotificationForUser(app._currentUser).ToList();
            foreach (Notification n in notifications)
                obavestenja.Add(n);

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.DataContext = this;
        }

        public void ResetComponents()
        {
            var app = Application.Current as App;

            obavestenja = new ObservableCollection<Notification>();
            List<Notification> notifications = _notificationController.GetNotificationForUser(app._currentUser).ToList();
            foreach (Notification n in notifications)
                obavestenja.Add(n);
            this.DataContext = this;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        private static Obavestenja instance = null;
        public static Obavestenja Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Obavestenja();
                }
                return instance;
            }
        }


        private void Window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void ButtonClickProcitaj(object sender, RoutedEventArgs e)
        {
            try
            {
                Notification dataRowView = (Notification)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Title;
                ObavestenjaDetaljnije obavestenje = ObavestenjaDetaljnije.Instance;
                obavestenje.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                obavestenje.Naslov.Text = ProductName;
                obavestenje.TekstObavestenja.Text = dataRowView.Text;
                obavestenje.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
