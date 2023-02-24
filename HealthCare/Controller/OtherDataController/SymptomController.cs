// File:    SymptomController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Class SymptomController

using Model.Diagnosis;
using Service.OtherDataService;
using System;
using System.Collections.Generic;

namespace Controller.OtherDataController
{
   public class SymptomController : ISymptomController
   {
        public ISymptomService _service;

        public SymptomController(ISymptomService service)
        {
            _service = service;
        }
        public IEnumerable<Symptom> GetAll() => _service.GetAll();

    }
}