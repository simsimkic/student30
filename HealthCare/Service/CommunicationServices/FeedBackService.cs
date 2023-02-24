/***********************************************************************
 * Module:  FeedBackService.cs
 * Purpose: Definition of the Class Model.Communication.FeedBackService
 ***********************************************************************/

using Model.Communication;
using Model.DataFiltration;
using Model.Users;
using Repository;
using Repository.CommunicationRepositories;
using System;
using System.Collections.Generic;

namespace Service.CommunicationServices
{
    public class FeedBackService : IFeedBackService
    {
        public Repository.CommunicationRepositories.IFeedBackRepository _repository;

        public FeedBackService(IFeedBackRepository repository)
        {
            _repository = repository;
        }

        public FeedBack Create(FeedBack entity) => _repository.Create(entity);

        public IEnumerable<FeedBack> GetAll() => _repository.GetAll();

        public IEnumerable<FeedBack> GetFeedBackFiltered(FeedBackFilter feedBackFilter) => _repository.GetFeedBackFiltered(feedBackFilter);

        public IEnumerable<FeedBack> GetFeedbackForDoctor(Doctor forDoctor) => _repository.GetFeedbackForDoctor(forDoctor);
    }
}