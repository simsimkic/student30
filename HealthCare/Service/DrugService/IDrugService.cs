// File:    IDrugService.cs
// Created: Friday, May 22, 2020 23:00:29
// Purpose: Definition of Interface IDrugService

using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using System.Collections.Generic;

namespace Service.DrugService
{
    public interface IDrugService : IService<Drug, int>
    {
        void DrugAccept(Model.Drug.Drug drug);

        void DrugReject(Model.Drug.Drug drug);

        IEnumerable<Drug> GetFilteredDrugs(Model.DataFiltration.DrugFilter drugFilter);
        IEnumerable<Drug> GetDrugByDoctorWhoApprovesIt(User doctor);

        IEnumerable<Drug> GetNonApprovedDrugs();

        IEnumerable<Drug> GetApprovedDrugs();

        void AddAmountOfDrug(Model.Drug.Drug drug, int amount);

        void ReduceAmountByPrescription(Prescription prescription);

        IEnumerable<Drug> GetApprovedVaccine();
    }
}