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
   public class ImmunizationService : ICUDService<Immunization>
    {
        private IRecordRepository _repository;

        public ImmunizationService(IRecordRepository repository)
        {
            _repository = repository;
        }
        public MedicalRecord Create(MedicalRecord record, Immunization entity)
        {
            record.Immunization.Add(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Delete(MedicalRecord record, Immunization entity)
        {
            record.Immunization.Remove(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Update(MedicalRecord record) => _repository.Update(record);
    }
}