// File:    DoctorOccupation.cs
// Created: Monday, May 11, 2020 23:10:41
// Purpose: Definition of Class DoctorOccupation

using System;

namespace Model.DataReport
{
   public class DoctorOccupation
   {
      private int totalWorkTime;
      private int occupationWorkTime;
      
      public Model.Users.Doctor doctor;

        public int TotalWorkTime
        {
            get { return totalWorkTime; }
            set
            {
                if (totalWorkTime != value)
                {
                    totalWorkTime = value;
                }
            }
        }
        public int OccupationWorkTime
        {
            get { return occupationWorkTime; }
            set
            {
                if (occupationWorkTime != value)
                {
                    occupationWorkTime = value;
                }
            }
        }

    }
}