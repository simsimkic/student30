/***********************************************************************
 * Module:  WorkTimeService.cs
 * Purpose: Definition of the Class Service.WorkTimeService
 ***********************************************************************/

using Model.Users;
using Service.UserService;
using System;
using System.Collections.Generic;

namespace Controller.UserControllers
{
   public class WorkTimeController : IWorkTimeController
   {
      public Service.UserService.IWorkTimeService _service;

        public WorkTimeController(IWorkTimeService service)
        {
            _service = service;
        }

        public WorkTime Create(WorkTime entity) => _service.Create(entity);

        public WorkTime Delete(WorkTime entity) => _service.Delete(entity);

        public WorkTime GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<WorkTime> GetFutureWorkTime() => _service.GetFutureWorkTime();

        public IEnumerable<WorkTime> GetWorkTime(User user) => _service.GetWorkTime(user);

        public WorkTime UpdateWhileOverriding(IEnumerable<WorkTime> oldWorkTimes, WorkTime newWorkTime) => _service.UpdateWorkTimeWhileOverridingExisting(oldWorkTimes, newWorkTime);
    }
}