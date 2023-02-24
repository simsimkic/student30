/***********************************************************************
 * Module:  SoftwareRatingService.cs
 * Purpose: Definition of the Class Model.Communication.SoftwareRatingService
 ***********************************************************************/

using Model.Communication;
using Repository.CommunicationRepositories;
using System;

namespace Service.CommunicationServices
{
    public class SoftwareRatingService : ISoftwareRatingService
    {
        public ISoftwareRatingRepository _repository;
        public SoftwareRatingService(ISoftwareRatingRepository repository)
        {
            _repository = repository;
        }
        public SoftwareRating Create(SoftwareRating entity) => _repository.Create(entity);
    }
}