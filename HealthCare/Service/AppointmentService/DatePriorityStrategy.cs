using HealthCare.Service.AppointmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.AppointmentService;
using Model.Appointment;
using HealthCare.Model.Appointment;
using Model.DataFiltration;
using Model.Users;
using Model.MedicalRecords;
using HealthCare;
using System.Windows;

namespace Service.AppointmentService
{
    class DatePriorityStrategy : IStrategy
    {
        private RecommendAppointmentParameters _parameters;
        private AppointmentService _appointmentService;

        public DatePriorityStrategy(AppointmentService appointmentService, RecommendAppointmentParameters parameters)
        {
            _appointmentService = appointmentService;
            _parameters = parameters;
        }

        public Appointment recommend()
        {
            Appointment freeAppointment = null;
            WorkPlace workPlace = new WorkPlace() { Name = "Lekar opste prakse" };
            List<User> doctors = _appointmentService.UserService.GetDoctorFromWorkPlace(workPlace).ToList();
            foreach(User doctor in doctors)
            {
                freeAppointment = _appointmentService.GetExaminationInRange(_parameters.DateFrom, _parameters.DateTo, doctor);
                if(freeAppointment != null)
                {
                    return freeAppointment;
                }
            }
            return freeAppointment;
        }
    }
}
