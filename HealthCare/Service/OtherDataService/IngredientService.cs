// File:    IgredientService.cs
// Created: Tuesday, May 26, 2020 22:45:53
// Purpose: Definition of Class IgredientService

using Model.Drug;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;

namespace Service.OtherDataService
{
   public class IgredientService : IIngredientService
   {
      public Repository.OtherDataRepository.IIngredientRepository _ingredientRepository;


        public IgredientService(IIngredientRepository repository)
        {
            _ingredientRepository = repository;
        }
        public IEnumerable<Ingredient> GetAll() => _ingredientRepository.GetAll();
    }
}