using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Util
{
    public class AbsenceConverter
    {
        public static List<DateTime> ConvertAbsenceToDateTimeList(Absence absence)
        {
            List<DateTime> retVal = new List<DateTime>();
            DateTime start = absence.StartDate.Date;
            while (start <= absence.EndDate.Date)
            {
                retVal.Add(start.Date);
                start = start.Date.AddDays(1);
            }

            return retVal;
        }

        public static IList<DateTime> ConvertAbsenceListToDateTimeList(IList<Absence> absences)
        {
            List<DateTime> retVal = new List<DateTime>();
            foreach (var v in absences)
            {
                retVal.AddRange(ConvertAbsenceToDateTimeList(v));
            }

            return retVal;
        }

        public static List<EmployeeAbsence> ConvertAbsenceToEmployeeAbsenceList(Absence absence)
        {
            List<EmployeeAbsence> retVal = new List<EmployeeAbsence>();
            DateTime start = absence.StartDate.Date;
            while (start <= absence.EndDate.Date)
            {
                retVal.Add(new EmployeeAbsence() { Date = start.Date, Type = absence.AbsenceType });
                start = start.Date.AddDays(1);
            }

            return retVal;
        }

        public static IList<EmployeeAbsence> ConvertAbsenceListToEmployeeAbsenceList(IList<Absence> absences)
        {
            List<EmployeeAbsence> retVal = new List<EmployeeAbsence>();
            foreach (var v in absences)
            {
                retVal.AddRange(ConvertAbsenceToEmployeeAbsenceList(v));
            }

            return retVal;
        }
    }
}
