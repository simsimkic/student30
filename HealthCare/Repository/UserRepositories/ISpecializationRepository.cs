// File:    ISpecializationRepository.cs
// Created: Saturday, May 23, 2020 17:28:03
// Purpose: Definition of Interface ISpecializationRepository

using Model.Users;
using System;

namespace Repository.UserRepositories
{
   public interface ISpecializationRepository : IGetAll<Specialization>
   {
   }
}