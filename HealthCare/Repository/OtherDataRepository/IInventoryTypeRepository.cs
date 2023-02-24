// File:    IInventoryTypeRepository.cs
// Created: Monday, May 25, 2020 2:16:32
// Purpose: Definition of Interface IInventoryTypeRepository

using Model.Rooms;
using System;

namespace Repository.OtherDataRepository
{
   public interface IInventoryTypeRepository : IGetAll<InventoryType>
   {
   }
}