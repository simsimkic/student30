// File:    IArticleService.cs
// Created: Friday, May 22, 2020 22:53:41
// Purpose: Definition of Interface IArticleService

using Model.Communication;
using Model.Users;
using System;

namespace Service.CommunicationServices
{
    public interface IArticleService : IGetAll<Article>, IDelete<Article>, ICreate<Article>, IGet<Article,int>
    {
        bool HasPermissionToDelete(Staff staff, Article article);
    }
}