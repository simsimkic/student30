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
   public class RiskFactorService : ICUDService<RiskFactor>
   {
         public IRecordRepository _repository;

        public RiskFactorService(IRecordRepository repository)
        {
            _repository = repository;
        }
        public MedicalRecord Create(MedicalRecord record, RiskFactor entity)
        {
            record.RiskFactor.Add(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Delete(MedicalRecord record, RiskFactor entity)
        {
            record.RiskFactor.Remove(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Update(MedicalRecord record) => _repository.Update(record);
    }
}