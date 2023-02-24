/***********************************************************************
 * Module:  RiskFactor.cs
 * Purpose: Definition of the Class RiskFactor
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class FamilyRiskFactor
   {
        private DateTime _date;
        private Relationship _relationship;
      
        private Model.Diagnosis.Diagnosis _familyRiskFactor;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public Relationship Relationship
        {
            get { return _relationship; }
            set
            {
                _relationship = value;
                OnPropertyChanged();
            }
        }

        public Model.Diagnosis.Diagnosis familyRiskFactor
        {
            get { return _familyRiskFactor; }
            set
            {
                _familyRiskFactor = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}