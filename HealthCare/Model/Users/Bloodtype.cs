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

    public enum Bloodtype
   {
        [Description("A")]
        A,
        [Description("B")]
        B,
        [Description("Ab")]
        Ab,
        [Description("0")]
        Zero
    }
}