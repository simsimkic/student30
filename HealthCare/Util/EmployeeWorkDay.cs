using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Util
{
    public class EmployeeWorkDay
    {
        private DateTime dateFrom;
        private DateTime dateTo;

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                if (dateFrom != value)
                {
                    dateFrom = value;
                }
            }
        }
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                if (dateTo != value)
                {
                    dateTo = value;
                }
            }
        }
    }
}
