// File:    IAbsenceService.cs
// Created: Friday, May 22, 2020 23:37:53
// Purpose: Definition of Interface IAbsenceService

using Model.Users;
using System;
using System.Collections.Generic;

namespace Service.UserService
{
   public interface IAbsenceService : IService<Absence,int>
   {
      void ApproveAbsence(Model.Users.Absence absence);

      IEnumerable<Absence> GetNonApprovedAbsence();
      
      void RejectAbsence(Model.Users.Absence absence, string rejectRason);
      
      IEnumerable<Absence> GetEmployeesAbsenceHistory(Model.Users.User staff);
      IEnumerable<Absence> GetEmployeesFutureAbsence(Model.Users.User staff);
    }
}