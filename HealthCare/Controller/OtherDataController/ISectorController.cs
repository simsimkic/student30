// File:    ISectorController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Interface ISectorController

using Model.Rooms;
using System;

namespace Controller.OtherDataController
{
   public interface ISectorController : IGetAll<Sector>
    {
   }
}