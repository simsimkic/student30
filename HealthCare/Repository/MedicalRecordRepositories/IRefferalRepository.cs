// File:    IRefferalRepository.cs
// Created: Saturday, May 23, 2020 17:02:54
// Purpose: Definition of Interface IRefferalRepository

using Model.MedicalRecords;
using System;
using System.Collections.Generic;

namespace Repository.MedicalRecordRepositories
{
   public interface IRefferalRepository : ICreate<Refferal>, IUpdate<Refferal>
   {
      IEnumerable<HospitalTreatment> GetUncompletedHospitalTreatment();
      
      IEnumerable<UrgentSurgeryRequest> GetUrgentSurgeryRequest();
        IEnumerable<HospitalTreatment> GetAllHospitalTreatment();
        IEnumerable<Refferal> GetAllUrgentSurgeryRequest();
        IEnumerable<Refferal> GetAllRefferals();
    }
}