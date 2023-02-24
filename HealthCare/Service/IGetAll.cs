// File:    IGetAll.cs
// Created: Friday, May 22, 2020 22:09:55
// Purpose: Definition of Interface IGetAll

using System;
using System.Collections.Generic;

namespace Service
{
   public interface IGetAll<T>
   {
      IEnumerable<T> GetAll();
   
   }
}