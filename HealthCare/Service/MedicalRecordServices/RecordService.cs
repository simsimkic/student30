/***********************************************************************
 * Module:  RecordService.cs
 * Purpose: Definition of the Class Service.RecordService
 ***********************************************************************/

using Model.Drug;
using Model.MedicalRecords;
using Model.Users;
using Repository.MedicalRecordRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Service.MedicalRecordServices
{
   public class RecordService : IRecordService
   {
        private IRecordRepository _repository;

        public RecordService(IRecordRepository repository)
        {
            _repository = repository;
        }
        public MedicalRecord Create(MedicalRecord entity) => _repository.Create(entity);

        public MedicalRecord GetRecordForPatient(int patientId) => _repository.GetRecordForPatient(patientId);


        public MedicalRecord Update(MedicalRecord entity) => _repository.Update(entity);

        public bool IsPatientAllergic(MedicalRecord entity,List<Drug> drugsForDiagnosis)
        {
            List<Allergen> allergyFromDrugs = AllergiesFromDrugs(drugsForDiagnosis);

            foreach (RiskAllergies patientRiskAllergen in entity.RiskAllergies)
                foreach (Allergen allergenFromDrug in allergyFromDrugs)
                    if (patientRiskAllergen.Allergen.Name.Equals(allergenFromDrug.Name))
                        return true;

            return false;
        }

        private List<Allergen> AllergiesFromDrugs(List<Drug> drugs)
        {
            List<Allergen> allergiesFromDrugs = new List<Allergen>();

            foreach(Drug drug in drugs)
                allergiesFromDrugs.AddRange(drug.allergens);

            return allergiesFromDrugs;
        }
    }
}