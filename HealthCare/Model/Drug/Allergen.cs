/***********************************************************************
 * Module:  Allergens.cs
 * Purpose: Definition of the Class Allergens
 ***********************************************************************/

using System;

namespace Model.Drug
{
   public class Allergen
   {
        private int code;
        private string name;


        public int Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                }
            }
        }

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