/***********************************************************************
 * Module:  City.cs
 * Purpose: Definition of the Class City
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class City
   {
      private string name;
      private int postNumber;
      
      public Country country;

        public Country Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
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

        public int PostNumber
        {
            get { return postNumber; }
            set
            {
                if (postNumber != value)
                {
                    postNumber = value;
                }
            }
        }
    }
}