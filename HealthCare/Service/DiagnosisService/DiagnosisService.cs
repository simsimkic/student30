/***********************************************************************
 * Module:  DiagnosisService.cs
 * Purpose: Definition of the Class Model.Diagnosis.DiagnosisService
 ***********************************************************************/

using HealthCare;
using Model.Diagnosis;
using Repository.DiagnosisRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.DiagnosisService
{
   public class DiagnosisService : IDiagnosisService
   {
      public IDiagnosisRepository _repository;

        public DiagnosisService(IDiagnosisRepository repository)
        {
            _repository = repository;
        }

        public Diagnosis Create(Diagnosis entity)   => IsDiagnosisUnique(entity) ? _repository.Create(entity) : null;
        
        private bool IsDiagnosisUnique(Diagnosis diagnosis) 
                            => GetAll().Where(x => x.Code == diagnosis.Code).Count()==0;

        public Diagnosis Delete(Diagnosis entity) => _repository.Delete(entity);

        public IEnumerable<Diagnosis> GetAll() => _repository.GetAll();

        public IEnumerable<Diagnosis> GetPotentialDiagnosis(List<Symptom> symptomes)
        {
            Dictionary<Diagnosis, double> diagnosisMap = GetPercentOfMatching(symptomes);

            return GetListOfPotentialDiagnosis(diagnosisMap);
        }

        private Dictionary<Diagnosis, double> GetPercentOfMatching(List<Symptom> symptomes)
        {
            Dictionary<Diagnosis, double> diagnosisMap = new Dictionary<Diagnosis, double>();

            foreach (Diagnosis diagnosis in this.GetAll())
                diagnosisMap[diagnosis] = GetPercentForDiagnosis(diagnosis, symptomes);

            return diagnosisMap;
        }

        private double GetPercentForDiagnosis(Diagnosis diagnosis, List<Symptom> symptomes)
        {
            double numberOfRepetition = 0;

            foreach (Symptom diagnosisSymptom in diagnosis.Symptoms)
                foreach (Symptom patientSymptom in symptomes)
                    if (patientSymptom.Name.Equals(diagnosisSymptom.Name))
                        numberOfRepetition++;

            return (numberOfRepetition / symptomes.Count()) * (numberOfRepetition / diagnosis.Symptoms.Count()) * 100;
        }

        private IEnumerable<Diagnosis> GetListOfPotentialDiagnosis(Dictionary<Diagnosis, double> diagnosisMap)
        {
            var diagnosisMapToList = diagnosisMap.ToList();

            diagnosisMapToList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            var returnDiagnosisList = new List<Diagnosis>();

            diagnosisMapToList.ForEach(x => { if (x.Value > App.PercentOfMatchingForPotentialDiagnosis) returnDiagnosisList.Add(x.Key); });

            return returnDiagnosisList;
        }

        public IEnumerable<Diagnosis> GetFilteredDiagnosisByNameOrCode(string nameOrCode)
                                                     => _repository.getFilteredDiagnosisByNameOrCode(nameOrCode);


        public Diagnosis Update(Diagnosis entity) => _repository.Update(entity);


        public Diagnosis GetFromID(string id) => _repository.GetFromID(id);

    }
}