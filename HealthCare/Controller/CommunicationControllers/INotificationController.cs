// File:    INotificationController.cs
// Created: Saturday, May 23, 2020 0:06:43
// Purpose: Definition of Interface INotificationController

using Model.Communication;
using System;
using System.Collections.Generic;

namespace Controller.CommunicationControllers
{
   public interface INotificationController : ICreate<Notification>
   {
      IEnumerable<Notification> GetNotificationForUser(Model.Users.User user);
   
   }
}