// File:    IInventoryRepository.cs
// Created: Saturday, May 23, 2020 17:18:13
// Purpose: Definition of Interface IInventoryRepository

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Repository.RoomRepositories
{
   public interface IInventoryRepository : IRepository<Inventory,int>
   {
      IEnumerable<Inventory> GetFilteredInventory(Model.DataFiltration.InventoryFilter inventoryFilter);
   
   }
}