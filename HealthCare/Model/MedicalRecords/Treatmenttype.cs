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

    public enum TreatmentType
   {
        [Description("Obican pregled")]
        Examination,
        [Description("Specijalisticki pregled")]
        Specialistexamination,
        [Description("Operacija")]
        Surgery
   }
}