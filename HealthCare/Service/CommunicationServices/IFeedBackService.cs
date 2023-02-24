// File:    IFeedBackService.cs
// Created: Friday, May 22, 2020 22:50:51
// Purpose: Definition of Interface IFeedBackService

using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Service.CommunicationServices
{
   public interface IFeedBackService : ICreate<FeedBack>, IGetAll<FeedBack>
   {
      IEnumerable<FeedBack> GetFeedbackForDoctor(Doctor forDoctor);
      
      IEnumerable<FeedBack> GetFeedBackFiltered(Model.DataFiltration.FeedBackFilter feedBackFilter);
   
   }
}