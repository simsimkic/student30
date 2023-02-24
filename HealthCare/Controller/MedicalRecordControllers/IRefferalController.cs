/***********************************************************************
 * Module:  IRefferalService.cs
 * Purpose: Definition of the Interface Service.MedicalRecordServices.IRefferalService
 ***********************************************************************/

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Controller.MedicalRecordControllers
{
   public interface IRefferalController : ICreate<Refferal>, IUpdate<Refferal>
   {
      IEnumerable<Refferal> GetAllHospitalTreatment();
      
      IEnumerable<Refferal> GetUncompletedHospitalTreatment();
      
      bool CommitHospitalTreatment(Model.MedicalRecords.HospitalTreatment hospitalTreatment);
      
      IEnumerable<UrgentSurgeryRequest> GetUrgentSurgeryRequest();
        IEnumerable<Refferal> GetAllUrgentSurgeryRequest();

        bool CommitUrgentSurgeryRequest(Model.MedicalRecords.UrgentSurgeryRequest urgentSurgeryRequest);

        IEnumerable<Refferal> GetAllRefferals();
    }
}