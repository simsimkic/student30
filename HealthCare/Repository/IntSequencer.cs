// File:    IntSequencer.cs
// Created: Saturday, May 23, 2020 14:28:00
// Purpose: Definition of Class IntSequencer

using System;

namespace Repository
{
   public class IntSequencer : ISequencer<int>
   {
        private int _nextId;

        public int GenerateID() => ++_nextId;

        public void Initialize(int initID) => _nextId = initID;
    }
}