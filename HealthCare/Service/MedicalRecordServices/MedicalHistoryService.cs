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
   public class MedicalHistoryService : ICUDService<MedicalHistory>
   {
        private IRecordRepository _repository;

        public MedicalHistoryService(IRecordRepository repository)
        {
            _repository = repository;
        }

        public MedicalRecord Create(MedicalRecord record, MedicalHistory entity)
        {
            record.MedicalHistory.Add(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Delete(MedicalRecord record, MedicalHistory entity)
        {
            record.MedicalHistory.Remove(entity);
            return _repository.Update(record);
        }

        public MedicalRecord Update(MedicalRecord record) => _repository.Update(record);
    }
}