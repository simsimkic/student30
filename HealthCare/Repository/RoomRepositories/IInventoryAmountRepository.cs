// File:    IInventoryAmountRepository.cs
// Created: Monday, May 25, 2020 1:42:22
// Purpose: Definition of Interface IInventoryAmountRepository

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Repository.RoomRepositories
{
   public interface IInventoryAmountRepository : ICreate<InventoryAmount>, IUpdate<InventoryAmount>, IDelete<InventoryAmount>
   {
      IEnumerable<InventoryAmount> GetInventoryAmountInRooms(Model.Rooms.Inventory inventory);
      
      IEnumerable<InventoryAmount> GetInventoryFromRoom(Model.Rooms.Room room);

      InventoryAmount GetByRoomAndInventory(Room room, Inventory inventory);

      bool DeleteAllInventoryAmountsOfInventory(Inventory inventory);
   }
}