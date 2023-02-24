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
   public class MedicalHistoryController : ICUDController<MedicalHistory>
   {
        private ICUDService<MedicalHistory> _service;
        public MedicalHistoryController(MedicalHistoryService service)
        {
            _service = service;
        }
        public MedicalRecord Create(MedicalRecord record, MedicalHistory entity) => _service.Create(record, entity);

        public MedicalRecord Delete(MedicalRecord record, MedicalHistory entity) => _service.Delete(record, entity);

        public MedicalRecord Update(MedicalRecord record) => _service.Update(record);
    }
}