using Model.Appointment;
using HealthCare.Service.AppointmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResolveView;
using HealthCare.Model.Appointment;
using Model.Users;
using Model.DataFiltration;
using Model.MedicalRecords;
using HealthCare;
using System.Windows;

namespace Service.AppointmentService
{
    class DoctorPriorityStrategy : IStrategy
    {
        private RecommendAppointmentParameters _parameters;
        private AppointmentService _appointmentService;

        public DoctorPriorityStrategy(AppointmentService appointmentService, RecommendAppointmentParameters parameters)
        {
            _appointmentService = appointmentService;
            _parameters = parameters;
        }

        public Appointment recommend()
        {
            List<Appointment> freeAppointments;
            DateTime date = _parameters.DateTo.Date;
            DateTime dateUpperLimit = date.AddDays(App.MaxTimePeriodToMakeAppointment);
            while(date < dateUpperLimit)
            {
                var app = Application.Current as App;
                Patient patient = (Patient)app._currentUser;
                TreatmentType treatmentType = TreatmentType.Examination;
                freeAppointments = _appointmentService.GetFreeAppointments(patient, _parameters.Doctor, date, treatmentType).ToList();
                if (freeAppointments.Count > 0)
                {
                    return freeAppointments[0];
                }
                date = date.AddDays(1);
            }
            return null;
        }
    }
}
