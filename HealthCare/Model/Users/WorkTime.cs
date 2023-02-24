// File:    WorkTime.cs
// Created: Thursday, April 16, 2020 20:04:39
// Purpose: Definition of Class WorkTime

using System;
using System.Collections.Generic;

namespace Model.Users
{
   public class WorkTime
   {
      private int id;
      private DateTime startDate;
      private DateTime endDate;
      
      public List<WorkDay> workDay;
      public Staff staff;

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
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                }
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                }
            }
        }

    }
}