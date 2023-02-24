// File:    IAppointmentController.cs
// Created: Saturday, May 23, 2020 0:04:04
// Purpose: Definition of Interface IAppointmentController

using HealthCare.Model.Appointment;
using Model.Appointment;
using System;
using System.Collections.Generic;

namespace Controller.AppointmenController
{
   public interface IAppointmentController : IController<Appointment,int>
   {
      IEnumerable<Appointment> GetAppointmentForDoctor(Model.Users.User doctor);
      
      IEnumerable<Appointment> GetAppointmentForRoom(Model.Rooms.Room room);
      
      IEnumerable<Appointment> GetAppointmentForPatient(Model.Users.User patient);
      
      IEnumerable<Appointment> GetAppointmentForDate(DateTime requestDate);
      
      IEnumerable<Appointment> GetAppointmentForDoctorForDate(Model.Users.User doctor, DateTime time);
      
      IEnumerable<Appointment> GetFreeAppointments(Model.Users.User patient,Model.Users.User doctor, DateTime date, Model.MedicalRecords.TreatmentType treatmentType);

      IEnumerable<Appointment> GetFreeAppointmentsForSurgery(Model.Users.User patient, Model.Users.User doctor, DateTime date, TimeSpan timeSpan);

        bool HasPatientAppointment(Model.Users.User patient);

        Appointment RecommendAppointment(RecommendAppointmentParameters parameters);


   }
}