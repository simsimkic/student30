// File:    IRoomService.cs
// Created: Friday, May 22, 2020 23:27:46
// Purpose: Definition of Interface IRoomService

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Service.RoomServices
{
   public interface IRoomService : IService<Room,int>
   {
      IEnumerable<Room> GetFilteredRooms(Model.DataFiltration.RoomFilter roomFilter);      
      IEnumerable<InventoryAmount> GetInventoryFromRoom(Model.Rooms.Room room);
   
   }
}