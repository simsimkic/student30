// File:    ISpecializationRepository.cs
// Created: Monday, May 25, 2020 2:15:54
// Purpose: Definition of Interface ISpecializationRepository

using Model.Users;
using System;

namespace Repository.OtherDataRepository
{
   public interface ISpecializationRepository : IGetAll<Specialization>
   {
   }
}