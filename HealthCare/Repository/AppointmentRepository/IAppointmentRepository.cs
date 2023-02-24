// File:    IAppointmentRepository.cs
// Created: Sunday, May 24, 2020 22:34:47
// Purpose: Definition of Interface IAppointmentRepository

using Model.Appointment;
using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Repository.AppointmentRepository
{
   public interface IAppointmentRepository : IRepository<Appointment,int>
   {
      IEnumerable<Appointment> GetAppointmentForDoctor(User doctor);
      
      IEnumerable<Appointment> GetAppointmentForRoom(Room room);
      IEnumerable<Appointment> GetAppointmentForRoomForDate(Room room, DateTime date);
      
      IEnumerable<Appointment> GetAppointmentForPatient(User patient);
      
      IEnumerable<Appointment> GetAppointmentForDate(DateTime requestDate);
      
      IEnumerable<Appointment> GetAppointmentForDoctorForDate(Model.Users.User doctor, DateTime time);
            
      bool HasPatientAppointment(Model.Users.User patient);

      bool DoesDoctorHaveFutureAppointment(Model.Users.User doctor);
      bool DoesRoomHasFutureAppointment(Room room);

    }
}