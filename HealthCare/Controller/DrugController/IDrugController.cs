// File:    IDrugController.cs
// Created: Saturday, May 23, 2020 0:11:03
// Purpose: Definition of Interface IDrugController

using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using System.Collections.Generic;

namespace Controller.DrugController
{
    public interface IDrugController : IController<Drug, int>
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