using HealthCare.Repository.CommunicationRepositories;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Service.CommunicationServices
{
    class QuestionService : IQuestionService
    {

        private IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public Question Create(Question entity)
        {
            return _repository.Create(entity);
        }

        public IEnumerable<Question> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Question> GetAnsweredQuestionForPatient(User user)
        {
            return _repository.GetAnsweredQuestionForPatient(user);
        }

        public IEnumerable<Question> GetFAQ()
        {
            return _repository.GetFAQ();
        }

        public IEnumerable<Question> GetQuestionForDoctor(User user)
        {
            return _repository.GetQuestionForDoctor(user);
        }

        public Question Update(Question entity)
        {
            return _repository.Update(entity);
        }
    }
}
