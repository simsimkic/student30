// File:    IgredientController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Class IgredientController

using Model.Drug;
using Service.OtherDataService;
using System;
using System.Collections.Generic;

namespace Controller.OtherDataController
{
   public class IngredientController : IIngredientController
   {
      public Service.OtherDataService.IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }
        public IEnumerable<Ingredient> GetAll() => _service.GetAll();
    }
}