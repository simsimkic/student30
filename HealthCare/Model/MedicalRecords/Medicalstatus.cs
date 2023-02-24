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

    public enum MedicalStatus
   {
       [Description("Aktivan")]
        Active,
       [Description("Neaktivan")]
        Inactive,
   }
}