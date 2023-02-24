// File:    ICUDController.cs
// Created: Monday, May 25, 2020 1:10:55
// Purpose: Definition of Interface ICUDController

using System;

namespace Controller
{
   public interface ICUDController<T>
   {
      Model.MedicalRecords.MedicalRecord Create(Model.MedicalRecords.MedicalRecord record, T entity);
      
      Model.MedicalRecords.MedicalRecord Update(Model.MedicalRecords.MedicalRecord record);
      
      Model.MedicalRecords.MedicalRecord Delete(Model.MedicalRecords.MedicalRecord record, T entity);
   
   }
}