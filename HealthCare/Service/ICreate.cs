// File:    ICreate.cs
// Created: Friday, May 22, 2020 22:09:54
// Purpose: Definition of Interface ICreate

using System;

namespace Service
{
   public interface ICreate<T>
   {
      T Create(T entity);
   
   }
}