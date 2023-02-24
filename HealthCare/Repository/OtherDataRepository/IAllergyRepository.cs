// File:    IAllergyRepository.cs
// Created: Monday, May 25, 2020 2:16:16
// Purpose: Definition of Interface IAllergyRepository

using Model.Drug;
using System;

namespace Repository.OtherDataRepository
{
   public interface IAllergyRepository : IGetAll<Allergen>
   {
   }
}