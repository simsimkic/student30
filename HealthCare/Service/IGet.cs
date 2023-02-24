// File:    IGet.cs
// Created: Friday, May 22, 2020 22:20:34
// Purpose: Definition of Interface IGet

using System;

namespace Service
{
   public interface IGet<T,ID>
   {
      T GetFromID(ID id);
   
   }
}