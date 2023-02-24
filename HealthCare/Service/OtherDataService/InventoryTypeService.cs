// File:    InventoryTypeService.cs
// Created: Tuesday, May 26, 2020 22:45:18
// Purpose: Definition of Class InventoryTypeService

using Model.Rooms;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;

namespace Service.OtherDataService
{
   public class InventoryTypeService : IInventoryTypeService
   {
      public Repository.OtherDataRepository.IInventoryTypeRepository inventoryTypeRepository;

        public InventoryTypeService(IInventoryTypeRepository repository)
        {
            inventoryTypeRepository = repository;
        }
        public IEnumerable<InventoryType> GetAll() => inventoryTypeRepository.GetAll();
    }
}