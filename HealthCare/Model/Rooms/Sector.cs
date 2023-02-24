/***********************************************************************
 * Module:  Sector.cs
 * Purpose: Definition of the Class Sector
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model.Rooms
{
    public class Sector
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }
    }
}