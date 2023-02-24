/***********************************************************************
 * Module:  IService.cs
 * Purpose: Definition of the Interface Service.IService
 ***********************************************************************/

using System;

namespace Repository
{
   public interface IRepository<T,ID> : ICreate<T>, IUpdate<T>, IDelete<T>, IGetAll<T>, IGet<T,ID>
   {
   }
}