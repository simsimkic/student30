/***********************************************************************
 * Module:  ICreate.cs
 * Purpose: Definition of the Interface Service.ICreate
 ***********************************************************************/

using System;

namespace Repository
{
   public interface ICreate<T>
   {
      T Create(T entity);
   
   }
}