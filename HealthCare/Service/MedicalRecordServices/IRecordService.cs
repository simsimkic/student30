// File:    IRecordService.cs
// Created: Monday, May 25, 2020 1:00:11
// Purpose: Definition of Interface IRecordService

using Model.Drug;
using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecordServices
{
   public interface IRecordService : ICreate<MedicalRecord>, IUpdate<MedicalRecord>
   {
      Model.MedicalRecords.MedicalRecord GetRecordForPatient(int patientId);
        bool IsPatientAllergic(MedicalRecord entity, List<Drug> drugsForDiagnosis);
    }
}