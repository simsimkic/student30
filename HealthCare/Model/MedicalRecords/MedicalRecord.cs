/***********************************************************************
 * Module:  Karton.cs
 * Purpose: Definition of the Class Karton
 ***********************************************************************/

using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class MedicalRecord
   {
        private int _id;
        private string _note;

        private List<MedicalHistory> _medicalHistory = new List<MedicalHistory>();
        private List<RiskFactor> _riskFactor = new List<RiskFactor>();
        private List<FamilyRiskFactor> _familyRiskFactor = new List<FamilyRiskFactor>();
        private List<RiskAllergies> _riskAllergies = new List<RiskAllergies>();
        private List<Immunization> _immunization = new List<Immunization>();
        private List<Treatment> _treatment = new List<Treatment>();
        private int _patientId;

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public List<MedicalHistory> MedicalHistory
        {
            get { return _medicalHistory; }
            set
            {
                _medicalHistory = value;
                OnPropertyChanged();
            }
        }
        
        public List<RiskFactor> RiskFactor
        {
            get { return _riskFactor; }
            set
            {
                _riskFactor = value;
                OnPropertyChanged();
            }
        }

        public List<FamilyRiskFactor> FamilyRiskFactor
        {
            get { return _familyRiskFactor; }
            set
            {
                _familyRiskFactor = value;
                OnPropertyChanged();
            }
        }

        public List<RiskAllergies> RiskAllergies
        {
            get { return _riskAllergies; }
            set
            {
                _riskAllergies = value;
                OnPropertyChanged();
            }
        }

        public List<Immunization> Immunization
        {
            get { return _immunization; }
            set
            {
                _immunization = value;
                OnPropertyChanged();
            }
        }

        public List<Treatment> Treatment
        {
            get { return _treatment; }
            set
            {
                _treatment = value;
                OnPropertyChanged();
            }
        }

        public int PatientID
        {
            get { return _patientId; }
            set
            {
                _patientId = value;
                OnPropertyChanged();
            }
        }

        public int GetId() => Id;

        public void SetId(int id) => Id = id;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}