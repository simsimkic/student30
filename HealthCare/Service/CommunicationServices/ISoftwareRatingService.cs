// File:    ISoftwareRatingService.cs
// Created: Sunday, May 24, 2020 22:19:38
// Purpose: Definition of Interface ISoftwareRatingService

using Model.Communication;
using System;

namespace Service.CommunicationServices
{
   public interface ISoftwareRatingService : ICreate<SoftwareRating>
   {
   }
}