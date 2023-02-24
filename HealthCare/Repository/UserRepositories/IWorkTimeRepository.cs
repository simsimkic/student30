// File:    IWorkTimeRepository.cs
// Created: Saturday, May 23, 2020 17:30:20
// Purpose: Definition of Interface IWorkTimeRepository

using Model.Users;
using System;
using System.Collections.Generic;

namespace Repository.UserRepositories
{
   public interface IWorkTimeRepository : ICreate<WorkTime>, IUpdate<WorkTime>, IGetAll<WorkTime>, IDelete<WorkTime>, IGet<WorkTime, int>
    {
       IEnumerable<WorkTime> GetWorkTimes(User user);
      void DeleteWorkTimeForStaff(User staff);
      IEnumerable<WorkTime> GetFutureWorkTime();
   
   }
}