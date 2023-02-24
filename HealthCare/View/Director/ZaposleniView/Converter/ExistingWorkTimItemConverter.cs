using HCIProjekat.View.ZaposleniView.DataView;
using Model.Users;
using System.Collections.Generic;

namespace HealthCare.View.Director.ZaposleniView.Converter
{
    class ExistingWorkTimItemConverter : AbstractConverter
    {
        public static ExistingWorkTimeItem ConvertWorkTImeToWorkTimeView(WorkTime workTime)
        {
            ExistingWorkTimeItem retVal = new ExistingWorkTimeItem()
            {
                dateFrom = workTime.StartDate.Date,
                dateTo = workTime.EndDate.Date,
                Id = workTime.Id,
                Monday = "/",
                Tuesday = "/",
                Wednesday = "/",
                Thursday = "/",
                Friday = "/",
                Saturday = "/",
                Sunday = "/"
            };

            foreach (var v in workTime.workDay)
            {
                if ((int)v.Day == (int)Days.Monday)
                    retVal.Monday = v.StartTime + "h - " + v.EndTime + "h";
                else if ((int)v.Day == (int)Days.Tuesday)
                    retVal.Tuesday = v.StartTime + "h - " + v.EndTime + "h";
                else if ((int)v.Day == (int)Days.Wednesday)
                    retVal.Wednesday = v.StartTime + "h - " + v.EndTime + "h";
                else if ((int)v.Day == (int)Days.Thursday)
                    retVal.Thursday = v.StartTime + "h - " + v.EndTime + "h";
                else if ((int)v.Day == (int)Days.Friday)
                    retVal.Friday = v.StartTime + "h - " + v.EndTime + "h";
                else if ((int)v.Day == (int)Days.Saturday)
                    retVal.Saturday = v.StartTime + "h - " + v.EndTime + "h";
                else
                    retVal.Sunday = v.StartTime + "h - " + v.EndTime + "h";

            }
            return retVal;
        }

        public static IList<ExistingWorkTimeItem> ConvertWorkTimeListToWorkTimeViewList(IList<WorkTime> workTimes)
            => ConvertEntityListToViewList(workTimes, ConvertWorkTImeToWorkTimeView);

    }
}
