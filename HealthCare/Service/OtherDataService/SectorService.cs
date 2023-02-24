// File:    SectorService.cs
// Created: Tuesday, May 26, 2020 22:45:05
// Purpose: Definition of Class SectorService

using Model.Rooms;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;

namespace Service.OtherDataService
{
   public class SectorService : ISectorService
   {
      public Repository.OtherDataRepository.ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository repository)
        {
            _sectorRepository = repository;
        }

        public IEnumerable<Sector> GetAll() => _sectorRepository.GetAll();
    }
}