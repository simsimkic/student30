/***********************************************************************
 * Module:  WorkTimeRepository.cs
 * Purpose: Definition of the Class Repository.WorkTimeRepository
 ***********************************************************************/

using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.UserRepositories
{
   public class WorkTimeRepository : IWorkTimeRepository
   {
      public JSONStream<WorkTime> _stream;
        public IntSequencer _sequencer;

        public WorkTimeRepository(JSONStream<WorkTime> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }
        public WorkTime Create(WorkTime entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<WorkTime> workTimes = _stream.GetAll().ToList();
            workTimes.Add(entity);
            _stream.SaveAll(workTimes);
            return entity;
        }

        public void DeleteWorkTimeForStaff(User staff)
        {
            List<WorkTime> workTimes = _stream.GetAll().ToList();
            workTimes.RemoveAll(x => x.staff.Id == staff.Id);
            _stream.SaveAll(workTimes);
        }
        public WorkTime Delete(WorkTime entity)
        {
            List<WorkTime> workTimes = _stream.GetAll().ToList();
            var entityToRemove = workTimes.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                workTimes.Remove(entityToRemove);
                _stream.SaveAll(workTimes);
            }

            return entityToRemove;
        }

        public IEnumerable<WorkTime> GetAll() => _stream.GetAll();

        public WorkTime GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);

        public IEnumerable<WorkTime> GetFutureWorkTime() => _stream.GetAll().Where(x => x.StartDate >= DateTime.Now.Date);

        public IEnumerable<WorkTime> GetWorkTimes(User user) => _stream.GetAll().Where(x => x.staff.Id == user.Id);

        public WorkTime Update(WorkTime entity)
        {
            List<WorkTime> workTimes = _stream.GetAll().ToList();
            workTimes[workTimes.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(workTimes);
            return entity;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<WorkTime> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);
    }
}