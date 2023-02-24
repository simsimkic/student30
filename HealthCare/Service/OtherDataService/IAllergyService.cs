// File:    IAllergyService.cs
// Created: Tuesday, May 26, 2020 22:47:02
// Purpose: Definition of Interface IAllergyService

using Model.Drug;
using System;

namespace Service.OtherDataService
{
   public interface IAllergyService : IGetAll<Allergen>
   {
   }
}