// File:    IReportController.cs
// Created: Saturday, May 23, 2020 0:19:42
// Purpose: Definition of Interface IReportController

using System;

namespace Controller.ReportController
{
   public interface IReportController
   {
      Model.DataReport.PatientTreatmentReport GetReportForPatientTreatment(Model.Users.Patient patient, DateTime dateFrom, DateTime dateTo);

        Model.DataReport.DoctorOccupationReport GetReportForDoctorOccupation(Model.DataReport.DoctorOccupationReport report);      
   
   }
}