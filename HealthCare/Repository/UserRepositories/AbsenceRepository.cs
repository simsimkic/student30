/***********************************************************************
 * Module:  AbsenceRepository.cs
 * Purpose: Definition of the Class Repository.AbsenceRepository
 ***********************************************************************/

using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.UserRepositories
{
   public class AbsenceRepository : IAbsenceRepository
   {
      public JSONStream<Absence> _stream;
        public IntSequencer _sequencer;

        public AbsenceRepository(JSONStream<Absence> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }
        public Absence Create(Absence entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Absence> absences = _stream.GetAll().ToList();
            absences.Add(entity);
            _stream.SaveAll(absences);
            return entity;
        }

        public void DeleteAbsenceForStaff(User staff)
        {
            List<Absence> absences = _stream.GetAll().ToList();
            absences.RemoveAll(x => x.staff.Id == staff.Id);
            _stream.SaveAll(absences);
        }

        public Absence Delete(Absence entity)
        {
            List<Absence> absences = _stream.GetAll().ToList();
            var entityToRemove = absences.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                absences.Remove(entityToRemove);
                _stream.SaveAll(absences);
            }

            return entityToRemove;
        }

        public IEnumerable<Absence> GetAll() => _stream.GetAll();

        public IEnumerable<Absence> GetEmployeesAbsenceHistory(User staff) => GetAll().Where(x => staff.Id == x.staff.Id && x.Approved && x.EndDate <= DateTime.Now.Date);

        public IEnumerable<Absence> GetEmployeesFutureAbsence(User staff) => GetAll().Where(x => staff.Id == x.staff.Id && x.Approved && x.StartDate >= DateTime.Now.Date);

        public Absence GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);

        public IEnumerable<Absence> GetNonApprovedAbsence() => GetAll().Where(x => x.Approved == false);

        public Absence Update(Absence entity)
        {
            List<Absence> absences = _stream.GetAll().ToList();
            absences[absences.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(absences);
            return entity;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Absence> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);
    }
}