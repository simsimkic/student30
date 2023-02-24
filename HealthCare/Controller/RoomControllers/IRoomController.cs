// File:    IRoomController.cs
// Created: Saturday, May 23, 2020 0:20:50
// Purpose: Definition of Interface IRoomController

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Controller.RoomControllers
{
   public interface IRoomController : IController<Room,int>
   {
      IEnumerable<Room> GetFilteredRooms(Model.DataFiltration.RoomFilter roomFilter);
           
      IEnumerable<InventoryAmount> GetInventoryFromRoom(Model.Rooms.Room room);
   
   }
}