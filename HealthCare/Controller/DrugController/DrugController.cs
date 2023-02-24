/***********************************************************************
 * Module:  DrugService.cs
 * Purpose: Definition of the Class Model.Drug.DrugService
 ***********************************************************************/

using Model.DataFiltration;
using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using Service.DrugService;
using System;
using System.Collections.Generic;

namespace Controller.DrugController
{
   public class DrugController : IDrugController
   {
      public Service.DrugService.IDrugService _service;

        public DrugController(IDrugService service)
        {
            _service = service;
        }
        public void AddAmountOfDrug(Drug drug, int amount) => _service.AddAmountOfDrug(drug, amount);

        public Drug Create(Drug entity) => _service.Create(entity);

        public Drug Delete(Drug entity) => _service.Delete(entity);

        public void DrugAccept(Drug drug) => _service.DrugAccept(drug);

        public void DrugReject(Drug drug) => _service.DrugReject(drug);

        public IEnumerable<Drug> GetAll() => _service.GetAll();
        public IEnumerable<Drug> GetApprovedDrugs() => _service.GetApprovedDrugs();

        public IEnumerable<Drug> GetApprovedVaccine() => _service.GetApprovedVaccine();

        public IEnumerable<Drug> GetDrugByDoctorWhoApprovesIt(User doctor) => _service.GetDrugByDoctorWhoApprovesIt(doctor);

        public IEnumerable<Drug> GetFilteredDrugs(DrugFilter drugFilter) => _service.GetFilteredDrugs(drugFilter);

        public Drug GetFromID(int id) => _service.GetFromID(id);

        public IEnumerable<Drug> GetNonApprovedDrugs() => _service.GetNonApprovedDrugs();

        public void ReduceAmountByPrescription(Prescription prescription) => _service.ReduceAmountByPrescription(prescription);

        public Drug Update(Drug entity) => _service.Update(entity);
    }
}