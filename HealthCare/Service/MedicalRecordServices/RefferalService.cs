/***********************************************************************
 * Module:  RefferalService.cs
 * Purpose: Definition of the Class Service.RefferalService
 ***********************************************************************/

using Model.MedicalRecords;
using Repository.MedicalRecordRepositories;
using Service.CommunicationServices;
using Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.MedicalRecordServices
{
   public class RefferalService : IRefferalService
   {
        private IRefferalRepository _repository;
        private INotificationService _notificationService;

        public RefferalService(IRefferalRepository repository, INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public bool CommitHospitalTreatment(HospitalTreatment hospitalTreatment)
        {
            HospitalTreatment treatment = _repository.GetAllHospitalTreatment().Single(i => i.Id == hospitalTreatment.Id);
            treatment.Completed = true;
            _repository.Update(treatment);

            return true;
        }

        public bool CommitUrgentSurgeryRequest(UrgentSurgeryRequest urgentSurgeryRequest)
        {
            UrgentSurgeryRequest surgeryRequest = _repository.GetUrgentSurgeryRequest().Single( i => i.Id == urgentSurgeryRequest.Id);
            surgeryRequest.Approved = true;
            _repository.Update(surgeryRequest);

            return true;
        }

        public Refferal Create(Refferal entity)
        {
            if (entity.GetType().Equals(typeof(HospitalTreatment)))
                _notificationService.CreateNotificationForCreateHospitalTreatmentRequest((HospitalTreatment)entity);
            else if (entity.GetType().Equals(typeof(UrgentSurgeryRequest)))
                _notificationService.CreateNotificationForCreateUrgentSurgeryRequest();

            return _repository.Create(entity);
        }

        public IEnumerable<Refferal> GetAllHospitalTreatment()
                                => _repository.GetAllHospitalTreatment();

        public IEnumerable<Refferal> GetAllUrgentSurgeryRequest()
                                => _repository.GetAllUrgentSurgeryRequest();

        public IEnumerable<Refferal> GetUncompletedHospitalTreatment()
                                => _repository.GetAllHospitalTreatment();

        public IEnumerable<UrgentSurgeryRequest> GetUrgentSurgeryRequest()
                                => _repository.GetUrgentSurgeryRequest();

        public Refferal Update(Refferal entity) 
                                => _repository.Update(entity);


        IEnumerable<Refferal> IRefferalService.GetAllRefferals()
                                => _repository.GetAllRefferals();
    }
}