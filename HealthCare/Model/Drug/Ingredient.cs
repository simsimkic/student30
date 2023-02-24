/***********************************************************************
 * Module:  Ingredient.cs
 * Purpose: Definition of the Class Ingredient
 ***********************************************************************/

using System;

namespace Model.Drug
{
   public class Ingredient
   {
      private int id;
      private string name;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
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