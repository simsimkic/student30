// File:    ICUDService.cs
// Created: Sunday, May 24, 2020 18:06:12
// Purpose: Definition of Interface ICUDService

using System;

namespace Service
{
   public interface ICUDService<T>
   {
      Model.MedicalRecords.MedicalRecord Create(Model.MedicalRecords.MedicalRecord record, T entity);
      
      Model.MedicalRecords.MedicalRecord Update(Model.MedicalRecords.MedicalRecord record);
      
      Model.MedicalRecords.MedicalRecord Delete(Model.MedicalRecords.MedicalRecord record, T entity);
   
   }
}