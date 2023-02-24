/***********************************************************************
 * Module:  NotificationService.cs
 * Purpose: Definition of the Class Model.Communication.NotificationService
 ***********************************************************************/

using Model.Communication;
using Model.Users;
using Service.CommunicationServices;
using System;
using System.Collections.Generic;

namespace Controller.CommunicationControllers
{
   public class NotificationController : INotificationController
   {
        private Service.CommunicationServices.INotificationService _service;

        public NotificationController(INotificationService serv)
        {
            _service = serv;
        }

        public Notification Create(Notification entity) => _service.Create(entity);


        public IEnumerable<Notification> GetNotificationForUser(User user) => _service.GetNotificationForUser(user);
    }
}