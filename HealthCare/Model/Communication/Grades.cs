// File:    Grades.cs
// Created: Thursday, April 16, 2020 20:46:49
// Purpose: Definition of Enum Grades

using HCIProjekat.View;
using System;
using System.ComponentModel;

namespace Model.Communication
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]

    public enum Grades {
        [Description("1")]
        I,
        [Description("2")]
        II,
        [Description("3")]
        III,
        [Description("4")]
        IV,
        [Description("5")]
        V }


}