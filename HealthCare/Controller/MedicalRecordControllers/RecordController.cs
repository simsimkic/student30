/***********************************************************************
 * Module:  RecordService.cs
 * Purpose: Definition of the Class Service.RecordService
 ***********************************************************************/

using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using Service.MedicalRecordServices;
using System;
using System.Collections.Generic;

namespace Controller.MedicalRecordControllers
{
   public class RecordController : IRecordController
   {
        public IRecordService _service;

        public RecordController(IRecordService service)
        {
            _service = service;
        }

        public MedicalRecord Create(MedicalRecord entity) => _service.Create(entity);

        public MedicalRecord GetRecordForPatient(int patientID) => _service.GetRecordForPatient(patientID);

        public MedicalRecord Update(MedicalRecord entity) => _service.Update(entity);
        public bool IsPatientAllergic(MedicalRecord entity, List<Drug> drugsForDiagnosis)
                                                    => _service.IsPatientAllergic(entity, drugsForDiagnosis);
    }
}