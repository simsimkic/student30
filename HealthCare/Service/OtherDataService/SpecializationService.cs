// File:    SpecializationService.cs
// Created: Tuesday, May 26, 2020 22:44:27
// Purpose: Definition of Class SpecializationService

using Model.Users;
using Repository.OtherDataRepository;
using System;
using System.Collections.Generic;

namespace Service.OtherDataService
{
    public class SpecializationService : ISpecializationService
    {
        public Repository.OtherDataRepository.ISpecializationRepository _specializationRepository;

        public SpecializationService(ISpecializationRepository repository)
        {
            _specializationRepository = repository;
        }
        public IEnumerable<Specialization> GetAll() => _specializationRepository.GetAll();
    }
}