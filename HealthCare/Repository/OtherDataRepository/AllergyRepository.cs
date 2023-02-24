// File:    AllergyRepository.cs
// Created: Monday, May 25, 2020 2:06:42
// Purpose: Definition of Class AllergyRepository

using Model.Drug;
using System;
using System.Collections.Generic;

namespace Repository.OtherDataRepository
{
   public class AllergyRepository : IAllergyRepository
   {
      public JSONStream<Allergen> _stream;

        public AllergyRepository(JSONStream<Allergen> stream)
        {
            _stream = stream;
        }
        public IEnumerable<Allergen> GetAll() => _stream.GetAll();
    }
}