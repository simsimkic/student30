/***********************************************************************
 * Module:  UserService.cs
 * Purpose: Definition of the Class Service.UserService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using Model.Users;
using Service.UserService;
using System;
using System.Collections.Generic;

namespace Controller.UserControllers
{
   public class UserController : IUserController
   {
      public Service.UserService.IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        public User Create(User entity) => _service.Create(entity);
        public User Delete(User entity) => _service.Delete(entity);

        public IEnumerable<User> GetAll() => _service.GetAll();

        public IEnumerable<User> GetAllStaff() => _service.GetAllStaff();
        public IEnumerable<User> GetFilteredPatient(PatientFitler patientFilter, Doctor doctor) => _service.GetFilteredPatient(patientFilter,doctor);

        public IEnumerable<User> GetFilteredStaff(StaffFilter staffFilter) => _service.GetFilteredStaff(staffFilter);

        public User GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<User> GetPatientForDoctor(Doctor doctor) => _service.GetPatientForDoctor(doctor);

        public User LoginUser(string username, string password) => _service.LoginUser(username, password);

        public User Update(User entity) => _service.Update(entity);

        public IEnumerable<User> GetDoctorFromWorkPlace(WorkPlace workPlace) => _service.GetDoctorFromWorkPlace(workPlace);

        public IEnumerable<User> GetDoctorFromWorkRoom(Room workRoom) => _service.GetDoctorFromWorkRoom(workRoom);

        public User LoginStaff(string username, string password) => _service.LoginStaff(username, password);

    }
}