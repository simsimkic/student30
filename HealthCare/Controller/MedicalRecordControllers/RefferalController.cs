/***********************************************************************
 * Module:  RefferalService.cs
 * Purpose: Definition of the Class Service.RefferalService
 ***********************************************************************/

using Model.MedicalRecords;
using Service.MedicalRecordServices;
using System;
using System.Collections.Generic;

namespace Controller.MedicalRecordControllers
{
   public class RefferalController : IRefferalController
   {
        private IRefferalService _service;

        public RefferalController(IRefferalService service)
        {
            _service = service;
        }

        public bool CommitHospitalTreatment(HospitalTreatment hospitalTreatment)
                                    => _service.CommitHospitalTreatment(hospitalTreatment);


        public bool CommitUrgentSurgeryRequest(UrgentSurgeryRequest urgentSurgeryRequest)
                                    => _service.CommitUrgentSurgeryRequest(urgentSurgeryRequest);

        public Refferal Create(Refferal entity) => _service.Create(entity);

        public IEnumerable<Refferal> GetAllHospitalTreatment() 
                                    => _service.GetAllHospitalTreatment();


        public IEnumerable<Refferal> GetUncompletedHospitalTreatment()
                                    => _service.GetUncompletedHospitalTreatment();


        public IEnumerable<UrgentSurgeryRequest> GetUrgentSurgeryRequest()
                                    => _service.GetUrgentSurgeryRequest();

        public IEnumerable<Refferal> GetAllUrgentSurgeryRequest()
                                    => _service.GetAllUrgentSurgeryRequest();

        public Refferal Update(Refferal entity)
                                    => _service.Update(entity);

        IEnumerable<Refferal> IRefferalController.GetAllRefferals()
                                    => _service.GetAllRefferals();
    }
}