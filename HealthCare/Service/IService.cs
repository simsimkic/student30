// File:    IService.cs
// Created: Friday, May 22, 2020 22:07:16
// Purpose: Definition of Interface IService

using System;

namespace Service
{
   public interface IService<T,ID> : ICreate<T>, IUpdate<T>, IDelete<T>, IGetAll<T>, IGet<T,ID> where T : class
    where ID : IComparable
   {
   }
}