// File:    Absence.cs
// Created: Thursday, April 16, 2020 21:29:52
// Purpose: Definition of Class Absence

using System;

namespace Model.Users
{
   public class Absence
   {
        private int id;
      private DateTime startDate;
      private DateTime endDate;
      private AbsenceType absenceType;
      private string reason;
      private bool approved;
      
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
        public AbsenceType AbsenceType
        {
            get { return absenceType; }
            set
            {
                if (absenceType != value)
                {
                    absenceType = value;
                }
            }
        }
        public string Reason
        {
            get { return reason; }
            set
            {
                if (reason != value)
                {
                    reason = value;
                }
            }
        }

        public bool Approved
        {
            get { return approved; }
            set
            {
                if (approved != value)
                {
                    approved = value;
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