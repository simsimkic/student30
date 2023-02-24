using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model.Appointment
{
    public class RecommendAppointmentParameters
    {
        public User Doctor { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public AppointmentPriority Priority { get; set; }

        public RecommendAppointmentParameters(User doctor, DateTime dateFrom, DateTime dateTo, AppointmentPriority priority)
        {
            Doctor = doctor;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Priority = priority;
        }

    }
}
