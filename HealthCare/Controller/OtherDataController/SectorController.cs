// File:    SectorController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Class SectorController

using Model.Rooms;
using Service.OtherDataService;
using System;
using System.Collections.Generic;

namespace Controller.OtherDataController
{
   public class SectorController : ISectorController
   {
      public Service.OtherDataService.ISectorService _service;

        public SectorController(ISectorService service)
        {
            _service = service;
        }
        public IEnumerable<Sector> GetAll() => _service.GetAll();
    }
}