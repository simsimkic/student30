// File:    SpecializationController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Class SpecializationController

using Model.Users;
using Service.OtherDataService;
using System;
using System.Collections.Generic;

namespace Controller.OtherDataController
{
   public class SpecializationController : ISpecializationController
   {
      public Service.OtherDataService.ISpecializationService _service;

        public SpecializationController(ISpecializationService service)
        {
            _service = service;
        }

        public IEnumerable<Specialization> GetAll() => _service.GetAll();
    }
}