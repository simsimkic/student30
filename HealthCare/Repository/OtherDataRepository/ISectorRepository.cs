// File:    ISectorRepository.cs
// Created: Monday, May 25, 2020 2:15:49
// Purpose: Definition of Interface ISectorRepository

using Model.Rooms;
using System;

namespace Repository.OtherDataRepository
{
   public interface ISectorRepository : IGetAll<Sector>
   {
   }
}