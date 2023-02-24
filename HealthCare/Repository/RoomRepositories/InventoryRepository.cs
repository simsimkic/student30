/***********************************************************************
 * Module:  InventoryRepository.cs
 * Purpose: Definition of the Class Model.Rooms.InventoryRepository
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.RoomRepositories
{
   public class InventoryRepository : IInventoryRepository
   {
      public JSONStream<Inventory> _stream;
      public IntSequencer _sequencer;

        public InventoryRepository(JSONStream<Inventory> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }

        public Inventory Create(Inventory entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Inventory> inventories = _stream.GetAll().ToList();
            inventories.Add(entity);
            _stream.SaveAll(inventories);
            return entity;
        }

        public Inventory Delete(Inventory entity)
        {
            List<Inventory> inventories = _stream.GetAll().ToList();
            var entityToRemove = inventories.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                inventories.Remove(entityToRemove);
                _stream.SaveAll(inventories);
            }

            return entityToRemove;
        }

        public IEnumerable<Inventory> GetAll() => _stream.GetAll();

        public IEnumerable<Inventory> GetFilteredInventory(InventoryFilter inventoryFilter)
            => GetAll().Where(x => ((inventoryFilter.AmountFrom > 0) ? x.QuantityHospital >= inventoryFilter.AmountFrom : true) &&
                                   ((inventoryFilter.AmountTo > 0) ? x.QuantityHospital <= inventoryFilter.AmountTo : true) &&
                                   ((inventoryFilter.InventoryType != null) ? inventoryFilter.InventoryType.Name.Equals(x.InventoryType.Name) : true) &&
                                   ((inventoryFilter.Name != "") ? inventoryFilter.Name.Equals(x.Name) : true));

        public Inventory GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);

        public Inventory Update(Inventory entity)
        {
            List<Inventory> inventories = _stream.GetAll().ToList();
            inventories[inventories.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(inventories);
            return entity;
        }
        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Inventory> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);
    }
}