// File:    IUserRepository.cs
// Created: Saturday, May 23, 2020 17:23:21
// Purpose: Definition of Interface IUserRepository

using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Repository.UserRepositories
{
   public interface IUserRepository : IRepository<User,int>
   {
      IEnumerable<User> GetFilteredStaff(Model.DataFiltration.StaffFilter staffFilter);
        IEnumerable<User> GetFilteredDoctorsForReport(Model.DataReport.DoctorOccupationReport report);

        IEnumerable<User> GetFilteredPatient(Model.DataFiltration.PatientFitler patientFilter, Doctor doctor);
      
      IEnumerable<User> GetPatientForDoctor(Model.Users.Doctor doctor);

      IEnumerable<User> GetAllStaff();
        IEnumerable<User> GetAllSecretary();

        User GetFromJMBG(string jmbg);

        IEnumerable<User> GetDoctorFromWorkPlace(WorkPlace workPlace);
        IEnumerable<User> GetDoctorFromWorkRoom(Room workRoom);

        User GetDirector();
    }
}