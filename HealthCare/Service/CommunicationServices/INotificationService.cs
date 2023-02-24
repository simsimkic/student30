// File:    INotificationService.cs
// Created: Friday, May 22, 2020 22:47:43
// Purpose: Definition of Interface INotificationService

using Model.Appointment;
using Model.Communication;
using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Service.CommunicationServices
{
   public interface INotificationService : ICreate<Notification>
   {
      IEnumerable<Notification> GetNotificationForUser(Model.Users.User user);

        Notification CreateNotificationForApprovingAbsence(Absence absence);
        Notification CreateNotificationForRejectingAbsence(Absence absence, string rejectRason);
        Notification CreateNotificationForCreatingDrug(Drug drug);
        Notification CreateNotificationForUpdateDrug(Drug drug);

        Notification CreateNotificationForCreateAbsence();

        Notification CreateNotificationForApprovedDrug(Drug drug);

        Notification CreateNotificationForRejectDrug(Drug drug);

        Notification CreateNotificationForSmallAmount(Drug drug);

        Notification CreateNotificationForCreateHospitalTreatmentRequest(HospitalTreatment refferal);

        Notification CreateNotificationForCreateUrgentSurgeryRequest();


        void AppointmentCancelled(Appointment appointment);


   }
}