// File:    ISpecializationController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Interface ISpecializationController

using Model.Users;
using System;

namespace Controller.OtherDataController
{
   public interface ISpecializationController : IGetAll<Specialization>
    {
   }
}