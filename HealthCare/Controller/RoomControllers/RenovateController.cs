/***********************************************************************
 * Module:  RenovateService.cs
 * Purpose: Definition of the Class Service.RenovateService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using Service.RoomServices;
using System;
using System.Collections.Generic;

namespace Controller.RoomControllers
{
   public class RenovateController : IRenovateController
   {
      public Service.RoomServices.IRenovateService _service;

        public RenovateController(IRenovateService service)
        {
            _service = service;
        }

        public Renovate Create(Renovate entity) => _service.Create(entity);

        public Renovate Delete(Renovate entity) => _service.Delete(entity);
        public IEnumerable<Renovate> GetAll() => _service.GetAll();

        public IEnumerable<Renovate> GetFilteredRenovations(RenovateFilter renovateFilter) => _service.GetFilteredRenovations(renovateFilter);

        public Renovate GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<Renovate> GetFutureRenovationsForRoom(Room room) => _service.GetFutureRenovationsForRoom(room);

    }
}