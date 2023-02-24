// File:    IRenovateController.cs
// Created: Saturday, May 23, 2020 0:23:31
// Purpose: Definition of Interface IRenovateController

using Model.Rooms;
using Repository;
using System;
using System.Collections.Generic;

namespace Controller.RoomControllers
{
   public interface IRenovateController : ICreate<Renovate>, IDelete<Renovate>, IGetAll<Renovate>, IGet<Renovate,int>
   {
      IEnumerable<Renovate> GetFilteredRenovations(Model.DataFiltration.RenovateFilter renovateFilter);
      IEnumerable<Renovate> GetFutureRenovationsForRoom(Room room);
    }
}