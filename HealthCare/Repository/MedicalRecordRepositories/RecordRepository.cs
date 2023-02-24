/***********************************************************************
 * Module:  RecordRepository.cs
 * Purpose: Definition of the Class Repository.RecordRepository
 ***********************************************************************/

using Model.MedicalRecords;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.MedicalRecordRepositories
{
   public class RecordRepository : IRecordRepository
   {
        private JSONStream<MedicalRecord> _stream;
        private IntSequencer _sequencer;

        public RecordRepository(JSONStream<MedicalRecord> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
        }

        public MedicalRecord Create(MedicalRecord entity)
        {
            entity.SetId(_sequencer.GenerateID());
            List<MedicalRecord> medicalRecords = _stream.GetAll().ToList();
            medicalRecords.Add(entity);
            _stream.SaveAll(medicalRecords);
            return entity;
        }

        public MedicalRecord GetRecordForPatient(int patientId)
                                => _stream.GetAll().SingleOrDefault(ent => ent.PatientID.CompareTo(patientId) == 0);


        public MedicalRecord Update(MedicalRecord entity)
        {
            List<MedicalRecord> medicalRecords = _stream.GetAll().ToList();

            medicalRecords[medicalRecords.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;

            _stream.SaveAll(medicalRecords);

            return entity;
        }

        protected void InitializeId() 
                                => _sequencer.Initialize(GetMaxId(_stream.GetAll()));
    
        private int GetMaxId(IEnumerable<MedicalRecord> entities)
                                => entities.Count() == 0 ? default : entities.Max(entity => entity.GetId());
    }
}