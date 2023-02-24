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
    public enum Days
   {
        [Description("Nedelja")]
        Sunday,
        [Description("Ponedeljak")]
        Monday,
        [Description("Utorak")]
        Tuesday,
        [Description("Sreda")]
        Wednesday,
        [Description("Cetvrtak")]
        Thursday,
        [Description("Petak")]
        Friday,
        [Description("Subota")]
        Saturday
    }
}