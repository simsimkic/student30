/***********************************************************************
 * Module:  ReportService.cs
 * Purpose: Definition of the Class Service.ReportService
 ***********************************************************************/

using Model.DataReport;
using Model.Users;
using Service.ReportService;
using System;

namespace Controller.ReportController
{
   public class ReportController : IReportController
   {
      public Service.ReportService.IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        public DoctorOccupationReport GetReportForDoctorOccupation(DoctorOccupationReport report) => _service.GetReportForDoctorOccupation(report);

        public PatientTreatmentReport GetReportForPatientTreatment(Patient patient, DateTime dateFrom, DateTime dateTo) => _service.GetReportForPatientTreatment(patient,dateFrom, dateTo);

    }
}