// File:    TreatmentReport.cs
// Created: Monday, May 11, 2020 23:24:47
// Purpose: Definition of Class TreatmentReport

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Model.DataReport
{
   public class TreatmentReport
   {
      private int id;
      private DateTime dateFrom;
      private DateTime dateTo;
      
      public List<Treatment> treatments;
      
   
   }
}