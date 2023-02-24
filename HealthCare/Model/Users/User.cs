/***********************************************************************
 * Module:  User.cs
 * Purpose: Definition of the Class User
 ***********************************************************************/

using System;

namespace Model.Users
{
   public abstract class User
   {
      private Usertype userType;
      private int id;
      private string password;
      private string jmbg;
      private string name;
      private string surname;
      private DateTime birthDate;
      private Gender gender;
      
      private City birthPlace;
      private Adress residence;
      private Contact contact;

        public City BirthPlace
        {
            get { return birthPlace; }
            set
            {
                if (birthPlace != value)
                {
                    birthPlace = value;
                }
            }
        }
        public Adress Residence
        {
            get { return residence; }
            set
            {
                if (residence != value)
                {
                    residence = value;
                }
            }
        }
        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (contact != value)
                {
                    contact = value;
                }
            }
        }

        public string JMBG
        {
            get { return jmbg; }
            set
            {
                if (jmbg != value)
                {
                    jmbg = value;
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

        public Usertype UserType
        {
            get { return userType; }
            set
            {
                if (userType != value)
                {
                    userType = value;
                }
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                if (surname != value)
                {
                    surname = value;
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                }
            }
        }
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


        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                }
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                }
            }
        }
    }
}