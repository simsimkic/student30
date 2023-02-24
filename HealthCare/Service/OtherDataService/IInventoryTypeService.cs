// File:    IInventoryTypeService.cs
// Created: Tuesday, May 26, 2020 22:47:20
// Purpose: Definition of Interface IInventoryTypeService

using Model.Rooms;
using System;

namespace Service.OtherDataService
{
   public interface IInventoryTypeService : IGetAll<InventoryType>
   {
   }
}