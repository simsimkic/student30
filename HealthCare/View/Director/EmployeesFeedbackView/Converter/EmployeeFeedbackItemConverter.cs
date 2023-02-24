using HCIProjekat.View.EmployeesFeedbackView.DataView;
using Model.Communication;
using System;
using System.Collections.Generic;
using System.IO;

namespace HealthCare.View.Director.EmployeesFeedbackView.Converter
{
    class EmployeeFeedbackItemConverter : AbstractConverter
    {
        public static EmployeeFeedbackItem ConvertFeedBackToFeedBackView(FeedBack feedBack)
          => new EmployeeFeedbackItem
          {
              Id = feedBack.ForDoctor.Id,
              Expertise = (int)feedBack.Expertise + 1,
              Communication = (int)feedBack.Communication + 1,
              Kindness = (int)feedBack.Kindness + 1,
              Organization = (int)feedBack.Organization + 1,
              Average = feedBack.Average,
              EmployeeName = feedBack.ForDoctor.Name,
              Surname = feedBack.ForDoctor.Surname,
              WorkPlace = feedBack.ForDoctor.WorkPlace.Name,
              Comment = feedBack.Note,
              Picture = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, feedBack.ForDoctor.Picture),
              Date = feedBack.Date
          };

        public static IList<EmployeeFeedbackItem> ConvertFeedBackListToFeedBackViewList(IList<FeedBack> feedBacks)
            => ConvertEntityListToViewList(feedBacks, ConvertFeedBackToFeedBackView);
    }
}
