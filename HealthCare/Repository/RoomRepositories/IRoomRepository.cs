// File:    IRoomRepository.cs
// Created: Saturday, May 23, 2020 17:16:42
// Purpose: Definition of Interface IRoomRepository

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Repository.RoomRepositories
{
   public interface IRoomRepository : IRepository<Room,int>
   {
      IEnumerable<Room> GetFilteredRooms(Model.DataFiltration.RoomFilter roomFilter);
   
   }
}