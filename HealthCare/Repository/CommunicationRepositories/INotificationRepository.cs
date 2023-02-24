// File:    INotificationRepository.cs
// Created: Sunday, May 24, 2020 22:49:34
// Purpose: Definition of Interface INotificationRepository

using Model.Communication;
using System;
using System.Collections.Generic;

namespace Repository.CommunicationRepositories
{
   public interface INotificationRepository : ICreate<Notification>, IGetAll<Notification>
   {
      IEnumerable<Notification> GetNotificationForUser(Model.Users.User user);
   
   }
}