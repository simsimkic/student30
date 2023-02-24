using Model.Appointment;
using HealthCare.Service.AppointmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.AppointmentService
{
    interface IStrategy
    {
        Appointment recommend();
    }
}
