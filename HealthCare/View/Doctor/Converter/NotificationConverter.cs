using HealthCareClinic.View.Model;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareClinic.View.Converter
{
    class NotificationConverter : AbstractConverter
    {
        public static NotificationView ConvertNotificationToNotificationView(Notification notification)
           => new NotificationView
           {
               Title = notification.Title,
               Text = notification.Text,
               Date = notification.Date,
               CreatedBy= notification.createdBy
           };


        public static IList<NotificationView> ConvertNotificationListToTNotificationViewList(IList<Notification> notifications)
            => ConvertEntityListToViewList(notifications, ConvertNotificationToNotificationView);
    }
}
