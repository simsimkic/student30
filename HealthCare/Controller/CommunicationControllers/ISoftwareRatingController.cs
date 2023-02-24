// File:    ISoftwareRatingController.cs
// Created: Tuesday, May 26, 2020 22:16:54
// Purpose: Definition of Interface ISoftwareRatingController

using Model.Communication;
using System;

namespace Controller.CommunicationControllers
{
   public interface ISoftwareRatingController : ICreate<SoftwareRating>
   {
   }
}