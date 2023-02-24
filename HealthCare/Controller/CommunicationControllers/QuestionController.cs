using HealthCare.Service.CommunicationServices;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Controller.CommunicationControllers
{
    class QuestionController : IQuestionController
    {

        private IQuestionService _service;

        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        public Question Create(Question entity) => _service.Create(entity);

        public IEnumerable<Question> GetAll() => _service.GetAll();

        public IEnumerable<Question> GetAnsweredQuestionForPatient(User user) => _service.GetAnsweredQuestionForPatient(user);

        public IEnumerable<Question> GetFAQ() => _service.GetFAQ();

        public IEnumerable<Question> GetQuestionForDoctor(User user) => _service.GetQuestionForDoctor(user);

        public Question Update(Question entity) => _service.Update(entity);

    }
}
