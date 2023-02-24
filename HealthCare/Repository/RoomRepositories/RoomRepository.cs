/***********************************************************************
 * Module:  RoomRepository.cs
 * Purpose: Definition of the Class Model.Rooms.RoomRepository
 ***********************************************************************/

using Model.DataFiltration;
using Model.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.RoomRepositories
{
   public class RoomRepository : IRoomRepository
   {
        public JSONStream<Room> _stream;
        public IntSequencer _sequencer;

        public RoomRepository(JSONStream<Room> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }

        public Room Create(Room entity)
        {
            entity.RoomNumber = _sequencer.GenerateID();
            List<Room> rooms = _stream.GetAll().ToList();
            rooms.Add(entity);
            _stream.SaveAll(rooms);
            return entity;
        }

        public Room Delete(Room entity)
        {
            List<Room> rooms = _stream.GetAll().ToList();
            var entityToRemove = rooms.SingleOrDefault(ent => ent.RoomNumber.CompareTo(entity.RoomNumber) == 0);
            if (entityToRemove != null)
            {
                rooms.Remove(entityToRemove);
                _stream.SaveAll(rooms);
            }

            return entityToRemove;
        }

        public IEnumerable<Room> GetAll() => _stream.GetAll();

        public IEnumerable<Room> GetFilteredRooms(RoomFilter roomFilter)
            => GetAll().Where(x => ((roomFilter.RoomNumber != -1) ? x.RoomNumber == roomFilter.RoomNumber : true) &&
                                   ((roomFilter.RoomSizeFrom > 0) ? x.RoomSize >= roomFilter.RoomSizeFrom : true) &&
                                   ((roomFilter.RoomSizeTo > 0) ? x.RoomSize <= roomFilter.RoomSizeTo : true) &&
                                   ((roomFilter.RoomType != -1) ? (int)x.RoomType == roomFilter.RoomType : true) &&
                                   ((roomFilter.Sector != null) ? x.RoomSector.Name.Equals(roomFilter.Sector.Name) : true) &&
                                   ((roomFilter.Floor != -1) ? x.Floor == roomFilter.Floor : true));

        public Room GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.RoomNumber == id);

        public Room Update(Room entity)
        {
            List<Room> rooms = _stream.GetAll().ToList();
            rooms[rooms.FindIndex(ent => ent.RoomNumber.CompareTo(entity.RoomNumber) == 0)] = entity;
            _stream.SaveAll(rooms);
            return entity;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));
        private int GetMaxId(IEnumerable<Room> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.RoomNumber);
    }
}