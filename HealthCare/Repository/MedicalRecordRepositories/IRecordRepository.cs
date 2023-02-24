// File:    IRecordRepository.cs
// Created: Saturday, May 23, 2020 16:53:51
// Purpose: Definition of Interface IRecordRepository

using Model.MedicalRecords;
using System;

namespace Repository.MedicalRecordRepositories
{
   public interface IRecordRepository : ICreate<MedicalRecord>, IUpdate<MedicalRecord>
   {
      Model.MedicalRecords.MedicalRecord GetRecordForPatient(int patientID);
   
   }
}