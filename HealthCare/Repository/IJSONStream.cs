// File:    IJSONStream.cs
// Created: Saturday, May 23, 2020 0:57:10
// Purpose: Definition of Interface IJSONStream

using System;
using System.Collections.Generic;

namespace Repository
{
   public interface IJSONStream<T> where T : class
   {
      IEnumerable<T> GetAll();
      
      void SaveAll(IEnumerable<T> entities);
   
   }
}