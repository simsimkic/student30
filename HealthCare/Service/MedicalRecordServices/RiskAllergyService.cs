/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class Service.MedicalHistory
 ***********************************************************************/

using Model.MedicalRecords;
using Repository.MedicalRecordRepositories;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecordServices
{
   public class RiskAllergyService : ICUDService<RiskAllergies>
   {
      public IRecordRepository _repository;

        public RiskAllergyService(IRecordRepository repository)
        {
            _repository = repository;
        }
        public MedicalRecord Create(MedicalRecord record, RiskAllergies entity)
        {
            record.RiskAllergies.Add(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Delete(MedicalRecord record, RiskAllergies entity)
        {
            record.RiskAllergies.Remove(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Update(MedicalRecord record) =>_repository.Update(record);
    }
}