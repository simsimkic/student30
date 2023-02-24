// File:    InventoryTypeController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Class InventoryTypeController

using Model.Rooms;
using Service.OtherDataService;
using System;
using System.Collections.Generic;

namespace Controller.OtherDataController
{
   public class InventoryTypeController : IInventoryTypeController
   {
      public Service.OtherDataService.IInventoryTypeService _service;

        public InventoryTypeController(IInventoryTypeService service)
        {
            _service = service;
        }
        public IEnumerable<InventoryType> GetAll() => _service.GetAll();
    }
}