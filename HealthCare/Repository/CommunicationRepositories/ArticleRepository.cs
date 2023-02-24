
/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using HealthCare;
using Model.Communication;
using Model.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;

namespace Repository.CommunicationRepositories
{
    public class ArticleRepository : IArticleRepository
    {
        public JSONStream<Article> _stream;
        public IntSequencer _sequencer;

        public ArticleRepository(JSONStream<Article> stream)
        {
            _stream = stream;
            _sequencer = new IntSequencer();
            InitializeId();
        }
        public Article Create(Article entity)
        {
            entity.Id = _sequencer.GenerateID();
            if (entity.Image != "")
                entity.Image = MoveBlogImageToProjectFolder(entity.Image, entity.Id.ToString());
            List<Article> lista = _stream.GetAll().ToList();
            lista.Add(entity);
            _stream.SaveAll(lista);
            return entity;
        }

        private string MoveBlogImageToProjectFolder(string image, string pictureName)
        {
            string newPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(@"../../Resources/BlogImages/", pictureName + @".png"));
            File.Copy(image, newPath, true);
            return @"../../Resources/BlogImages/" + pictureName + ".png";
        }

        public Article Delete(Article entity)
        {
            List<Article> articleList = _stream.GetAll().ToList();

            var entityToRemove = articleList.SingleOrDefault(ent => ent.Id.CompareTo(entity.Id) == 0);
            if (entityToRemove != null)
            {
                articleList.Remove(entityToRemove);
                DeleteBlogImage(entityToRemove);
                _stream.SaveAll(articleList);
            }

            return entity;
        }

        private void DeleteBlogImage(Article entity)
        {
            if (entity.Image != "")
            {
                File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entity.Image));
            }
        }

        public IEnumerable<Article> GetAll() => _stream.GetAll().ToList();

        public Article GetFromID(int id) => _stream.GetAll().SingleOrDefault(x => x.Id == id);

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.GetAll()));

        private int GetMaxId(IEnumerable<Article> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.Id);

    }
}