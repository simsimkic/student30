// File:    ISymptomController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Interface ISymptomController

using Model.Diagnosis;
using System;

namespace Controller.OtherDataController
{
   public interface ISymptomController : IGetAll<Symptom>
    {
   }
}