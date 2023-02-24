/***********************************************************************
 * Module:  Specialization.cs
 * Purpose: Definition of the Class Specialization
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model.Users
{
   public class Specialization
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