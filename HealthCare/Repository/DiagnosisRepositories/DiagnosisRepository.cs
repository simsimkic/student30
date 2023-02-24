/***********************************************************************
 * Module:  DiagnosisFileStorage.cs
 * Purpose: Definition of the Class DiagnosisFileStorage
 ***********************************************************************/

using Model.Diagnosis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Repository.DiagnosisRepositories
{
   public class DiagnosisRepository : IDiagnosisRepository
   {
      private JSONStream<Diagnosis> _stream;


        public DiagnosisRepository(JSONStream<Diagnosis> stream)
        {
            _stream = stream;
        }

        public Diagnosis Create(Diagnosis entity)
        {
            List<Diagnosis> diagnosis = _stream.GetAll().ToList<Diagnosis>();
            diagnosis.Add(entity);
            _stream.SaveAll(diagnosis);
            return entity;
 
        }

        public Diagnosis Delete(Diagnosis entity)
        {
            List<Diagnosis> diagnosis = _stream.GetAll().ToList<Diagnosis>();

            var diagnosisToRemove = diagnosis.SingleOrDefault(ent => ent.Code.CompareTo(entity.Code) == 0);

            if (diagnosisToRemove != null)
            {
                diagnosis.Remove(diagnosisToRemove);
                _stream.SaveAll(diagnosis);
            }

            return diagnosisToRemove;
        }

        public IEnumerable<Diagnosis> GetAll() => _stream.GetAll().ToList<Diagnosis>(); 
  

        public IEnumerable<Diagnosis> getFilteredDiagnosisByNameOrCode(string nameOrCode) 
                                        => _stream.GetAll().Where(x => x.Name == nameOrCode || x.Code == nameOrCode);

        public Diagnosis GetFromID(string id)
                                    => this.GetAll().SingleOrDefault(ent => ent.Code.CompareTo(id) == 0);

        public Diagnosis Update(Diagnosis entity)
        {
            List<Diagnosis> diagnosis = _stream.GetAll().ToList();

            diagnosis[diagnosis.FindIndex(ent => ent.Code.CompareTo(entity.Code) == 0)] = entity;
                
            _stream.SaveAll(diagnosis);

            return entity;
        }
    }
}