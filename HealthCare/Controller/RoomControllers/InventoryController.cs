/***********************************************************************
 * Module:  InventoryService.cs
 * Purpose: Definition of the Class Service.InventoryService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using Service.RoomServices;
using System;
using System.Collections.Generic;

namespace Controller.RoomControllers
{
   public class InventoryController : IInventoryController
   {
      public Service.RoomServices.IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        public void AddAmountOfInventory(Inventory inventory, int amount) => _service.AddAmountOfInventory(inventory, amount);

        public Inventory Create(Inventory entity) => _service.Create(entity);

        public Inventory Delete(Inventory entity) => _service.Delete(entity);

        public IEnumerable<Inventory> GetAll() => _service.GetAll();

        public IEnumerable<Inventory> GetFilteredInventory(InventoryFilter inventoryFilter) => _service.GetFilteredInventory(inventoryFilter);

        public Inventory GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<InventoryAmount> GetInventoryAmountInRooms(Inventory inventory) => _service.GetInventoryAmountInRooms(inventory);

        public void MoveInventoryFromRoom(Room from, InventoryAmount inventoryAmount) => _service.MoveInventoryFromRoom(from, inventoryAmount);

        public void MoveInventoryFromStorage(InventoryAmount inventoryAmount) => _service.MoveInventoryFromStorage(inventoryAmount);

        public Inventory Update(Inventory entity) => _service.Update(entity);
    }
}