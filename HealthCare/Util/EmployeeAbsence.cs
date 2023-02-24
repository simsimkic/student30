using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Util
{
    public class EmployeeAbsence
    {
        private DateTime _date;
        private AbsenceType _type;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                }
            }
        }
        public AbsenceType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                }
            }
        }
    }
}
