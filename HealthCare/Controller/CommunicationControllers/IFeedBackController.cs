// File:    IFeedBackController.cs
// Created: Saturday, May 23, 2020 0:05:52
// Purpose: Definition of Interface IFeedBackController

using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Controller.CommunicationControllers
{
   public interface IFeedBackController : ICreate<FeedBack>, IGetAll<FeedBack>
   {
      IEnumerable<FeedBack> GetFeedbackForDoctor(Doctor forDoctor);
      
      IEnumerable<FeedBack> GetFeedBackFiltered(Model.DataFiltration.FeedBackFilter feedBackFilter);
   
   }
}