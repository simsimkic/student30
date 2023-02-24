/***********************************************************************
 * Module:  InventoryService.cs
 * Purpose: Definition of the Class Service.InventoryService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using Repository.RoomRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.RoomServices
{
   public class InventoryService : IInventoryService
   {
      public Repository.RoomRepositories.IInventoryRepository _inventoryRepository;
      public Repository.RoomRepositories.IInventoryAmountRepository _inventoryAmountRepository;

        public InventoryService(IInventoryRepository inventoryRepository, IInventoryAmountRepository inventoryAmountRepository)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryAmountRepository = inventoryAmountRepository;
        }
        public void AddAmountOfInventory(Inventory inventory, int amount)
        {
            inventory.QuantityHospital += amount;
            inventory.QuantityStorage += amount;
            Update(inventory);
        }

        private void AddAmountOfInventoryToStorage(Inventory inventory, int amount)
        {
            inventory.QuantityStorage += amount;
            Update(inventory);
        }

        public Inventory Create(Inventory entity) => _inventoryRepository.Create(entity);

        public Inventory Delete(Inventory entity) => (_inventoryAmountRepository.DeleteAllInventoryAmountsOfInventory(entity)) ? _inventoryRepository.Delete(entity) : null;

        public IEnumerable<Inventory> GetAll() => _inventoryRepository.GetAll();

        public IEnumerable<Inventory> GetFilteredInventory(InventoryFilter inventoryFilter) => _inventoryRepository.GetFilteredInventory(inventoryFilter);

        public IEnumerable<InventoryAmount> GetInventoryAmountInRooms(Inventory inventory) => _inventoryAmountRepository.GetInventoryAmountInRooms(inventory);
        public Inventory GetFromID(int id) => _inventoryRepository.GetFromID(id);

        public void MoveInventoryFromStorage(InventoryAmount inventoryAmount)
        {
            Inventory toUpdate = GetFromID(inventoryAmount.inventory.Id);
            toUpdate.QuantityStorage -= inventoryAmount.Amount;
            Update(toUpdate);
            _inventoryAmountRepository.Create(inventoryAmount);
        }

        public void MoveInventoryFromRoom(Room from,InventoryAmount inventoryAmount)
        {
            InventoryAmount toUpdate = _inventoryAmountRepository.GetByRoomAndInventory(from, _inventoryRepository.GetFromID(inventoryAmount.inventory.Id));
            toUpdate.Amount -= inventoryAmount.Amount;
            _inventoryAmountRepository.Update(toUpdate);
            _inventoryAmountRepository.Create(inventoryAmount);
        }

        public Inventory Update(Inventory entity) => _inventoryRepository.Update(entity);

        public void MoveInventoryBackToStorage(IEnumerable<InventoryAmount> inventoryAmounts)
            => inventoryAmounts.ToList().ForEach(x => { AddAmountOfInventoryToStorage(GetFromID(x.inventory.Id), x.Amount); _inventoryAmountRepository.Delete(x);});
    }
}