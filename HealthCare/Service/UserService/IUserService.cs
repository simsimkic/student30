
// File:    IUserService.cs
// Created: Friday, May 22, 2020 23:36:08
// Purpose: Definition of Interface IUserService

using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Service.UserService
{
   public interface IUserService : IDelete<User>, IUpdate<User>, ICreate<User>, IGet<User,int>, IGetAll<User>
    {
      User LoginUser(string username, string password);
        User LoginStaff(string username, string password);


        IEnumerable<User> GetFilteredStaff(Model.DataFiltration.StaffFilter staffFilter);

        IEnumerable<User> GetFilteredDoctorsForReport(Model.DataReport.DoctorOccupationReport report);

        IEnumerable<User> GetAllStaff();

      IEnumerable<User> GetFilteredPatient(Model.DataFiltration.PatientFitler patientFilter, Doctor doctor);
      
      IEnumerable<User> GetPatientForDoctor(Model.Users.Doctor doctor);

        IEnumerable<User> GetDoctorFromWorkPlace(WorkPlace workPlace);
        IEnumerable<User> GetDoctorFromWorkRoom(Room workRoom);
    }
}