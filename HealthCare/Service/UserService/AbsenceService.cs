/***********************************************************************
 * Module:  AbsenceService.cs
 * Purpose: Definition of the Class Service.AbsenceService
 ***********************************************************************/

using Controller.CommunicationControllers;
using HealthCare;
using Model.Communication;
using Model.Users;
using Repository.AppointmentRepository;
using Repository.CommunicationRepositories;
using Repository.UserRepositories;
using Service.CommunicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Service.UserService
{
   public class AbsenceService : IAbsenceService
   {
      private Repository.UserRepositories.IAbsenceRepository _repository;
        private IAppointmentRepository _appointmentRepository;
        private INotificationService _notificationService;
        public AbsenceService(IAbsenceRepository repository, IAppointmentRepository appointmentRepository, INotificationService notificationService)
        {
            _repository = repository;
            _appointmentRepository = appointmentRepository;
            _notificationService = notificationService;
        }
        public void ApproveAbsence(Absence absence)
        {
            absence.Approved = true;
            _notificationService.CreateNotificationForApprovingAbsence(absence);
            Update(absence);
        }

        public Absence Create(Absence entity) {

            if (!DoesDoctorHaveAppointments(entity) && !DoesDoctorHaveAbsence(entity))
            {
                _notificationService.CreateNotificationForCreateAbsence();
                return _repository.Create(entity);
            }

            return null;
        }

        private bool DoesDoctorHaveAbsence(Absence entity) 
            => GetEmployeesFutureAbsence(entity.staff).Where(x => ((entity.StartDate.Date <= x.StartDate.Date && entity.EndDate.Date >= x.StartDate.Date) ||
                                                                (entity.StartDate.Date >= x.StartDate.Date && entity.StartDate.Date <= x.EndDate.Date))).Any();

        private bool DoesDoctorHaveAppointments(Absence entity)
        {
            
            DateTime start = entity.StartDate.Date;
            while (start <= entity.EndDate.Date)
            {
                if (_appointmentRepository.GetAppointmentForDoctorForDate(entity.staff, start).Count()!=0)
                    return true;
                start = start.Date.AddDays(1);
            }
            return false;
        }

        public Absence Delete(Absence entity) => _repository.Delete(entity);

        public IEnumerable<Absence> GetEmployeesAbsenceHistory(User staff) => _repository.GetEmployeesAbsenceHistory(staff);

        public IEnumerable<Absence> GetEmployeesFutureAbsence(User staff) => _repository.GetEmployeesFutureAbsence(staff);

        public IEnumerable<Absence> GetAll() => _repository.GetAll();

        public IEnumerable<Absence> GetNonApprovedAbsence() => _repository.GetNonApprovedAbsence();

        public Absence GetFromID(int id) => _repository.GetFromID(id);

        public void RejectAbsence(Absence absence, string rejectRason)
        {
            _notificationService.CreateNotificationForRejectingAbsence(absence, rejectRason);
            Delete(absence);
        }

        public Absence Update(Absence entity) => _repository.Update(entity);

    }
}