// File:    SectorRepository.cs
// Created: Monday, May 25, 2020 2:06:22
// Purpose: Definition of Class SectorRepository

using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Repository.OtherDataRepository
{
   public class SectorRepository : ISectorRepository
   {
      public JSONStream<Sector> _stream;

        public SectorRepository(JSONStream<Sector> stream)
        {
            _stream = stream;
        }

        public IEnumerable<Sector> GetAll() => _stream.GetAll();
    }
}