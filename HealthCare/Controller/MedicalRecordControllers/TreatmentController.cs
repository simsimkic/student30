/***********************************************************************
 * Module:  TreatmentService.cs
 * Purpose: Definition of the Class Service.TreatmentService
 ***********************************************************************/

using Model.MedicalRecords;
using Model.Users;
using Service.MedicalRecordServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Controller.MedicalRecordControllers
{
   public class TreatmentController : ITreatmentController
   {
        private ITreatmentService _service;

        public TreatmentController(ITreatmentService service)
        {
            _service = service;
        }
        public MedicalRecord AddTreatmentToRecord(MedicalRecord record, Treatment treatment) 
                                                        => _service.AddTreatmentToRecord(record, treatment);
    }
}