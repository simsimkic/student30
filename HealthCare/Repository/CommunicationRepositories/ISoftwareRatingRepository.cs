// File:    ISoftwareRatingRepository.cs
// Created: Saturday, May 23, 2020 16:29:17
// Purpose: Definition of Interface ISoftwareRatingRepository

using Model.Communication;
using System;

namespace Repository.CommunicationRepositories
{
   public interface ISoftwareRatingRepository : ICreate<SoftwareRating>
   {
   }
}