// File:    IUpdate.cs
// Created: Friday, May 22, 2020 22:09:54
// Purpose: Definition of Interface IUpdate

using System;

namespace Service
{
   public interface IUpdate<T>
   {
      T Update(T entity);
   
   }
}