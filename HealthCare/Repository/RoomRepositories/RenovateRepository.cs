/***********************************************************************
 * Module:  RenovateRepository.cs
 * Purpose: Definition of the Class Repository.RenovateRepository
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.RoomRepositories
{
   public class RenovateRepository : IRenovateRepository
   {
      public JSONStream<Renovate> _stream;
      public IntSequencer _sequencer;

        public RenovateRepository(JSONStream<Renovate> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }

        public Renovate Create(Renovate entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Renovate> renovations = _stream.GetAll().ToList();
            renovations.Add(entity);
            _stream.SaveAll(renovations);
            return entity;
        }

        public void DeleteAllRoomRenovations(Room room)
        {
            List<Renovate> renovations = _stream.GetAll().ToList();
            renovations.RemoveAll(x => x.room.RoomNumber == room.RoomNumber);
             _stream.SaveAll(renovations);
        }

        public Renovate Delete(Renovate entity)
        {
            List<Renovate> renovations = _stream.GetAll().ToList();
            var entityToRemove = renovations.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                renovations.Remove(entityToRemove);
                _stream.SaveAll(renovations);
            }

            return entityToRemove;
        }

        public IEnumerable<Renovate> GetAll() => _stream.GetAll();

        public IEnumerable<Renovate> GetFilteredRenovations(RenovateFilter renovateFilter)
            => GetAll().Where(x => ((renovateFilter.Sector != null) ? renovateFilter.Sector.Name == x.room.RoomSector.Name : true) &&
                                   ((renovateFilter.EndDate > DateTime.MinValue.Date) ? x.DateEnd.Date <= renovateFilter.EndDate.Date : true) &&
                                   ((renovateFilter.StartDate > DateTime.MinValue.Date) ? x.DateStart.Date >= renovateFilter.StartDate.Date : true) &&
                                   ((renovateFilter.RoomNumber != -1) ? x.room.RoomNumber == renovateFilter.RoomNumber : true) &&
                                   ((renovateFilter.RoomType != -1) ? (int)x.room.RoomType == renovateFilter.RoomType : true));

        public Renovate GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Renovate> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);

        public IEnumerable<Renovate> GetFutureRenovationsForRoom(Room room)
                    => GetAll().Where(x => x.room.RoomNumber == room.RoomNumber && x.DateEnd.Date >= DateTime.Now.Date);

        public bool IsRenovating(Room room, DateTime date)
                    => GetAll().Where(x => x.room.RoomNumber == room.RoomNumber && x.DateEnd.Date >= date.Date && x.DateStart.Date <= date.Date).Any();

    }
}