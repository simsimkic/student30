// File:    IDiagnosisController.cs
// Created: Saturday, May 23, 2020 0:10:19
// Purpose: Definition of Interface IDiagnosisController

using Model.Diagnosis;
using Model.Drug;
using System;
using System.Collections.Generic;

namespace Controller.DiagnosisController
{
   public interface IDiagnosisController : IController<Diagnosis,string>
   {
      IEnumerable<Diagnosis> GetPotentialDiagnosis(List<Symptom> symptomes);

      IEnumerable<Diagnosis> GetFilteredDiagnosisByNameOrCode(string nameOrCode);
   }
}