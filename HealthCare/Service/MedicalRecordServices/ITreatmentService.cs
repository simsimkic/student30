// File:    ITreatmentService.cs
// Created: Monday, May 25, 2020 1:00:11
// Purpose: Definition of Interface ITreatmentService

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecordServices
{
   public interface ITreatmentService
   {      
      MedicalRecord AddTreatmentToRecord(Model.MedicalRecords.MedicalRecord record, Model.MedicalRecords.Treatment treatment);
   
   }
}