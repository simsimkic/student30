// File:    IFeedBackRepository.cs
// Created: Sunday, May 24, 2020 22:48:16
// Purpose: Definition of Interface IFeedBackRepository

using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Repository.CommunicationRepositories
{
   public interface IFeedBackRepository : ICreate<FeedBack>
   {
      IEnumerable<FeedBack> GetFeedbackForDoctor(Doctor forDoctor);
      
      IEnumerable<FeedBack> GetFeedBackFiltered(Model.DataFiltration.FeedBackFilter feedBackFilter);
        IEnumerable<FeedBack> GetAll();
    }
}