// File:    WorkDay.cs
// Created: Thursday, April 16, 2020 20:25:15
// Purpose: Definition of Class WorkDay

using System;

namespace Model.Users
{
   public class WorkDay
   {
      private Days day;
      private int startTime;
      private int endTime;

        public Days Day
        {
            get { return day; }
            set
            {
                if (day != value)
                {
                    day = value;
                }
            }
        }

        public int StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                }
            }
        }

        public int EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                }
            }
        }

    }
}