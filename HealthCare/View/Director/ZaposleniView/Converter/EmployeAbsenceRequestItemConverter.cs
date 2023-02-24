using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare.View.Director;
using Model.Users;
using System.Collections.Generic;

namespace Director.ZaposleniView.Converter
{
    class EmployeAbsenceRequestItemConverter : AbstractConverter
    {
        public static EmployeeAbsenceRequestItem ConvertAbsenceToAbsenceView(Absence absence)
            => new EmployeeAbsenceRequestItem
            {
                Id = absence.staff.Id,
                IdAbsence = absence.Id,
                EmployeeName = absence.staff.Name,
                Surname = absence.staff.Surname,
                WorkPlace = absence.staff.GetType().Equals(typeof(Model.Users.Doctor)) ? "Doktor" : "Sekretar",
                AbsenceKind = absence.AbsenceType,
                Reason = absence.Reason,
                Picture = absence.staff.Picture,
                DateStartAbsence = absence.StartDate.Date,
                DateEndAbsence = absence.EndDate.Date
            };

        public static IList<EmployeeAbsenceRequestItem> ConvertAbsenceListToAbsenceViewList(IList<Absence> absences)
            => ConvertEntityListToViewList(absences, ConvertAbsenceToAbsenceView);
    }
}
