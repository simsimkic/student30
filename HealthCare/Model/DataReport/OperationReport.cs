// File:    OperationReport.cs
// Created: Monday, May 11, 2020 23:21:42
// Purpose: Definition of Class OperationReport

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Model.DataReport
{
   public class OperationReport
   {
      private int id;
      private DateTime dateFrom;
      private DateTime dateTo;
      
      public List<Treatment> treatment;
   
   }
}