/***********************************************************************
 * Module:  IGetAll.cs
 * Purpose: Definition of the Interface Service.IGetAll
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Controller
{
   public interface IGetAll<T>
   {
      IEnumerable<T> GetAll();
   
   }
}