// File:    InventoryTypeRepository.cs
// Created: Monday, May 25, 2020 2:10:29
// Purpose: Definition of Class InventoryTypeRepository

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Repository.OtherDataRepository
{
   public class InventoryTypeRepository : IInventoryTypeRepository
   {
      public JSONStream<InventoryType> _stream;

        public InventoryTypeRepository(JSONStream<InventoryType> stream)
        {
            _stream = stream;
        }
        public IEnumerable<InventoryType> GetAll() => _stream.GetAll();
    }
}