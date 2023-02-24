/***********************************************************************
 * Module:  TreatmentService.cs
 * Purpose: Definition of the Class Service.TreatmentService
 ***********************************************************************/

using Model.MedicalRecords;
using Model.Users;
using Repository.MedicalRecordRepositories;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Service.MedicalRecordServices
{
   public class TreatmentService : ITreatmentService
   {
        private IRecordRepository _repository;

        public TreatmentService(IRecordRepository repository)
        {
            _repository = repository;
        }
        public MedicalRecord AddTreatmentToRecord(MedicalRecord record, Treatment treatment)
        {
            record.Treatment.Add(treatment);
            return _repository.Update(record);
        }
    }
}