using Controller.CommunicationControllers;
using HealthCare;
using HealthCareClinic.View.Converter;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HealthCareClinic.View.Model
{
    /// <summary>
    /// Interaction logic for NotificationDataView.xaml
    /// </summary>
    public partial class NotificationDataView : UserControl
    {
        private readonly INotificationController _notificationController;
        public ObservableCollection<UserControl> Data { get; set; }
        public NotificationDataView()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;

            _notificationController = app.notificationController;


            // ovde samo da se izmeni getForDoctor...  // 
            Data = new ObservableCollection<UserControl>(NotificationConverter
               .ConvertNotificationListToTNotificationViewList(_notificationController.GetNotificationForUser(app._currentUser).ToList()));
        }

    }
}
