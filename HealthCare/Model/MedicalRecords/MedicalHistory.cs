/***********************************************************************
 * Module:  MedicalHistory.cs
 * Purpose: Definition of the Class MedicalHistory
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class MedicalHistory
   {
        private DateTime _date;
        private MedicalStatus _medicalStatus;

        private Model.Diagnosis.Diagnosis _diagnosis;

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

        public Model.Diagnosis.Diagnosis Diagnosis
        {
            get { return _diagnosis; }
            set
            {
                _diagnosis = value;
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