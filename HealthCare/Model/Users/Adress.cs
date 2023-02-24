/***********************************************************************
 * Module:  Adress.cs
 * Purpose: Definition of the Class Adress
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Adress
   {
      private int number;
      private string street;
      
      private City city;

        public City City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                }
            }
        }

        public string Street
        {
            get { return street; }
            set
            {
                if (street != value)
                {
                    street = value;
                }
            }
        }
        public int Number
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                }
            }
        }

    }
}