// File:    IIgredientRepository.cs
// Created: Monday, May 25, 2020 2:16:31
// Purpose: Definition of Interface IIgredientRepository

using Model.Drug;
using System;

namespace Repository.OtherDataRepository
{
   public interface IIngredientRepository : IGetAll<Ingredient>
   {
   }
}