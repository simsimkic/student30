/***********************************************************************
 * Module:  AppointmentService.cs
 * Purpose: Definition of the Class Model.Appointment.AppointmentService
 ***********************************************************************/

using HealthCare.Model.Appointment;
using Model.Appointment;
using Model.Rooms;
using Model.Users;
using Service.AppointmentService;
using System;
using System.Collections.Generic;

namespace Controller.AppointmenController
{
    public class AppointmentController : IAppointmentController
    {
        public Service.AppointmentService.AppointmentService _service;

        public AppointmentController(AppointmentService service)
        {
            _service = service;
        }

        public Appointment Create(Appointment entity) => _service.Create(entity);


        public Appointment Delete(Appointment entity) => _service.Delete(entity);

        public IEnumerable<Appointment> GetAll() => _service.GetAll();

        public IEnumerable<Appointment> GetAppointmentForDate(DateTime requestDate)
                                            => _service.GetAppointmentForDate(requestDate);


        public IEnumerable<Appointment> GetAppointmentForDoctor(User doctor)
                                            => _service.GetAppointmentForDoctor(doctor);

        public IEnumerable<Appointment> GetAppointmentForDoctorForDate(User doctor, DateTime time)
                                            =>_service.GetAppointmentForDoctorForDate(doctor, time);


        public IEnumerable<Appointment> GetAppointmentForPatient(User patient)
                                            => _service.GetAppointmentForPatient(patient);


        public IEnumerable<Appointment> GetAppointmentForRoom(Room room)
                                            => _service.GetAppointmentForRoom(room);


        public Appointment GetFromID(int id) => _service.GetFromID(id);


        public bool HasPatientAppointment(User patient)
                                => _service.HasPatientAppointment(patient);

        public Appointment Update(Appointment entity)
                                => _service.Update(entity);

        public IEnumerable<Appointment> GetFreeAppointments(Model.Users.User patient, Model.Users.User doctor, DateTime date, Model.MedicalRecords.TreatmentType treatmentType)
                                => _service.GetFreeAppointments(patient, doctor, date, treatmentType);

        public Appointment RecommendAppointment(RecommendAppointmentParameters parameters)
            => _service.RecommendAppointment(parameters);

        public IEnumerable<Appointment> GetFreeAppointmentsForSurgery(Model.Users.User patient, Model.Users.User doctor, DateTime date, TimeSpan timeSpan)
            => _service.GetFreeAppointmentsForSurgery(patient, doctor, date, timeSpan);


    }
}