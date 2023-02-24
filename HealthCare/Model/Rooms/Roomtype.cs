/***********************************************************************
 * Module:  FormatDrug.cs
 * Purpose: Definition of the Class FormatDrug
 ***********************************************************************/

using HCIProjekat.View;
using System;
using System.ComponentModel;

namespace Model.Rooms
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Roomtype
   {
        [Description("Soba za lezanje")]
        Ward,
        [Description("Operaciona sala")]
        Opperationroom,
        [Description("Soba za preglede")]
        Examinationroom
    }
}