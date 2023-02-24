// File:    ISymptomService.cs
// Created: Tuesday, May 26, 2020 22:47:24
// Purpose: Definition of Interface ISymptomService

using Model.Diagnosis;
using System;

namespace Service.OtherDataService
{
   public interface ISymptomService : IGetAll<Symptom>
   {
   }
}