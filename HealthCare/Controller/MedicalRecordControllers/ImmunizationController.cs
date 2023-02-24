/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class Service.MedicalHistory
 ***********************************************************************/

using Model.MedicalRecords;
using Service;
using System;

namespace Controller.MedicalRecordControllers
{
   public class ImmunizationController : ICUDController<Immunization>
   {
        private ICUDService<Immunization> _service;

        public ImmunizationController(ICUDService<Immunization> service)
        {
            _service = service;
        }

        public MedicalRecord Create(MedicalRecord record, Immunization entity) => _service.Create(record, entity);

        public MedicalRecord Delete(MedicalRecord record, Immunization entity) => _service.Delete(record, entity);

        public MedicalRecord Update(MedicalRecord record) => _service.Update(record);
    }
}