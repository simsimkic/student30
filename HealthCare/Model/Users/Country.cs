/***********************************************************************
 * Module:  Country.cs
 * Purpose: Definition of the Class Country
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Country
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