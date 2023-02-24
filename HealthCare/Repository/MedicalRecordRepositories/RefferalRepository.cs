/***********************************************************************
 * Module:  RefferalRepository.cs
 * Purpose: Definition of the Class Repository.RefferalRepository
 ***********************************************************************/

using Model.Communication;
using Model.MedicalRecords;
using Service.CommunicationServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.MedicalRecordRepositories
{
   public class RefferalRepository : IRefferalRepository
   {
        private JSONStream<Refferal> _stream;
        private IntSequencer _sequencer;

        public RefferalRepository(JSONStream<Refferal> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }
        public Refferal Create(Refferal entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Refferal> lista = _stream.GetAll().ToList();
            lista.Add(entity);
            _stream.SaveAll(lista);
            return entity;
        }

        public IEnumerable<Refferal> GetAllRefferals() => _stream.GetAll();


        public IEnumerable<HospitalTreatment> GetUncompletedHospitalTreatment() 
                                => GetAllHospitalTreatment().Where(i => i.Completed == false);


        public IEnumerable<HospitalTreatment> GetAllHospitalTreatment()
                                => (IEnumerable<HospitalTreatment>)_stream.GetAll().Where(i => i.GetType().Equals(typeof(HospitalTreatment)));

        public IEnumerable<UrgentSurgeryRequest> GetUrgentSurgeryRequest()
                                => (IEnumerable<UrgentSurgeryRequest>)_stream.GetAll().Where(i => i.GetType().Equals(typeof(UrgentSurgeryRequest)));


        public Refferal Update(Refferal entity)
        {
            List<Refferal> refferalList = _stream.GetAll().ToList();
            refferalList[refferalList.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(refferalList);
            return entity;
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Refferal> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);

        public IEnumerable<Refferal> GetAllUrgentSurgeryRequest() => _stream.GetAll();
    }
}