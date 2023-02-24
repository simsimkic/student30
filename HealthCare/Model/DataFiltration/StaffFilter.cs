// File:    StaffFilter.cs
// Created: Monday, May 11, 2020 21:21:37
// Purpose: Definition of Class StaffFilter

using Model.Users;
using System;

namespace Model.DataFiltration
{
   public class StaffFilter
   {
      private string staffType;
      private string name;
      private string surname;
      private int id;

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
        public string StaffType
        {
            get { return staffType; }
            set
            {
                if (staffType != value)
                {
                    staffType = value;
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

    }
}