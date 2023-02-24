/***********************************************************************
 * Module:  DiagnosisService.cs
 * Purpose: Definition of the Class Model.Diagnosis.DiagnosisService
 ***********************************************************************/

using Model.Diagnosis;
using Service.DiagnosisService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Controller.DiagnosisController
{
   public class DiagnosisController : IDiagnosisController
   {
        public IDiagnosisService _service;

        public DiagnosisController(IDiagnosisService service)
        {
            _service = service;
        }

        public Diagnosis Create(Diagnosis entity) => _service.Create(entity);

        public Diagnosis Delete(Diagnosis entity) => _service.Delete(entity);

        public IEnumerable<Diagnosis> GetAll() => _service.GetAll();
 
        public IEnumerable<Diagnosis> GetFilteredDiagnosisByNameOrCode(string nameOrCode) 
                                                  => _service.GetFilteredDiagnosisByNameOrCode(nameOrCode);

        public Diagnosis GetFromID(string id) 
                            => _service.GetFromID(id);


        public IEnumerable<Diagnosis> GetPotentialDiagnosis(List<Symptom> symptomes)
                                                  =>  _service.GetPotentialDiagnosis(symptomes);
         
        public Diagnosis Update(Diagnosis entity) => _service.Update(entity);

    }
}