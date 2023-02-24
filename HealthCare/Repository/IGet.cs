/***********************************************************************
 * Module:  IGet.cs
 * Purpose: Definition of the Interface Service.IGet
 ***********************************************************************/

using System;

namespace Repository
{
   public interface IGet<T,ID>
   {
      T GetFromID(ID id);
   
   }
}