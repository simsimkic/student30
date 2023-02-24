/***********************************************************************
 * Module:  UserService.cs
 * Purpose: Definition of the Class Service.UserService
 ***********************************************************************/

using Model.DataFiltration;
using Model.DataReport;
using Model.Rooms;
using Model.Users;
using Repository.AppointmentRepository;
using Repository.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        public Repository.UserRepositories.IUserRepository _repository;
        public IWorkTimeRepository _workTimeRepository;
        public IAbsenceRepository _absenceRepository;
        public IAppointmentRepository _appointmentRepository;

        public UserService(IUserRepository repository, IWorkTimeRepository workTimeRepository, IAbsenceRepository absenceRepository, IAppointmentRepository appointmentRepository)
        {
            _repository = repository;
            _absenceRepository = absenceRepository;
            _workTimeRepository = workTimeRepository;
            _appointmentRepository = appointmentRepository;
        }
        public User Create(User entity) => (IsJmbgUnique(entity)) ? _repository.Create(entity) : null;

        private bool IsJmbgUnique(User entity)
            => GetAll().SingleOrDefault(x => x.JMBG == entity.JMBG) == null;

        public User Delete(User entity)
        {
            if (IsUserStaff(entity))
            {
                if (_appointmentRepository.DoesDoctorHaveFutureAppointment(entity))
                    return null;
                _workTimeRepository.DeleteWorkTimeForStaff(entity);
                _absenceRepository.DeleteAbsenceForStaff(entity);
            }
            return _repository.Delete(entity);
        }

        private bool IsUserStaff(User entity) => entity.GetType().Equals(typeof(Doctor)) || entity.GetType().Equals(typeof(Model.Users.Secretary));

        public IEnumerable<User> GetAll() => _repository.GetAll();

        public IEnumerable<User> GetAllStaff() => _repository.GetAllStaff();

        public IEnumerable<User> GetFilteredDoctorsForReport(DoctorOccupationReport report) => _repository.GetFilteredDoctorsForReport(report);

        public IEnumerable<User> GetFilteredPatient(PatientFitler patientFilter, Doctor doctor) => _repository.GetFilteredPatient(patientFilter, doctor);
        public IEnumerable<User> GetFilteredStaff(StaffFilter staffFilter) => _repository.GetFilteredStaff(staffFilter);

        public IEnumerable<User> GetPatientForDoctor(Doctor doctor) => _repository.GetPatientForDoctor(doctor);

        public User GetFromID(int id) => _repository.GetFromID(id);

        public User LoginUser(string username, string password)
        {
            if (IsValidUserInput(username, password))
            {
                User patient = _repository.GetFromJMBG(username);

                if (patient!=null && patient.Password == password)
                    return patient;
            }
            return null;
        }

        private bool IsValidUserInput(string username, string password) 
                                => !(string.IsNullOrEmpty(username) || username.Length != 13);

        private bool IsValidStaffInput(string username, string password) => (!string.IsNullOrEmpty(username) && Int32.TryParse(username, out int id));


        public User LoginStaff(string username, string password) 
        {
            if (IsValidStaffInput(username, password))
            {
                Int32.TryParse(username, out int id);
                User staff = _repository.GetFromID(id);
                if (staff != null && staff.Password == password)
                    return staff;
            }
            return null;
        }

        public User Update(User entity) => _repository.Update(entity);

        public IEnumerable<User> GetDoctorFromWorkPlace(WorkPlace workPlace) => _repository.GetDoctorFromWorkPlace(workPlace);

        public IEnumerable<User> GetDoctorFromWorkRoom(Room workRoom)
            => _repository.GetDoctorFromWorkRoom(workRoom);
    }
}