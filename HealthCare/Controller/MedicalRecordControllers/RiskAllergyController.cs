/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class Service.MedicalHistory
 ***********************************************************************/

using Model.MedicalRecords;
using Service;
using System;

namespace Controller.MedicalRecordControllers
{
   public class RiskAllergyController : ICUDController<RiskAllergies>
   {
        private ICUDService<RiskAllergies> _service;

        public RiskAllergyController(ICUDService<RiskAllergies> service)
        {
            _service = service;
        }

        public MedicalRecord Create(MedicalRecord record, RiskAllergies entity) => _service.Create(record, entity);

        public MedicalRecord Delete(MedicalRecord record, RiskAllergies entity) => _service.Delete(record, entity);

        public MedicalRecord Update(MedicalRecord record) => _service.Update(record);

    }
}