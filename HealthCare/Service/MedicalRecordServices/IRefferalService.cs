// File:    IRefferalService.cs
// Created: Monday, May 25, 2020 1:00:11
// Purpose: Definition of Interface IRefferalService

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Service.MedicalRecordServices
{
   public interface IRefferalService : ICreate<Refferal>, IUpdate<Refferal>
   {
      IEnumerable<Refferal> GetAllHospitalTreatment();
      
      IEnumerable<Refferal> GetUncompletedHospitalTreatment();
      
      bool CommitHospitalTreatment(Model.MedicalRecords.HospitalTreatment hospitalTreatment);
      
      IEnumerable<UrgentSurgeryRequest> GetUrgentSurgeryRequest();
      
      bool CommitUrgentSurgeryRequest(Model.MedicalRecords.UrgentSurgeryRequest urgentSurgeryRequest);
        IEnumerable<Refferal> GetAllUrgentSurgeryRequest();
        IEnumerable<Refferal> GetAllRefferals();
    }
}