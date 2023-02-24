/***********************************************************************
 * Module:  SoftwareRatingService.cs
 * Purpose: Definition of the Class Model.Communication.SoftwareRatingService
 ***********************************************************************/

using Model.Communication;
using Service.CommunicationServices;
using System;

namespace Controller.CommunicationControllers
{
    public class SoftwareRatingController : ISoftwareRatingController
    {
        public Service.CommunicationServices.ISoftwareRatingService _service;

        public SoftwareRatingController(ISoftwareRatingService service)
        {
            _service = service;
        }

        public SoftwareRating Create(SoftwareRating entity) => _service.Create(entity);
    }
}