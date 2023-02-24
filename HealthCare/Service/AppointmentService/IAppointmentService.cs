// File:    IAppointmentService.cs
// Created: Friday, May 22, 2020 22:26:29
// Purpose: Definition of Interface IAppointmentService

using HealthCare.Model.Appointment;
using HealthCare.Util;
using Model.Appointment;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Service.AppointmentService
{
   public interface IAppointmentService : IService<Appointment,int>
   {
      IEnumerable<Appointment> GetAppointmentForDoctor(Model.Users.User doctor);
      
      IEnumerable<Appointment> GetAppointmentForRoom(Model.Rooms.Room room);
      
      IEnumerable<Appointment> GetAppointmentForPatient(Model.Users.User patient);
      
      IEnumerable<Appointment> GetAppointmentForDate(DateTime requestDate);
      
      IEnumerable<Appointment> GetAppointmentForDoctorForDate(Model.Users.User doctor, DateTime time);
      
      IEnumerable<Appointment> GetFreeAppointments(Model.Users.User patient, Model.Users.User doctor, DateTime date, Model.MedicalRecords.TreatmentType treatmentType);

      Appointment RecommendAppointment(RecommendAppointmentParameters parameters);

      IEnumerable<Appointment> GetFreeAppointmentsForSurgery(Model.Users.User patient, Model.Users.User doctor, DateTime date, TimeSpan appointmentDuration);


        bool HasPatientAppointment(Model.Users.User patient);
   
   }
}