/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class Service.MedicalHistory
 ***********************************************************************/

using Model.MedicalRecords;
using Service;
using Service.MedicalRecordServices;
using System;

namespace Controller.MedicalRecordControllers
{
   public class FamilyRiskFactorController : ICUDController<FamilyRiskFactor>
   {
        private ICUDService<FamilyRiskFactor> _service;

        public FamilyRiskFactorController(FamilyRiskFactorService service)
        {
            _service = service;
        }

        public MedicalRecord Create(MedicalRecord record, FamilyRiskFactor entity) => _service.Create(record, entity);
        public MedicalRecord Delete(MedicalRecord record, FamilyRiskFactor entity) => _service.Delete(record, entity);
        public MedicalRecord Update(MedicalRecord record) => _service.Update(record);

    }
}