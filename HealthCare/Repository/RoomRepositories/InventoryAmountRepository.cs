// File:    InventoryAmountRepository.cs
// Created: Monday, May 25, 2020 1:42:05
// Purpose: Definition of Class InventoryAmountRepository

using Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.RoomRepositories
{
   public class InventoryAmountRepository : IInventoryAmountRepository
   {
      public JSONStream<InventoryAmount> _stream;

        public InventoryAmountRepository(JSONStream<InventoryAmount> stream)
        {
            _stream = stream;
        }

        public InventoryAmount Create(InventoryAmount entity)
        {
            InventoryAmount temp;
            if((temp = GetByRoomAndInventory(entity.room, entity.inventory)) != null)
            {
                entity.Amount += temp.Amount;
                return Update(entity);
            }
            List<InventoryAmount> inventoryAmounts = _stream.GetAll().ToList();
            inventoryAmounts.Add(entity);
            _stream.SaveAll(inventoryAmounts);
            return entity;
        }

        public InventoryAmount GetByRoomAndInventory(Room room, Inventory inventory) => _stream.GetAll().SingleOrDefault(x => x.room.RoomNumber == room.RoomNumber && x.inventory.Id == inventory.Id);

        public InventoryAmount Delete(InventoryAmount entity)
        {
            List<InventoryAmount> inventoryAmounts = _stream.GetAll().ToList();
            var entityToRemove = inventoryAmounts.SingleOrDefault(ent => (ent.inventory.Id.CompareTo(entity.inventory.Id) == 0) && (ent.room.RoomNumber.CompareTo(entity.room.RoomNumber) == 0));
            if (entityToRemove != null)
            {
                inventoryAmounts.Remove(entityToRemove);
                _stream.SaveAll(inventoryAmounts);
            }

            return entityToRemove;
        }

        public IEnumerable<InventoryAmount> GetInventoryAmountInRooms(Inventory inventory) => _stream.GetAll().Where(x => x.inventory.Id == inventory.Id);

        public IEnumerable<InventoryAmount> GetInventoryFromRoom(Room room) => _stream.GetAll().Where(x => x.room.RoomNumber == room.RoomNumber);

        public InventoryAmount Update(InventoryAmount entity)
        {
            List<InventoryAmount> inventoryAmounts = _stream.GetAll().ToList();
            if (entity.Amount == 0)
                Delete(entity);
            else
            {
                inventoryAmounts[inventoryAmounts.FindIndex(ent => (ent.inventory.Id.CompareTo(entity.inventory.Id) == 0) 
                                                            && (ent.room.RoomNumber.CompareTo(entity.room.RoomNumber) == 0))] = entity;
                _stream.SaveAll(inventoryAmounts);
            }
            return entity;
        }

        private IEnumerable<InventoryAmount> GetNonUsedInventoryAmounts(Inventory inventory) 
            => _stream.GetAll().Where(x => x.inventory.Id == inventory.Id && x.Usage == 0);

        public bool DeleteAllInventoryAmountsOfInventory(Inventory inventory)
        {
            List<InventoryAmount> inventoryAmounts = _stream.GetAll().ToList();
            if (GetNonUsedInventoryAmounts(inventory).Count() == GetInventoryAmountInRooms(inventory).Count())
            {
                inventoryAmounts.RemoveAll(x => x.inventory.Id == inventory.Id);
                _stream.SaveAll(inventoryAmounts);
                return true;
            }
            return false;
        }
    }
}