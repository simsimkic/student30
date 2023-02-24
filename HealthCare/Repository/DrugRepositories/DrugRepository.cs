/***********************************************************************
 * Module:  DrugFileStorage.cs
 * Purpose: Definition of the Class DrugFileStorage
 ***********************************************************************/

using Model.DataFiltration;
using Model.Drug;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.DrugRepositories
{
   public class DrugRepository : IDrugRepository
   {
      public JSONStream<Drug> _stream;
      public IntSequencer _sequencer;

        public DrugRepository(JSONStream<Drug> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }
        public Drug Create(Drug entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Drug> drugs = _stream.GetAll().ToList();
            drugs.Add(entity);
            _stream.SaveAll(drugs);
            return entity;
        }

        public Drug Delete(Drug entity)
        {
            List<Drug> drugs = _stream.GetAll().ToList();
            var entityToRemove = drugs.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                drugs.Remove(entityToRemove);
                _stream.SaveAll(drugs);
            }

            return entityToRemove;
        }

        public IEnumerable<Drug> GetAll() => _stream.GetAll();

        public IEnumerable<Drug> GetApprovedDrugs() 
                            => _stream.GetAll().Where(x => (int)x.Status == (int)DrugStatus.Approved);
        public IEnumerable<Drug> GetFilteredDrugs(DrugFilter drugFilter)
         => _stream.GetAll().Where(x => ((drugFilter.Name != "") ? x.Name == drugFilter.Name : true) &&
                                        ((drugFilter.Manufacturer != "") ? x.Manufacturer == drugFilter.Manufacturer : true) &&
                                        ((drugFilter.AmountFrom > 0) ? x.Amount >= drugFilter.AmountFrom : true) &&
                                        ((drugFilter.AmountTo > 0) ? x.Amount <= drugFilter.AmountTo : true) &&
                                        ((drugFilter.Status != -1) ? (int)x.Status == drugFilter.Status : true));
        public Drug GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);

        public IEnumerable<Drug> GetNonApprovedDrugs() 
                            => _stream.GetAll().Where(x => (int)x.Status == (int)DrugStatus.Waiting);

        public IEnumerable<Drug> GetApprovedVaccine() 
                            => _stream.GetAll().Where(x => (int)x.Status == (int)DrugStatus.Approved && x.Format==Formatdrug.Vaccine);
        
        public Drug Update(Drug entity)
        {
            List<Drug> drugs = _stream.GetAll().ToList();
            drugs[drugs.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(drugs);
            return entity;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Drug> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);

        public IEnumerable<Drug> GetDrugByDoctorWhoApprovesIt(User doctor)
            => GetAll().Where(x => x.whoApprovesDrug.Id == doctor.Id);
    }
}