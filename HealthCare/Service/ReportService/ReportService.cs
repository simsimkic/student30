/***********************************************************************
 * Module:  ReportService.cs
 * Purpose: Definition of the Class Service.ReportService
 ***********************************************************************/

using HealthCare;
using HealthCare.Util;
using Model.Appointment;
using Model.DataReport;
using Model.MedicalRecords;
using Model.Users;
using Service.AppointmentService;
using Service.MedicalRecordServices;
using Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ReportService
{
    public class ReportService : IReportService
    {
        private IUserService _userService;
        private IWorkTimeService _workTimeService;
        private IAppointmentService _appointmentService;
        private IRecordService _recordService;
        public ReportService(IUserService userService, IWorkTimeService workTimeService, IAppointmentService appointmentService, IRecordService recordService) 
        {
            _userService = userService;
            _workTimeService = workTimeService;
            _appointmentService = appointmentService;
            _recordService = recordService;
        }
        public DoctorOccupationReport GetReportForDoctorOccupation(DoctorOccupationReport report)
        {
            report.doctorOccupation = new List<DoctorOccupation>();
            foreach(User user in _userService.GetFilteredDoctorsForReport(report))
                report.doctorOccupation.Add(CreateDoctorOcupation(user, report));

            return report;
        }

        private DoctorOccupation CreateDoctorOcupation(User user, DoctorOccupationReport report)
        {
            DoctorOccupation doctorOccupation = new DoctorOccupation() { doctor = (Doctor)user};
            DateTime start = report.DateFrom.Date;
            while (start <= report.DateTo.Date)
            {
                doctorOccupation.TotalWorkTime += GetEmployeeWorkTime(user, start);
                _appointmentService.GetAppointmentForDoctorForDate(user, start).ToList().ForEach(x =>
                                                                    { doctorOccupation.OccupationWorkTime += x.EndDateTime.Hour - x.StartDateTime.Hour; });

                start = start.Date.AddDays(1);
            }

            return doctorOccupation;
        }

        private int GetEmployeeWorkTime(User user, DateTime date)
        {
            EmployeeWorkDay workDay = _workTimeService.GetEmployeeWorkDay(user, date);
            return (workDay != null) ? workDay.DateTo.Hour - workDay.DateFrom.Hour : 0;
        }

        public PatientTreatmentReport GetReportForPatientTreatment(Patient patient, DateTime dateFrom, DateTime dateTo)
                => new PatientTreatmentReport
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    patient = patient,
                    treatment = GenerateListOfTreatment(patient,dateFrom, dateTo)
                };

        private IList<Treatment> GenerateListOfTreatment(Patient patient,DateTime dateFrom, DateTime dateTo)
        {
            IList<Treatment> listOfTreatment = new List<Treatment>();
           
            foreach (Treatment item in _recordService.GetRecordForPatient(patient.Id).Treatment)
            {
                if (DateTime.Compare(item.Date.Date, dateTo.Date) <= 0 && DateTime.Compare(item.Date.Date, dateFrom.Date) >= 0)
                {
                    listOfTreatment.Add(item);
                }
            }
            return listOfTreatment;
        }

    }
}