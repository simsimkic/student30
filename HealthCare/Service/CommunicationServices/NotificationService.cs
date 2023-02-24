/***********************************************************************
 * Module:  NotificationService.cs
 * Purpose: Definition of the Class Model.Communication.NotificationService
 ***********************************************************************/

using HealthCare;
using Model.Communication;
using Model.Users;
using Repository.CommunicationRepositories;
using System;
using System.Windows;
using System.Collections.Generic;
using Model.Drug;
using Model.Appointment;
using HealthCare.Util;
using Repository.UserRepositories;
using Model.MedicalRecords;
using System.Linq;

namespace Service.CommunicationServices
{
   public class NotificationService : INotificationService
   {
        private Repository.CommunicationRepositories.INotificationRepository _repository;
        private IUserRepository _userRepository;

        public NotificationService(INotificationRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public Notification Create(Notification entity) => _repository.Create(entity);
        
        public IEnumerable<Notification> GetNotificationForUser(User user) => _repository.GetNotificationForUser(user);

        public Notification CreateNotificationForApprovingAbsence(Absence absence)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.ABSENCE_APPROVAL_APPROVED_TEXT + absence.StartDate.ToShortDateString() + " - " + absence.EndDate.ToShortDateString(),
                Title = NotificationMessages.ABSENCE_APPROVAL_APPROVED_TITLE,
                users = new List<User>() { new Staff() { Id = absence.staff.Id } }
            });
        public Notification CreateNotificationForRejectingAbsence(Absence absence, string rejectRason)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.ABSENCE_APPROVAL_REJECTED_TEXT + absence.StartDate.ToShortDateString() + " - " + absence.EndDate.ToShortDateString() + " " + rejectRason,
                Title = NotificationMessages.ABSENCE_APPROVAL_REJECTED_TITLE,
                users = new List<User>() { new Staff() { Id = absence.staff.Id } }
            });

        public Notification CreateNotificationForCreatingDrug(Drug drug)
            => Create(new Notification()
            {
                    createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                    Date = DateTime.Now,
                    Text = NotificationMessages.DRUG_CREATE_TITLE,
                    Title = NotificationMessages.DRUG_CREATE_TEXT + "Naziv leka: " + drug.Name,
                    users = new List<User>() { new Staff() { Id = drug.whoApprovesDrug.Id } }
            });

        public Notification CreateNotificationForUpdateDrug(Drug drug)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.DRUG_UPDATE_TITLE,
                Title = NotificationMessages.DRUG_UPDATE_TEXT + "Naziv leka: " + drug.Name,
                users = new List<User>() { new Staff() { Id = drug.whoApprovesDrug.Id } }
            });

        public Notification CreateNotificationForCreateAbsence()
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date=DateTime.Now,
                Text= NotificationMessages.ABSENCE_CREATE_TEXT,
                Title= NotificationMessages.ABSENCE_CREATE_TITLE,
                users = new List<User>() { new Model.Users.Director() { Id = _userRepository.GetDirector().Id } }
            });

        public Notification CreateNotificationForApprovedDrug(Drug drug)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.DRUG_APPROVAL_APPROVED_TEXT + " ID: " + drug.Id + " NAME: " + drug.Name,
                Title = NotificationMessages.DRUG_APPROVAL_APPROVED_TITLE,
                users = new List<User>() { new Model.Users.Director() { Id = _userRepository.GetDirector().Id } }
            });

        public Notification CreateNotificationForRejectDrug(Drug drug)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.DRUG_APPROVAL_REJECTED_TEXT + " ID: " + drug.Id + " NAME: " + drug.Name,
                Title = NotificationMessages.DRUG_APPROVAL_REJECTED_TITLE,
                users = new List<User>() { new Model.Users.Director() { Id = _userRepository.GetDirector().Id } }
            });

        public Notification CreateNotificationForSmallAmount(Drug drug)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.DRUG_AMOUNT_TEXT + "\n ID: " + drug.Id + " NAME: " + drug.Name,
                Title = NotificationMessages.DRUG_AMOUNT_TITLE,
                users = new List<User>() { new Model.Users.Director() { Id = _userRepository.GetDirector().Id } }
            });

        public Notification CreateNotificationForCreateHospitalTreatmentRequest(HospitalTreatment refferal)
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.REFFERAL_HOSPITALTREATMENT_CREATE_TEXT + refferal.Date.ToString("dd MMM yyyy"),
                Title = NotificationMessages.REFFERAL_HOSPITALTREATMENT_CREATE_TITLE,
                users = _userRepository.GetAllSecretary().ToList()
            });

        public Notification CreateNotificationForCreateUrgentSurgeryRequest()
            => Create(new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = NotificationMessages.REFFERAL_SURGERY_CREATE_TEXT,
                Title = NotificationMessages.REFFERAL_SURGERY_CREATE_TITLE,
                users = _userRepository.GetAllSecretary().ToList()
            });

        public void AppointmentCancelled(Appointment appointment)
        {
            Notification notification = new Notification()
            {
                createdBy = new Staff() { Id = (Application.Current as App)._currentUser.Id },
                Date = DateTime.Now,
                Text = appointment.Type + " " + appointment.StartDateTime.ToString() + " - " + appointment.EndDateTime.ToString() + " je otkazan",
                Title = "Otkazan termin",
                users = new List<User>() { new Patient(){ Id = appointment.Patient.Id } }
            };
            Create(notification);
        }

        
    }
}