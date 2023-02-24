// File:    IIgredientService.cs
// Created: Tuesday, May 26, 2020 22:47:20
// Purpose: Definition of Interface IIgredientService

using Model.Drug;
using System;

namespace Service.OtherDataService
{
   public interface IIngredientService : IGetAll<Ingredient>
   {
   }
}