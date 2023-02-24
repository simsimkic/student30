/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using Model.Appointment;
using Model.Rooms;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public JSONStream<Appointment> _stream;
        public IntSequencer _sequencer;

        public AppointmentRepository(JSONStream<Appointment> stream)
        {
            _stream = stream;
            _sequencer = new
                IntSequencer();
            InitializeId();
        }

        public Appointment Create(Appointment entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Appointment> appointmentList = _stream.GetAll().ToList();
            appointmentList.Add(entity);
            _stream.SaveAll(appointmentList);
            return entity;
        }

        public Appointment Delete(Appointment entity)
        {
            List<Appointment> appointmentList = _stream.GetAll().ToList();
            var entityToRemove = appointmentList.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                appointmentList.Remove(entityToRemove);
                _stream.SaveAll(appointmentList);
            }

            return entityToRemove;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Appointment> entities)
          => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);


        public IEnumerable<Appointment> GetAll() => _stream.GetAll();

        public IEnumerable<Appointment> GetAppointmentForDate(DateTime requestDate)
                                       => this.GetAll().Where(x => x.StartDateTime.Date.Equals(requestDate.Date));


        public IEnumerable<Appointment> GetAppointmentForDoctor(User doctor)
                                        => this.GetAll().Where(x => x.Doctor.Id.Equals(doctor.Id));

        public IEnumerable<Appointment> GetAppointmentForDoctorForDate(User doctor, DateTime requestDate)
                                       => this.GetAll().Where(x => x.Doctor.Id.Equals(doctor.Id) && x.StartDateTime.Date.Equals(requestDate.Date));

        public IEnumerable<Appointment> GetAppointmentForPatient(User patient)
                                => this.GetAll().Where(x => x.Patient.Id.Equals(patient.Id));


        public IEnumerable<Appointment> GetAppointmentForRoom(Room room)
                          => this.GetAll().Where(x => x.Room.RoomNumber.Equals(room.RoomNumber));


        public Appointment GetFromID(int id)
                          => this.GetAll().SingleOrDefault(x => x.Id == id);


        public bool DoesDoctorHaveFutureAppointment(User doctor)
                          => GetAppointmentForDoctor(doctor).Where(x => x.StartDateTime.Date >= DateTime.Now.Date).Count() > 0;

        public bool HasPatientAppointment(User patient)
                           => GetAll().Where(x => x.Patient.Id.Equals(patient.Id) && DateTime.Compare(x.StartDateTime, DateTime.Now) < 0 && DateTime.Compare(x.EndDateTime, DateTime.Now) > 0).Count() > 0;


        public Appointment Update(Appointment entity)
        {
            List<Appointment> appointmentList = _stream.GetAll().ToList();
            appointmentList[appointmentList.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(appointmentList);
            return entity;
        }

        public bool DoesRoomHasFutureAppointment(Room room)
                    => GetAppointmentForRoom(room).Where(x => x.StartDateTime.Date >= DateTime.Now.Date).Count() > 0;

        public IEnumerable<Appointment> GetAppointmentForRoomForDate(Room room, DateTime date)
                    => GetAppointmentForRoom(room).Where(x => x.StartDateTime.Date == date.Date);
    }   
}