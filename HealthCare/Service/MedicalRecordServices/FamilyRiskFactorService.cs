/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class Service.MedicalHistory
 ***********************************************************************/

using Model.MedicalRecords;
using Repository.MedicalRecordRepositories;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service.MedicalRecordServices
{
    public class FamilyRiskFactorService : ICUDService<FamilyRiskFactor>
    {
        private IRecordRepository _repository;

        public FamilyRiskFactorService(IRecordRepository repository)
        {
            _repository = repository;
        }

        public MedicalRecord Create(MedicalRecord record, FamilyRiskFactor entity)
        {
            record.FamilyRiskFactor.Add(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Delete(MedicalRecord record, FamilyRiskFactor entity)
        {
            record.FamilyRiskFactor.Remove(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Update(MedicalRecord record) => _repository.Update(record);
    } 
}
