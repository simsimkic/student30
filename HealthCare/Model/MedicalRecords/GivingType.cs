using HCIProjekat.View;
using System;
using System.ComponentModel;

namespace Model.MedicalRecords
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]

    public enum GivingType
    {
        [Description("Nacin 1")]
        Required,
        [Description("Nacin 2")]
        Forced,
        [Description("Nacin 3")]
        Forced1
    }
}
