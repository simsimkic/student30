// File:    ISymptomRepository.cs
// Created: Monday, May 25, 2020 2:15:00
// Purpose: Definition of Interface ISymptomRepository

using Model.Diagnosis;
using System;

namespace Repository.OtherDataRepository
{
   public interface ISymptomRepository : IGetAll<Symptom>
   {
   }
}