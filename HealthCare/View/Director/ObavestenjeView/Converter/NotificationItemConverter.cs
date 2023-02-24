using HCIProjekat.View.ObavestenjeView.DataView;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.Director.ObavestenjeView.Converter
{
    class NotificationItemConverter : AbstractConverter
    {
        public static NotificationItem ConvertNotificationToNotificationView(Notification notification)
           => new NotificationItem
           {   
               Title = notification.Title,
               Text = notification.Text,
               Date = notification.Date,
               Author = notification.createdBy.Name + " " + notification.createdBy.Surname
           };

        public static IList<NotificationItem> ConvertNotificationListToNotificationViewList(IList<Notification> notifications)
            => ConvertEntityListToViewList(notifications, ConvertNotificationToNotificationView);
    }
}
