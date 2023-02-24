/***********************************************************************
 * Module:  RiskAllergies.cs
 * Purpose: Definition of the Class RiskAllergies
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class Immunization
   {
        private DateTime _date;
        private GivingType _givingType;
        private VaccineType _vaccineType;
      
        public Model.Drug.Drug _vaccine;


        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }
        public GivingType GivingType
        {
            get { return _givingType; }
            set
            {
                _givingType = value;
                OnPropertyChanged();
            }
        }

        public VaccineType VacineType
        {
            get { return _vaccineType; }
            set
            {
                _vaccineType = value;
                OnPropertyChanged();
            }
        }

        public Model.Drug.Drug Vaccine
        {
            get { return _vaccine; }
            set
            {
                _vaccine = value;
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