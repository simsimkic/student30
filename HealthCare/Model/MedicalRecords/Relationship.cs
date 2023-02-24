/***********************************************************************
 * Module:  Treatmenttype.cs
 * Purpose: Definition of the Class Treatmenttype
 ***********************************************************************/

using HCIProjekat.View;
using System;
using System.ComponentModel;

namespace Model.MedicalRecords
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]

    public enum Relationship
   {
        [Description("Majka")]
        Mother,
        [Description("Otac")]
        Father,
        [Description("Brat")]
        Brother,
        [Description("Sestra")]
        Sister,
        [Description("Baba")]
        Grandmother,
        [Description("Deda")]
        Grandfather
   }
}