/***********************************************************************
 * Module:  IDelete.cs
 * Purpose: Definition of the Interface Service.IDelete
 ***********************************************************************/

using System;

namespace Repository
{
   public interface IDelete<T>
   {
      T Delete(T entity);
   
   }
}