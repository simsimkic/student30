/***********************************************************************
 * Module:  IRecordService.cs
 * Purpose: Definition of the Interface Service.MedicalRecordServices.IRecordService
 ***********************************************************************/

using Model.Drug;
using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Controller.MedicalRecordControllers
{
   public interface IRecordController : ICreate<MedicalRecord>, IUpdate<MedicalRecord>
   {
      MedicalRecord GetRecordForPatient(int patientID);
      bool IsPatientAllergic(MedicalRecord entity, List<Drug> drugsForDiagnosis);
   }
}