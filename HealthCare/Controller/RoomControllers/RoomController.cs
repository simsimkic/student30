/***********************************************************************
 * Module:  RoomService.cs
 * Purpose: Definition of the Class Service.RoomService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using Service.RoomServices;
using System;
using System.Collections.Generic;

namespace Controller.RoomControllers
{
   public class RoomController : IRoomController
   {
      public Service.RoomServices.IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }
        public Room Create(Room entity) => _service.Create(entity);

        public Room Delete(Room entity) => _service.Delete(entity);

        public IEnumerable<Room> GetAll() => _service.GetAll();

        public IEnumerable<Room> GetFilteredRooms(RoomFilter roomFilter) => _service.GetFilteredRooms(roomFilter);

        public Room GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<InventoryAmount> GetInventoryFromRoom(Room room) => _service.GetInventoryFromRoom(room);

        public Room Update(Room entity) => _service.Update(entity);
    }
}