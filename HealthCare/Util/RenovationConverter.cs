using Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Util
{
    class RenovationConverter
    {
        public static IList<DateTime> ConvertRenovateToDateTimeList(Renovate renovate)
        {
            List<DateTime> retVal = new List<DateTime>();
            DateTime start = renovate.DateStart.Date;
            while (start <= renovate.DateEnd.Date)
            {
                retVal.Add(start.Date);
                start = start.Date.AddDays(1);
            }

            return retVal;
        }
    }
}
