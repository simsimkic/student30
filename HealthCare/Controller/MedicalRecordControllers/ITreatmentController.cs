/***********************************************************************
 * Module:  ITreatmentService.cs
 * Purpose: Definition of the Interface Service.MedicalRecordServices.ITreatmentService
 ***********************************************************************/

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Controller.MedicalRecordControllers
{
   public interface ITreatmentController
   {
        MedicalRecord AddTreatmentToRecord(Model.MedicalRecords.MedicalRecord record, Model.MedicalRecords.Treatment treatment);
   
   }
}