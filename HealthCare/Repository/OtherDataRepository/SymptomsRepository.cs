// File:    SymptomsRepository.cs
// Created: Monday, May 25, 2020 2:06:51
// Purpose: Definition of Class SymptomsRepository

using Model.Diagnosis;
using System;
using System.Collections.Generic;

namespace Repository.OtherDataRepository
{
   public class SymptomsRepository : ISymptomRepository
   {
      public JSONStream<Symptom> _stream;

        public SymptomsRepository(JSONStream<Symptom> stream)
        {
            _stream = stream;
        }

        public IEnumerable<Symptom> GetAll() => _stream.GetAll();

    }
}