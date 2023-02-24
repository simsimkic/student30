// File:    PatientTreatmentReport.cs
// Created: Monday, May 11, 2020 23:10:39
// Purpose: Definition of Class PatientTreatmentReport

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Model.DataReport
{
   public class PatientTreatmentReport
   {
        private int id;
        private DateTime dateFrom;
        private DateTime dateTo;
      
      public Model.Users.Patient patient;
      public IList<Treatment> treatment;
   
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        public DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }

    }
}