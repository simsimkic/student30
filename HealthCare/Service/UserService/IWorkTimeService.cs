// File:    IWorkTimeService.cs
// Created: Friday, May 22, 2020 23:40:12
// Purpose: Definition of Interface IWorkTimeService

using HealthCare.Util;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Service.UserService
{
   public interface IWorkTimeService : ICreate<WorkTime>, IDelete<WorkTime>, IGet<WorkTime,int>
   {
       IEnumerable<WorkTime> GetWorkTime(User user);
      
      IEnumerable<WorkTime> GetFutureWorkTime();
      EmployeeWorkDay GetEmployeeWorkDay(User staff, DateTime date);

        WorkTime UpdateWorkTimeWhileOverridingExisting(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime);

    }
}