// File:    IAbsenceController.cs
// Created: Saturday, May 23, 2020 0:26:57
// Purpose: Definition of Interface IAbsenceController

using Model.Users;
using System;
using System.Collections.Generic;

namespace Controller.UserControllers
{
   public interface IAbsenceController : ICreate<Absence>, IDelete<Absence>, IGet<Absence,int>, IGetAll<Absence>
   {
      void ApproveAbsence(Model.Users.Absence absence);
      
      IEnumerable<Absence> GetNonApprovedAbsence();
      
      void RejectAbsence(Model.Users.Absence absence, string rejectRason);
      
      IEnumerable<Absence> GetEmployeesAbsenceHistory(Model.Users.User staff);
      IEnumerable<Absence> GetEmployeesFutureAbsence(Model.Users.User staff);

    }
}