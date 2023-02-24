/***********************************************************************
 * Module:  ArticleService.cs
 * Purpose: Definition of the Class Model.Communication.ArticleService
 ***********************************************************************/

using Model.Communication;
using System;
using System.Collections.Generic;
using Repository.CommunicationRepositories;
using Model.Users;

namespace Service.CommunicationServices
{
    public class ArticleService : IArticleService
    {
        public Repository.CommunicationRepositories.IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public Article Create(Article entity) => _repository.Create(entity);

        public Article Delete(Article entity) => _repository.Delete(entity);

        public IEnumerable<Article> GetAll() => _repository.GetAll();


        public Article GetFromID(int id) => _repository.GetFromID(id);

        public bool HasPermissionToDelete(Staff staff, Article article)
            => (article.CreatedBy.Id == staff.Id) ? true : false;
    }
}