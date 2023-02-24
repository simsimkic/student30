// File:    IReportService.cs
// Created: Friday, May 22, 2020 23:23:26
// Purpose: Definition of Interface IReportService

using System;

namespace Service.ReportService
{
   public interface IReportService
   {
      Model.DataReport.PatientTreatmentReport GetReportForPatientTreatment(Model.Users.Patient patient, DateTime dateFrom, DateTime dateTo);
      
      Model.DataReport.DoctorOccupationReport GetReportForDoctorOccupation(Model.DataReport.DoctorOccupationReport report);
               
   }
}