// File:    SymptomService.cs
// Created: Tuesday, May 26, 2020 22:43:38
// Purpose: Definition of Class SymptomService

using Model.Diagnosis;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;

namespace Service.OtherDataService
{
   public class SymptomService : ISymptomService
   {
      public ISymptomRepository _repository;

        public SymptomService(ISymptomRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Symptom> GetAll() => _repository.GetAll();
    }
}