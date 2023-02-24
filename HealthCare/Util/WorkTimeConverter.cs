using Model.Users;
using System;
using System.Collections.Generic;

namespace HealthCare.Util
{
    public class WorkTimeConverter
    {
        public static List<EmployeeWorkDay> ConvertWorkTimeToEmployeeWorkDay(WorkTime workTime)
        {
            List<EmployeeWorkDay> retVal = new List<EmployeeWorkDay>();
            DateTime start = workTime.StartDate.Date;
            while (start <= workTime.EndDate.Date)
            {
                foreach (var v in workTime.workDay)
                {
                    if ((int)v.Day == (int)start.DayOfWeek)
                    {
                        retVal.Add(new EmployeeWorkDay() { DateFrom = start.Date.AddHours(v.StartTime), DateTo = start.Date.AddHours(v.EndTime) });
                    }
                }
                start = start.Date.AddDays(1);
            }

            return retVal;
        }

        public static IList<EmployeeWorkDay> ConvertWorkTimeListToEmployeeWorkDayList(IList<WorkTime> workTimes)
        {
            List<EmployeeWorkDay> retVal = new List<EmployeeWorkDay>();
            foreach (var v in workTimes)
            {
                retVal.AddRange(ConvertWorkTimeToEmployeeWorkDay(v));
            }

            return retVal;
        }
    }
}
