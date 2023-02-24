/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class Service.MedicalHistory
 ***********************************************************************/

using Model.MedicalRecords;
using Service;
using System;

namespace Controller.MedicalRecordControllers
{
   public class RiskFactorController : ICUDController<RiskFactor>
   {
        public ICUDService<RiskFactor> _service;
        public RiskFactorController(ICUDService<RiskFactor> service)
        {
            _service = service;
        }

        public MedicalRecord Create(MedicalRecord record, RiskFactor entity) => _service.Create(record, entity);

        public MedicalRecord Delete(MedicalRecord record, RiskFactor entity) => _service.Delete(record, entity);

        public MedicalRecord Update(MedicalRecord record) => _service.Update(record);
    }
}