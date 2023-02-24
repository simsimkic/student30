/***********************************************************************
 * Module:  FormatDrug.cs
 * Purpose: Definition of the Class FormatDrug
 ***********************************************************************/

using HCIProjekat.View;
using System;
using System.ComponentModel;

namespace Model.Users
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Gender
   {
        [Description("Muski")]
        Male,
        [Description("Zenski")]
        Female
    }
}