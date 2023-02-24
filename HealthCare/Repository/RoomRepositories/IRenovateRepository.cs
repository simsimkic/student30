// File:    IRenovateRepository.cs
// Created: Saturday, May 23, 2020 17:18:06
// Purpose: Definition of Interface IRenovateRepository

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Repository.RoomRepositories
{
   public interface IRenovateRepository : ICreate<Renovate>, IDelete<Renovate>, IGetAll<Renovate>, IGet<Renovate, int>
    {
        IEnumerable<Renovate> GetFilteredRenovations(Model.DataFiltration.RenovateFilter renovateFilter);
        void DeleteAllRoomRenovations(Room room);
        IEnumerable<Renovate> GetFutureRenovationsForRoom(Room room);
        bool IsRenovating(Room room, DateTime date);
    }
}