// File:    IArticleRepository.cs
// Created: Sunday, May 24, 2020 22:46:22
// Purpose: Definition of Interface IArticleRepository

using Model.Communication;
using System;

namespace Repository.CommunicationRepositories
{
   public interface IArticleRepository : IGetAll<Article>, IDelete<Article>, ICreate<Article>, IGet<Article, int>
   {
   }
}