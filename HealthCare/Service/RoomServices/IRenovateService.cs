// File:    IRenovateService.cs
// Created: Friday, May 22, 2020 23:29:06
// Purpose: Definition of Interface IRenovateService

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Service.RoomServices
{
   public interface IRenovateService : ICreate<Renovate>, IDelete<Renovate>, IGetAll<Renovate>, IGet<Renovate, int>
    {
      IEnumerable<Renovate> GetFilteredRenovations(Model.DataFiltration.RenovateFilter renovateFilter);
      IEnumerable<Renovate> GetFutureRenovationsForRoom(Room room);
      bool IsRenovating(Room room, DateTime date);


   }
}