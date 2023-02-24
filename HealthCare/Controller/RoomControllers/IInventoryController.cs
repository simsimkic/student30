// File:    IInventoryController.cs
// Created: Saturday, May 23, 2020 0:21:37
// Purpose: Definition of Interface IInventoryController

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Controller.RoomControllers
{
   public interface IInventoryController : IController<Inventory,int>
   {
      void AddAmountOfInventory(Model.Rooms.Inventory inventory, int amount);

        void MoveInventoryFromRoom(Model.Rooms.Room from, Model.Rooms.InventoryAmount inventoryAmount);
        void MoveInventoryFromStorage(InventoryAmount inventoryAmount);

        IEnumerable<Inventory> GetFilteredInventory(Model.DataFiltration.InventoryFilter inventoryFilter);
      
      IEnumerable<InventoryAmount> GetInventoryAmountInRooms(Model.Rooms.Inventory inventory);
   
   }
}