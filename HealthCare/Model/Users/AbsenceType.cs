using HCIProjekat.View;
using System.ComponentModel;

namespace Model.Users
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum AbsenceType
    {
        [Description("Konferencija")]
        Conference,
        [Description("Poslovne obaveze")]
        WorkObligations,
        [Description("Privatne obaveze")]
        PrivateObligations,
        [Description("Odmor")]
        Vacation,
        [Description("Ostalo")]
        Other
    }
}
