// File:    IAbsenceRepository.cs
// Created: Saturday, May 23, 2020 17:25:11
// Purpose: Definition of Interface IAbsenceRepository

using Model.Users;
using System;
using System.Collections.Generic;

namespace Repository.UserRepositories
{
   public interface IAbsenceRepository : IRepository<Absence,int>
   {
      IEnumerable<Absence> GetNonApprovedAbsence();
      void DeleteAbsenceForStaff(User staff);
      IEnumerable<Absence> GetEmployeesAbsenceHistory(Model.Users.User staff);
      IEnumerable<Absence> GetEmployeesFutureAbsence(Model.Users.User staff);

    }
}