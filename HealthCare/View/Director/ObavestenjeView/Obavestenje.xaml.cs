using Controller.CommunicationControllers;
using Controller.UserControllers;
using HCIProjekat.View.ObavestenjeView.DataView;
using HealthCare;
using HealthCare.View.Director.ObavestenjeView.Converter;
using Model.Communication;
using Model.Users;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCIProjekat.View.ObavestenjeView
{
    /// <summary>
    /// Interaction logic for Obavestenje.xaml
    /// </summary>
    public partial class Obavestenje : Page
    {
        public ObservableCollection<UserControl> Notifications { get; set; }
        private INotificationController notificationController;
        private readonly IUserController userController;

        public Obavestenje()
        {

            var app = Application.Current as App;
            notificationController = app.notificationController;
            userController = app.userController;


            List<Notification> notifications = notificationController.GetNotificationForUser(app._currentUser).ToList();
            foreach (Notification n in notifications)
                n.createdBy = (Staff)userController.GetFromID(n.createdBy.Id);

            Notifications = new ObservableCollection<UserControl>(NotificationItemConverter.ConvertNotificationListToNotificationViewList(notifications));

            InitializeComponent();

            DataContext = this;
        }

        private void addNotification_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ObavestenjeAdd(), false);
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                MainWindow.AppWindow.navigateWithTitleChange(new ObavestenjeDetalji((NotificationItem)listaObavestenja.SelectedItem), false);
            }
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.navigateWithTitleChange(new ObavestenjeDetalji((NotificationItem)listaObavestenja.SelectedItem), false);
        }
    }
}
