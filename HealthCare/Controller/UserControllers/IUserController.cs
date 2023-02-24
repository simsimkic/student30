// File:    IUserController.cs
// Created: Saturday, May 23, 2020 0:25:12
// Purpose: Definition of Interface IUserController

using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Controller.UserControllers
{
   public interface IUserController : IGet<User,int>, IDelete<User>, ICreate<User>, IUpdate<User>, IGetAll<User>
   {
      Model.Users.User LoginUser(string username, string password);
      
      IEnumerable<User> GetFilteredStaff(Model.DataFiltration.StaffFilter staffFilter);

      IEnumerable<User> GetAllStaff();

      IEnumerable<User> GetFilteredPatient(Model.DataFiltration.PatientFitler patientFilter, Doctor doctor);
      
      IEnumerable<User> GetPatientForDoctor(Model.Users.Doctor doctor);

      IEnumerable<User> GetDoctorFromWorkPlace(WorkPlace workPlace);
      IEnumerable<User> GetDoctorFromWorkRoom(Room workRoom);

        User LoginStaff(string username, string password);    }
}