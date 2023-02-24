// File:    DoctorOccupationReport.cs
// Created: Monday, May 11, 2020 23:10:40
// Purpose: Definition of Class DoctorOccupationReport

using System;
using System.Collections.Generic;

namespace Model.DataReport
{
   public class DoctorOccupationReport
   {
      private int id;
      private DateTime dateFrom;
      private DateTime dateTo;
      private Model.Users.WorkPlace workPlace;
      
      public List<DoctorOccupation> doctorOccupation;

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

        public Model.Users.WorkPlace WorkPlace
        {
            get { return workPlace; }
            set
            {
                if (workPlace != value)
                {
                    workPlace = value;
                }
            }
        }
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                if (dateTo != value)
                {
                    dateTo = value;
                }
            }
        }
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                if (dateFrom != value)
                {
                    dateFrom = value;
                }
            }
        }
    }
}