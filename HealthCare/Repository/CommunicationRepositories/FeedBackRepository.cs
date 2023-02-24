/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using Model.Communication;
using Model.DataFiltration;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.CommunicationRepositories
{
    public class FeedBackRepository : IFeedBackRepository
    {
        public JSONStream<FeedBack> _stream;

        public FeedBackRepository(JSONStream<FeedBack> stream)
        {
            _stream = stream;
        }


        public FeedBack Create(FeedBack entity)
        {
            List<FeedBack> feedbackList = _stream.GetAll().ToList();
            feedbackList.Add(entity);
            _stream.SaveAll(feedbackList);
            return entity;
        }

        public IEnumerable<FeedBack> GetFeedBackFiltered(FeedBackFilter feedBackFilter)
         => GetAll().Where(x => ((feedBackFilter.Id != -1) ? feedBackFilter.Id == x.ForDoctor.Id : true) &&
                                ((feedBackFilter.AverageGradeFrom != 0) ? feedBackFilter.AverageGradeFrom <= x.Average : true) &&
                                ((feedBackFilter.AverageGradeTo != 0) ? feedBackFilter.AverageGradeTo >= x.Average : true));

        public IEnumerable<FeedBack> GetFeedbackForDoctor(Doctor forDoctor) => _stream.GetAll().Where(x => x.ForDoctor.Id == forDoctor.Id);

        public IEnumerable<FeedBack> GetAll() => _stream.GetAll().ToList();
    }
}