/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using HealthCare;
using Model.Appointment;
using Model.DataFiltration;
using Model.DataReport;
using Model.Rooms;
using Model.Users;
using Repository.AppointmentRepository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Repository.UserRepositories
{
   public class UserRepository : IUserRepository
   {
        public JSONStream<User> _stream;
        public IntSequencer _sequencer;
        private IAppointmentRepository _appointmentRepository;

        public UserRepository(JSONStream<User> stream,IAppointmentRepository appointmentRepository)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
            _appointmentRepository = appointmentRepository;
        }
        public User Create(User entity)
        {
            if (GetFromJMBG(entity.JMBG) != null)
                return entity;

            entity.Id = _sequencer.GenerateID();
            List<User> users = _stream.GetAll().ToList();
            if(entity.GetType().Equals(typeof(Doctor)) || entity.GetType().Equals(typeof(Model.Users.Secretary)))
                entity = setPicture(entity);
            users.Add(entity);
            _stream.SaveAll(users);
            return entity;
        }

        private User setPicture(User entity)
        {
            Staff staff = (Staff)entity;
            if (staff.Picture != "")
                staff.Picture = MoveStaffImageToProjectFolder(staff);

            return staff;
        }

        private string MoveStaffImageToProjectFolder(Staff staff)
        {

            string newPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(@"../../Resources/UserImages/", staff.Id + "-"+ ++staff.PictureNumber + @".png"));
            if (!File.Exists(newPath))
                File.Copy(staff.Picture, newPath, true);
            
            return Path.Combine(@"../../Resources/UserImages/", staff.Id + "-" + staff.PictureNumber + @".png");
        }

        public User Delete(User entity)
        {
            List<User> users = _stream.GetAll().ToList();
            var entityToRemove = users.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                users.Remove(entityToRemove);
                _stream.SaveAll(users);
            }

            return entityToRemove;
        }

        public IEnumerable<User> GetAll() => _stream.GetAll();

        public IEnumerable<User> GetFilteredPatient(PatientFitler patientFilter,Doctor doctor) => GetPatientForDoctor(doctor).Where(x => ((patientFilter.JMBG != "") ? x.JMBG == patientFilter.JMBG : true) &&
                                   ((patientFilter.RecordNumber != -1) ? ((Patient)x).MedicalRecord.Id == patientFilter.RecordNumber : true) &&
                                   ((patientFilter.Name != "") ? x.Name.Equals(patientFilter.Name) : true) &&
                                   ((patientFilter.Surname != "") ? x.Surname.Equals(patientFilter.Surname) : true));


        public IEnumerable<User> GetFilteredStaff(StaffFilter staffFilter)
            => GetAllStaff().Where(x => ((staffFilter.Name != "") ? x.Name == staffFilter.Name : true) &&
                                        ((staffFilter.Surname != "") ? x.Surname == staffFilter.Surname : true) &&
                                        ((staffFilter.Id != -1) ? x.Id == staffFilter.Id : true) &&
                                        ((staffFilter.StaffType != "") ? ((staffFilter.StaffType.Equals("Doktor")) ? x.GetType().Equals(typeof(Doctor)) : x.GetType().Equals(typeof(Model.Users.Secretary))) : true));

        public User GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);
        public User GetFromJMBG(string jmbg) => _stream.GetAll().SingleOrDefault(x => x.JMBG == jmbg);

        public IEnumerable<User> GetPatientForDoctor(Doctor doctor)
        {
            List<User> doctorsPatients = new List<User>();
            foreach (Appointment item in _appointmentRepository.GetAppointmentForDoctor(doctor).ToList())
            {
                if(!DoesExistInList(GetFromID(item.Patient.Id),doctorsPatients))
                    doctorsPatients.Add(GetFromID(item.Patient.Id));
            }

            return doctorsPatients.ToList();
        }

        private bool DoesExistInList(User patientForCheck, List<User> doctorsPatients)
        {
            foreach(User patient in doctorsPatients)
                if(patient.Id == patientForCheck.Id)
                    return true;

            return false;
        }

        public User Update(User entity)
        {
            List<User> users = _stream.GetAll().ToList();
            if (entity.GetType().Equals(typeof(Doctor)) && !(((Staff)users[users.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)]).Picture==((Staff)entity).Picture))
                entity = setPicture(entity);
            users[users.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(users);
            return entity;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));
        private int GetMaxId(IEnumerable<User> entities)
           => entities.Count() == 0 ? 1000 : entities.Max(entity => entity.Id);

        public IEnumerable<User> GetAllStaff() => GetAll().Where(x => x.GetType().Equals(typeof(Model.Users.Secretary)) || x.GetType().Equals(typeof(Doctor)));

        public User GetDirector() => _stream.GetAll().SingleOrDefault(x => x.GetType().Equals(typeof(Model.Users.Director)));

        public IEnumerable<User> GetFilteredDoctorsForReport(DoctorOccupationReport report) => GetAllStaff().Where(x => x.GetType().Equals(typeof(Doctor)) &&
                                                                  ((report.Id != -1) ? x.Id == report.Id : true) &&
                                                                  ((report.WorkPlace != null) ? ((Doctor)x).WorkPlace.Name.Equals(report.WorkPlace.Name) : true));

        public IEnumerable<User> GetDoctorFromWorkPlace(WorkPlace workPlace)
            => GetAllDoctor().Where(x => ((Doctor)x).WorkPlace.Name.Equals(workPlace.Name));

        private IEnumerable<User> GetAllDoctor() => GetAll().Where(x => x.GetType().Equals(typeof(Doctor)));

        public IEnumerable<User> GetDoctorFromWorkRoom(Room workRoom)
            => GetAllDoctor().Where(x => ((Doctor)x).WorkRoom.RoomNumber == workRoom.RoomNumber);

        public IEnumerable<User> GetAllSecretary()
                        => GetAll().Where(x => x.GetType().Equals(typeof(Model.Users.Secretary)));
    }
}