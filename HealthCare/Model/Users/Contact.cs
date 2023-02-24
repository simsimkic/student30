/***********************************************************************
 * Module:  Contact.cs
 * Purpose: Definition of the Class Contact
 ***********************************************************************/

using System;

namespace Model.Users
{
   public class Contact
   {
      private string number;
      private string email;

        public string Number
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
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                }
            }
        }
    }
}