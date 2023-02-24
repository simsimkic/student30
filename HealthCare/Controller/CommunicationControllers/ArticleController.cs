/***********************************************************************
 * Module:  ArticleService.cs
 * Purpose: Definition of the Class Model.Communication.ArticleService
 ***********************************************************************/

using Model.Communication;
using Model.Users;
using System;
using Service.CommunicationServices;
using System.Collections.Generic;

namespace Controller.CommunicationControllers
{
    public class ArticleController : IArticleController
    {
        public Service.CommunicationServices.IArticleService _service;

        public ArticleController(IArticleService service)
        {
            _service = service;
        }


        public Article Create(Article entity) => _service.Create(entity);

        public Article Delete(Article entity) => _service.Delete(entity);

        public IEnumerable<Article> GetAll() => _service.GetAll();

        public Article GetFromID(int id) => _service.GetFromID(id);

        public bool HasPermissionToDelete(Staff staff, Article article) => _service.HasPermissionToDelete(staff, article);

    }
}