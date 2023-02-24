// File:    IDelete.cs
// Created: Friday, May 22, 2020 22:09:55
// Purpose: Definition of Interface IDelete

using System;

namespace Service
{
   public interface IDelete<T>
   {
      T Delete(T entity);
   
   }
}