// File:    IDiagnosisService.cs
// Created: Friday, May 22, 2020 22:56:06
// Purpose: Definition of Interface IDiagnosisService

using Model.Diagnosis;
using System;
using System.Collections.Generic;

namespace Service.DiagnosisService
{
   public interface IDiagnosisService : IService<Diagnosis,string>
   {
      IEnumerable<Diagnosis> GetPotentialDiagnosis(List<Symptom> symptomes);
      IEnumerable<Diagnosis> GetFilteredDiagnosisByNameOrCode(string nameOrCode);
   }
}