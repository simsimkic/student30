using Model.Communication;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Repository.CommunicationRepositories
{
    class QuestionRepository : IQuestionRepository
    {
        public JSONStream<Question> _stream;
        public IntSequencer _sequencer;

        public QuestionRepository(JSONStream<Question> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }
        public Question Create(Question entity)
        {
            entity.Id = _sequencer.GenerateID();
            List<Question> lista = _stream.GetAll().ToList();
            lista.Add(entity);
            _stream.SaveAll(lista);
            return entity;
        }

        public Question Update(Question entity)
        {

            List<Question> questions = _stream.GetAll().ToList();
            questions[questions.FindIndex(ent => ent.Id.CompareTo(entity.Id) == 0)] = entity;
            _stream.SaveAll(questions);
            return entity;
        }

        public IEnumerable<Question> GetAll() 
                        => _stream.GetAll().ToList();

        public IEnumerable<Question> GetAnsweredQuestionForPatient(User user)
                        => GetAll().Where(x => x.Patient.Id == user.Id && x.IsAnswered == true);


        public IEnumerable<Question> GetQuestionForDoctor(User user)
                        => GetAll().Where(x => x.Doctor.Id == user.Id);

        public IEnumerable<Question> GetFAQ()
                        => GetAll().Where(x => x.IsFAQ == true);


        protected void InitializeId() 
                        => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Question> entities)
                        => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);
    }
}
