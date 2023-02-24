// File:    IDiagnosisRepository.cs
// Created: Saturday, May 23, 2020 16:34:17
// Purpose: Definition of Interface IDiagnosisRepository

using Model.Diagnosis;
using System;
using System.Collections.Generic;

namespace Repository.DiagnosisRepositories
{
   public interface IDiagnosisRepository : IRepository<Diagnosis,string>
   {

        IEnumerable<Diagnosis> getFilteredDiagnosisByNameOrCode(string nameOrCode);
    }
}