using HCIProjekat.View;
using System.ComponentModel;

namespace Model.Drug
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum DrugStatus
    {
        [Description("Na cekanju")]
        Waiting,
        [Description("Odbijen")]
        Rejected,
        [Description("Odobren")]
        Approved
    }

}
