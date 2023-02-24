/***********************************************************************
 * Module:  FeedBackService.cs
 * Purpose: Definition of the Class Model.Communication.FeedBackService
 ***********************************************************************/

using Model.Communication;
using Model.DataFiltration;
using Model.Users;
using Service.CommunicationServices;
using System;
using System.Collections.Generic;

namespace Controller.CommunicationControllers
{
    public class FeedBackController : IFeedBackController
    {
        public Service.CommunicationServices.IFeedBackService _service;

        public FeedBackController(IFeedBackService service)
        {
            _service = service;
        }

        public FeedBack Create(FeedBack entity) => _service.Create(entity);

        public IEnumerable<FeedBack> GetAll() => _service.GetAll();

        public IEnumerable<FeedBack> GetFeedBackFiltered(FeedBackFilter feedBackFilter) => _service.GetFeedBackFiltered(feedBackFilter);


        public IEnumerable<FeedBack> GetFeedbackForDoctor(Doctor forDoctor) => _service.GetFeedbackForDoctor(forDoctor);

    }
}