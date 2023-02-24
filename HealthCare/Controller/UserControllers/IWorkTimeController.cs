// File:    IWorkTimeController.cs
// Created: Saturday, May 23, 2020 0:27:44
// Purpose: Definition of Interface IWorkTimeController

using Model.Users;
using System;
using System.Collections.Generic;

namespace Controller.UserControllers
{
   public interface IWorkTimeController : ICreate<WorkTime>, IDelete<WorkTime>, IGet<WorkTime,int>
   {
      IEnumerable<WorkTime> GetWorkTime(Model.Users.User user);
        WorkTime UpdateWhileOverriding(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime);

        IEnumerable<WorkTime> GetFutureWorkTime();   
   }
}