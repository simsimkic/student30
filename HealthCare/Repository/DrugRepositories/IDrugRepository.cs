// File:    IDrugRepository.cs
// Created: Saturday, May 23, 2020 16:44:20
// Purpose: Definition of Interface IDrugRepository

using Model.Drug;
using Model.Users;
using System.Collections.Generic;

namespace Repository.DrugRepositories
{
    public interface IDrugRepository : IRepository<Drug, int>
    {
        IEnumerable<Drug> GetFilteredDrugs(Model.DataFiltration.DrugFilter drugFilter);

        IEnumerable<Drug> GetNonApprovedDrugs();
        IEnumerable<Drug> GetDrugByDoctorWhoApprovesIt(User doctor);

        IEnumerable<Drug> GetApprovedDrugs();
        IEnumerable<Drug> GetApprovedVaccine();


    }
}