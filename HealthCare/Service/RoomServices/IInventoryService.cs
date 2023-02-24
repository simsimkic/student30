// File:    IInventoryService.cs
// Created: Friday, May 22, 2020 23:26:18
// Purpose: Definition of Interface IInventoryService

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Service.RoomServices
{
   public interface IInventoryService : IService<Inventory,int>
   {
      void AddAmountOfInventory(Model.Rooms.Inventory inventory, int amount);
      
      void MoveInventoryFromRoom(Model.Rooms.Room from, Model.Rooms.InventoryAmount inventoryAmount);
      void MoveInventoryFromStorage(InventoryAmount inventoryAmount);

      void MoveInventoryBackToStorage(IEnumerable<InventoryAmount> inventoryAmounts);

      IEnumerable<Inventory> GetFilteredInventory(Model.DataFiltration.InventoryFilter inventoryFilter);
      
      IEnumerable<InventoryAmount> GetInventoryAmountInRooms(Model.Rooms.Inventory inventory);
   
   }
}