/***********************************************************************
 * Module:  AppointmentService.cs
 * Purpose: Definition of the Class Model.Appointment.AppointmentService
 ***********************************************************************/

using Model.Appointment;
using Model.Rooms;
using Model.Users;
using Repository.AppointmentRepository;
using System;
using System.Collections.Generic;
using Service.UserService;
using System.Linq;
using HealthCare.Util;
using HealthCare.Service.AppointmentService;
using HealthCare.Model.Appointment;
using HealthCare;
using Model.MedicalRecords;
using System.Windows.Controls;
using MaterialDesignColors.Recommended;
using System.Windows;
using Service.CommunicationServices;
using Model.Communication;
using Service.RoomServices;

namespace Service.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private Repository.AppointmentRepository.IAppointmentRepository _repository;
        public IWorkTimeService WorkTimeService { get; set; }
        public UserService.IUserService UserService { get; set; }

        public INotificationService _notificationService;

        public IRenovateService _renovateService;

        private IStrategy strategy;

        public AppointmentService(IAppointmentRepository repository, IWorkTimeService WorkTimeService, UserService.IUserService UserService, INotificationService notificationService, IRenovateService renovateService)
        {
            _repository = repository;
            _notificationService = notificationService;
            _renovateService = renovateService;
            this.WorkTimeService = WorkTimeService;
            this.UserService = UserService;
        }

        public Appointment Create(Appointment entity) => _repository.Create(entity);

        public Appointment Delete(Appointment entity)
        { 
            _notificationService.AppointmentCancelled(entity);
            return _repository.Delete(entity);
        }

        public IEnumerable<Appointment> GetAll() => _repository.GetAll();


        public IEnumerable<Appointment> GetAppointmentForDate(DateTime requestDate)
                                        => _repository.GetAppointmentForDate(requestDate);

        public IEnumerable<Appointment> GetAppointmentForDoctor(User doctor)
                                        => _repository.GetAppointmentForDoctor(doctor);

        public IEnumerable<Appointment> GetAppointmentForDoctorForDate(User doctor, DateTime time)
                                        => _repository.GetAppointmentForDoctorForDate(doctor, time);

        public IEnumerable<Appointment> GetAppointmentForPatient(User patient)
                                        => _repository.GetAppointmentForPatient(patient);


        public IEnumerable<Appointment> GetAppointmentForRoom(Room room)
                                        => _repository.GetAppointmentForRoom(room);

        public Appointment GetFromID(int id)
                                        => _repository.GetFromID(id);

        public bool HasPatientAppointment(User patient)
                                        => _repository.HasPatientAppointment(patient);

        public Appointment Update(Appointment entity) 
                                        => _repository.Update(entity);

        public IEnumerable<Appointment> GetFreeAppointments(User patient, User doctor, DateTime date, TreatmentType treatmentType)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            Doctor doctorObject = (Doctor)UserService.GetFromID(doctor.Id);
            if(_renovateService.IsRenovating(new Room() { RoomNumber = doctorObject.WorkRoom.RoomNumber }, date))
            {
                return freeAppointments;
            }
            List<Appointment> appointments = GetAppointmentForDoctorForDate(doctor, date).ToList();
            EmployeeWorkDay employeeWorkDay = WorkTimeService.GetEmployeeWorkDay(doctor, date);
            if (employeeWorkDay == null)
            {
                return freeAppointments;
            }
            TimeSpan appointmentDuration = GetAppointmentDuration(treatmentType);
            freeAppointments = FindFreeAppointments(appointments, employeeWorkDay, appointmentDuration).ToList();
            freeAppointments = InitializeAppointments(freeAppointments, patient, doctor, treatmentType).ToList();
            return freeAppointments;   
        }

        private IEnumerable<Appointment> FindFreeAppointments(List<Appointment> appointments, EmployeeWorkDay employeeWorkDay, TimeSpan appointmentDuration)
        {
            appointments.Sort((x, y) => DateTime.Compare(x.StartDateTime, y.StartDateTime));
            List<Appointment> freeAppointments = new List<Appointment>();
            
            DateTime startTime = employeeWorkDay.DateFrom;
            DateTime endTime;
            if (appointments.Any())
            {
                foreach (Appointment appointment in appointments)
                {
                    endTime = appointment.StartDateTime;
                    freeAppointments.AddRange(FillFreeInterval(startTime, endTime, appointmentDuration));
                    startTime = appointment.EndDateTime;
                }
            }
            endTime = employeeWorkDay.DateTo;
            freeAppointments.AddRange(FillFreeInterval(startTime, endTime, appointmentDuration));
            return freeAppointments;
        }

        public IEnumerable<Appointment> FillFreeInterval(DateTime startTime, DateTime endTime, TimeSpan appointmentDuration)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            while (endTime - startTime >= appointmentDuration)
            {
                Appointment freeAppointment = new Appointment() { StartDateTime = startTime, EndDateTime = startTime.Add(appointmentDuration) };
                startTime = freeAppointment.EndDateTime;
                freeAppointments.Add(freeAppointment);
            }
            return freeAppointments;
        }

        private TimeSpan GetAppointmentDuration(TreatmentType treatmentType)
            => (treatmentType == TreatmentType.Surgery) ? new TimeSpan(0,App.DefaultAppointmentSurgeryDuration,0) : new TimeSpan(0,App.DefaultAppointmentExaminationDuration,0);

        private IEnumerable<Appointment> InitializeAppointments(IEnumerable<Appointment> appointments, User patient, User doctor, TreatmentType treatmentType)
        {
            foreach(Appointment appointment in appointments)
            {
                appointment.Doctor = new Doctor() { Id = doctor.Id };
                Doctor doctorObject = (Doctor)UserService.GetFromID(doctor.Id);
                appointment.Room = new Room() { RoomNumber = doctorObject.WorkRoom.RoomNumber };
                appointment.Patient = new Patient() { Id = patient.Id };
                appointment.Type = treatmentType;
            }
            return appointments;
        }

        public Appointment RecommendAppointment(RecommendAppointmentParameters parameters)
        {
            strategy = ChooseSrategy(parameters);
            Appointment appointment = GetExaminationInRange(parameters.DateFrom, parameters.DateTo, parameters.Doctor);
            if(appointment != null)
            {
                return appointment;
            }
            else
            {
                return strategy.recommend();
            }
        }

        public Appointment GetExaminationInRange(DateTime dateFrom, DateTime dateTo, User doctor)
        {
            List<Appointment> freeAppointments;
            var app = Application.Current as App;
            Patient patient = (Patient)app._currentUser;
            TreatmentType treatmentType = TreatmentType.Examination;
            for (DateTime date = dateFrom; date <= dateTo; date = date.AddDays(1))
            {
                freeAppointments = GetFreeAppointments(patient, doctor, date, treatmentType).ToList();
                if (freeAppointments.Count > 0)
                {
                    return freeAppointments[0];
                }
            }
            return null;
        }

        public IEnumerable<Appointment> GetFreeAppointmentsForSurgery(Model.Users.User patient, Model.Users.User doctor, DateTime date, TimeSpan appointmentDuration)
        {
            List<Appointment> freeAppointments = new List<Appointment>();
            Doctor doctorObject = (Doctor)UserService.GetFromID(doctor.Id);
            if (_renovateService.IsRenovating(new Room() { RoomNumber = doctorObject.WorkRoom.RoomNumber }, date))
            {
                return freeAppointments;
            }
            List<Appointment> appointments = GetAppointmentForDoctorForDate(doctor, date).ToList();
            EmployeeWorkDay employeeWorkDay = WorkTimeService.GetEmployeeWorkDay(doctor, date);
            if (employeeWorkDay == null)
            {
                return freeAppointments;
            }
            freeAppointments = FindFreeAppointments(appointments, employeeWorkDay, appointmentDuration).ToList();
            TreatmentType treatmentType = TreatmentType.Surgery;
            freeAppointments = InitializeAppointments(freeAppointments, patient, doctor, treatmentType).ToList();
            return freeAppointments;
        }


        IStrategy ChooseSrategy(RecommendAppointmentParameters parameters)
            => (parameters.Priority == AppointmentPriority.DatePriority) ? (IStrategy)new DatePriorityStrategy(this, parameters) : new DoctorPriorityStrategy(this, parameters);

    }
}