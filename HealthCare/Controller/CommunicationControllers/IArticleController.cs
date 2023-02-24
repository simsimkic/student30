// File:    IArticleController.cs
// Created: Saturday, May 23, 2020 0:07:52
// Purpose: Definition of Interface IArticleController

using Model.Communication;
using System;

namespace Controller.CommunicationControllers
{
   public interface IArticleController : ICreate<Article>, IDelete<Article>, IGetAll<Article>, IGet<Article,int>
   {
      bool HasPermissionToDelete(Model.Users.Staff staff, Model.Communication.Article article);
   
   }
}