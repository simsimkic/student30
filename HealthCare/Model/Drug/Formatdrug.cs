/***********************************************************************
 * Module:  Formatdrug.cs
 * Purpose: Definition of the Class Formatdrug
 ***********************************************************************/

using HCIProjekat.View;
using System;
using System.ComponentModel;

namespace Model.Drug
{

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Formatdrug
    {
        [Description("Kapsule")]
        Capsule,
        [Description("Vakcina")]
        Vaccine,
        [Description("Inekcija")]
        Injection
    }
}