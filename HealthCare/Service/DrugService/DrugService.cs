/***********************************************************************
 * Module:  DrugService.cs
 * Purpose: Definition of the Class Model.Drug.DrugService
 ***********************************************************************/

using HealthCare;
using Model.Communication;
using Model.DataFiltration;
using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using Repository.CommunicationRepositories;
using Repository.DrugRepositories;
using Repository.UserRepositories;
using Service.CommunicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Service.DrugService
{
    public class DrugService : IDrugService
    {
        public Repository.DrugRepositories.IDrugRepository _repository;
        private INotificationService _notificationService;
        private IUserRepository _userRepository;
        public DrugService(IDrugRepository repository, INotificationService notificationService, IUserRepository userRepository)
        {
            _repository = repository;
            _notificationService = notificationService;
            _userRepository = userRepository;
        }
        public void AddAmountOfDrug(Drug drug, int amount)
        {
            drug.Amount += amount;
            _repository.Update(drug);
        }


        public Drug Create(Drug entity)
        {
            if(IsDrugUnique(entity)){
                _notificationService.CreateNotificationForCreatingDrug(entity);
                return _repository.Create(entity);
            }
            return null;
        }
        public void ReduceAmountByPrescription(Prescription prescription)
        {
            Drug currentDrug;

            foreach(Drug drug in prescription.Theraphy)
            {
                if((currentDrug = GetFromID(drug.Id)).Amount>0)
                    AddAmountOfDrug(currentDrug, -1);

                if (currentDrug.Amount < App.MinDrugAmountForNotification)
                    _notificationService.CreateNotificationForSmallAmount(currentDrug);
            }
        }

        public Drug Delete(Drug entity) => _repository.Delete(entity);

        public void DrugAccept(Drug drug)
        {
            drug.Status = DrugStatus.Approved;
            _repository.Update(drug);
            _notificationService.CreateNotificationForApprovedDrug(drug);
        }

        public void DrugReject(Drug drug)
        {
            drug.Status = DrugStatus.Rejected;
            _repository.Update(drug);
            _notificationService.CreateNotificationForRejectDrug(drug);
        }

        private bool IsDrugUnique(Drug drug)
            => GetAll().Where(x => x.Name == drug.Name && x.Manufacturer == drug.Manufacturer &&
                                   x.Quantity == drug.Quantity && 
                                   x.Format == drug.Format && x.Id != drug.Id).Count() == 0;

        public IEnumerable<Drug> GetAll() => _repository.GetAll();

        public IEnumerable<Drug> GetApprovedDrugs() => _repository.GetApprovedDrugs();

        public IEnumerable<Drug> GetFilteredDrugs(DrugFilter drugFilter) => _repository.GetFilteredDrugs(drugFilter);

        public IEnumerable<Drug> GetNonApprovedDrugs() => _repository.GetNonApprovedDrugs();

        public Drug GetFromID(int id) => _repository.GetFromID(id);

        public Drug Update(Drug entity)
        {
            if(IsDrugUnique(entity)){
                _notificationService.CreateNotificationForCreatingDrug(entity);
                return _repository.Update(entity);
            }
            return null;
        }
        public IEnumerable<Drug> GetApprovedVaccine() => _repository.GetApprovedVaccine();

        public IEnumerable<Drug> GetDrugByDoctorWhoApprovesIt(User doctor) => _repository.GetDrugByDoctorWhoApprovesIt(doctor);
    }
}