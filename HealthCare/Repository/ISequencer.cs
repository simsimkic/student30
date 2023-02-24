// File:    ISequencer.cs
// Created: Saturday, May 23, 2020 14:26:30
// Purpose: Definition of Interface ISequencer

using System;

namespace Repository
{
   public interface ISequencer<T>
   {
      void Initialize(T initID);
      
      T GenerateID();
   
   }
}