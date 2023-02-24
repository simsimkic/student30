// File:    IngredientRepository.cs
// Created: Monday, May 25, 2020 2:10:15
// Purpose: Definition of Class IngredientRepository

using Model.Drug;
using System;
using System.Collections.Generic;

namespace Repository.OtherDataRepository
{
   public class IngredientRepository : IIngredientRepository
   {
      public JSONStream<Ingredient> _stream;

        public IngredientRepository(JSONStream<Ingredient> stream)
        {
            _stream = stream;
        }
        public IEnumerable<Ingredient> GetAll() => _stream.GetAll();
    }
}