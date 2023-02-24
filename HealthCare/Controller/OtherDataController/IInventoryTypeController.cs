// File:    IInventoryTypeController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Interface IInventoryTypeController

using Model.Rooms;
using System;

namespace Controller.OtherDataController
{
   public interface IInventoryTypeController : IGetAll<InventoryType>
    {
   }
}