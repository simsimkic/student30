/***********************************************************************
 * Module:  AbsenceService.cs
 * Purpose: Definition of the Class Service.AbsenceService
 ***********************************************************************/

using Model.Users;
using Service.UserService;
using System;
using System.Collections.Generic;

namespace Controller.UserControllers
{
   public class AbsenceController : IAbsenceController
   {
      public Service.UserService.IAbsenceService _service;

        public AbsenceController(IAbsenceService service)
        {
            _service = service;
        }
        public void ApproveAbsence(Absence absence) => _service.ApproveAbsence(absence);

        public Absence Create(Absence entity) => _service.Create(entity);

        public Absence Delete(Absence entity) => _service.Delete(entity);

        public IEnumerable<Absence> GetEmployeesAbsenceHistory(User staff) => _service.GetEmployeesAbsenceHistory(staff);
        public IEnumerable<Absence> GetEmployeesFutureAbsence(User staff) => _service.GetEmployeesFutureAbsence(staff);
        public IEnumerable<Absence> GetAll() => _service.GetAll();
        public Absence GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<Absence> GetNonApprovedAbsence() => _service.GetNonApprovedAbsence();

        public void RejectAbsence(Absence absence, string rejectRason) => _service.RejectAbsence(absence, rejectRason);

    }
}