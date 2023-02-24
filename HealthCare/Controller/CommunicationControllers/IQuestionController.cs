using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Controller.CommunicationControllers
{
    public interface IQuestionController
    {
        Question Create(Question entity);
        Question Update(Question entity);
        IEnumerable<Question> GetAll();
        IEnumerable<Question> GetAnsweredQuestionForPatient(User user);
        IEnumerable<Question> GetQuestionForDoctor(User user);
        IEnumerable<Question> GetFAQ();
    }
}
