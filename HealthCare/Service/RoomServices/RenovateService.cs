/***********************************************************************
 * Module:  RenovateService.cs
 * Purpose: Definition of the Class Service.RenovateService
 ***********************************************************************/

using HealthCare.Util;
using Model.DataFiltration;
using Model.Rooms;
using Repository.AppointmentRepository;
using Repository.RoomRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.RoomServices
{
    public class RenovateService : IRenovateService
    {
        public Repository.RoomRepositories.IRenovateRepository _repository;
        public IAppointmentRepository _appointmentRepository;

        public RenovateService(IRenovateRepository repository, IAppointmentRepository appointmentRepository)
        {
            _repository = repository;
            _appointmentRepository = appointmentRepository;
        }

        public Renovate Create(Renovate entity) 
            => (!IsDesiredRenovationInRangeOfExistingRenovation(entity) && !DoesRoomHaveAppoointmentInRenovationPeriod(entity)) ? _repository.Create(entity) : null;

        public Renovate Delete(Renovate entity) => _repository.Delete(entity);

        private bool DoesRoomHaveAppoointmentInRenovationPeriod(Renovate renovate)
            => RenovationConverter.ConvertRenovateToDateTimeList(renovate).Where(x => HasApointmentThatDay(renovate.room, x)).Count() > 0;

        private bool HasApointmentThatDay(Room room, DateTime date) => _appointmentRepository.GetAppointmentForRoomForDate(room, date).Count() > 0;

        private bool IsDesiredRenovationInRangeOfExistingRenovation(Renovate renovate)
            => GetFutureRenovationsForRoom(renovate.room).Where(x => (renovate.DateStart.Date <= x.DateStart.Date && renovate.DateEnd.Date >= x.DateStart.Date) ||
                                                                     (renovate.DateStart.Date >= x.DateStart.Date && renovate.DateStart.Date <= x.DateEnd.Date)).Count() > 0;

        public IEnumerable<Renovate> GetAll() => _repository.GetAll();
        public IEnumerable<Renovate> GetFilteredRenovations(RenovateFilter renovateFilter) => _repository.GetFilteredRenovations(renovateFilter);

        public Renovate GetFromID(int id) => _repository.GetFromID(id);
        public IEnumerable<Renovate> GetFutureRenovationsForRoom(Room room) => _repository.GetFutureRenovationsForRoom(room);

        public bool IsRenovating(Room room, DateTime date) => _repository.IsRenovating(room, date);
    }
}