// File:    AllergyController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Class AllergyController

using Model.Drug;
using Service.OtherDataService;
using System;
using System.Collections.Generic;

namespace Controller.OtherDataController
{
   public class AllergyController : IAllergyController
   {
      public Service.OtherDataService.IAllergyService _service;

        public AllergyController(IAllergyService service) 
        {
            _service = service;
        }
        public IEnumerable<Allergen> GetAll() => _service.GetAll();
    }
}