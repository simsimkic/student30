/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.CommunicationRepositories
{
    public class NotificationRepository : INotificationRepository
    {
        public JSONStream<Notification> _stream;

        public NotificationRepository(JSONStream<Notification> stream)
        {
            _stream = stream;
        }

        public Notification Create(Notification entity)
        {
            List<Notification> lista = _stream.GetAll().ToList();
            lista.Add(entity);
            _stream.SaveAll(lista);
            return entity;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _stream.GetAll().ToList();
        }

        public IEnumerable<Notification> GetNotificationForUser(User user)
            => _stream.GetAll().Where(x => x.users.SingleOrDefault(y => y.Id == user.Id) != null);
    }
}