// File:    ISpecializationService.cs
// Created: Tuesday, May 26, 2020 22:47:21
// Purpose: Definition of Interface ISpecializationService

using Model.Users;
using System;

namespace Service.OtherDataService
{
   public interface ISpecializationService : IGetAll<Specialization>
   {
   }
}