// File:    ISectorService.cs
// Created: Tuesday, May 26, 2020 22:47:21
// Purpose: Definition of Interface ISectorService

using Model.Rooms;
using System;

namespace Service.OtherDataService
{
   public interface ISectorService : IGetAll<Sector>
   {
   }
}