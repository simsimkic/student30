// File:    AllergyService.cs
// Created: Tuesday, May 26, 2020 22:46:18
// Purpose: Definition of Class AllergyService

using Model.Drug;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;

namespace Service.OtherDataService
{
   public class AllergyService : IAllergyService
   {
      public Repository.OtherDataRepository.IAllergyRepository _allergyRepository;

        public AllergyService(IAllergyRepository repository)
        {
            _allergyRepository = repository;
        }
        public IEnumerable<Allergen> GetAll() => _allergyRepository.GetAll();
    }
}