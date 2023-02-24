/***********************************************************************
 * Module:  RiskAllergies.cs
 * Purpose: Definition of the Class RiskAllergies
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class RiskAllergies
   {
        private DateTime _date;
        private MedicalStatus _medicalStatus;

        private Model.Drug.Allergen _allergen;

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


        public Model.Drug.Allergen Allergen
        {
            get { return _allergen; }
            set
            {
                _allergen = value;
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