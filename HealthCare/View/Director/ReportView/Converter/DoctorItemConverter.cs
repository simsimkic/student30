using HCIProjekat.View.ReportView.DataView;
using HealthCare.View.Director;
using Model.DataReport;
using System;
using System.Collections.Generic;
using System.IO;

namespace Director.ReportView.Converter
{
    class DoctorItemConverter : AbstractConverter
    {
        public static FilteredDoctorItem ConvertDoctorToDoctorView(DoctorOccupation doctorOccupation)
           => new FilteredDoctorItem
           {
               Id = doctorOccupation.doctor.Id,
               DoctorName = doctorOccupation.doctor.Name,
               Surname = doctorOccupation.doctor.Surname,
               Picture = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, doctorOccupation.doctor.Picture),
               WorkPlace = doctorOccupation.doctor.WorkPlace.Name,
               WorkTime = doctorOccupation.TotalWorkTime,
               AppointmentTime = doctorOccupation.OccupationWorkTime
           };

        public static IList<FilteredDoctorItem> ConvertDoctorListToDoctorViewList(IList<DoctorOccupation> doctorOccupations)
            => ConvertEntityListToViewList(doctorOccupations, ConvertDoctorToDoctorView);
    }
}
