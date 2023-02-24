// File:    SpecializationRepository.cs
// Created: Monday, May 25, 2020 2:06:30
// Purpose: Definition of Class SpecializationRepository

using Model.Users;
using System;
using System.Collections.Generic;

namespace Repository.OtherDataRepository
{
   public class SpecializationRepository : ISpecializationRepository
   {
      public JSONStream<Specialization> _stream;

        public SpecializationRepository(JSONStream<Specialization> stream)
        {
            _stream = stream;
        }
        public IEnumerable<Specialization> GetAll() => _stream.GetAll();
    }
}