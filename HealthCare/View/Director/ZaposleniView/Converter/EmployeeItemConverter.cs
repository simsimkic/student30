using HCIProjekat.View.ZaposleniView.DataView;
using HealthCare.View.Director;
using Model.Users;
using System;
using System.Collections.Generic;
using System.IO;

namespace Director.ZaposleniView.Converter
{
    class EmployeeItemConverter : AbstractConverter
    {
        public static EmployeeItem ConvertEmployeeToEmployeeView(User staff)
            => new EmployeeItem
            {
                Id = staff.Id,
                EmployeeName = staff.Name,
                Surname = staff.Surname,
                WorkPlace = (staff.GetType().Equals(typeof(Model.Users.Doctor))) ? "Doktor" : "Sekretar",
                Picture = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ((Staff)staff).Picture)
            };

        public static IList<EmployeeItem> ConvertEmployeeListToEmployeeViewList(IList<User> staffs)
            => ConvertEntityListToViewList(staffs, ConvertEmployeeToEmployeeView);
    }
}
