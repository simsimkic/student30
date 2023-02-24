/***********************************************************************
 * Module:  IUpdate.cs
 * Purpose: Definition of the Interface Service.IUpdate
 ***********************************************************************/

using System;

namespace Controller
{
   public interface IUpdate<T>
   {
      T Update(T entity);
   
   }
}