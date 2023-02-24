// File:    IIgredientController.cs
// Created: Tuesday, May 26, 2020 22:59:44
// Purpose: Definition of Interface IIgredientController

using Model.Drug;
using System;

namespace Controller.OtherDataController
{
   public interface IIngredientController : IGetAll<Ingredient>
    {
   }
}