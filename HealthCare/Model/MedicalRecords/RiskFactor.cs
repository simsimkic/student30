/***********************************************************************
 * Module:  RiskFactor.cs
 * Purpose: Definition of the Class RiskFactor
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class RiskFactor
   {
        private DateTime _date;
        private MedicalStatus _medicalStatus;

        private Model.Diagnosis.Diagnosis _riskFactor;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public MedicalStatus MedicalStatus
        {
            get { return _medicalStatus; }
            set
            {
                _medicalStatus = value;
                OnPropertyChanged();
            }
        }

        public Model.Diagnosis.Diagnosis riskFactor
        {
            get { return _riskFactor; }
            set
            {
                _riskFactor = value;
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