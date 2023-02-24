/***********************************************************************
 * Module:  Treatment.cs
 * Purpose: Definition of the Class Treatment
 ***********************************************************************/

using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class Treatment
   {
        private int _id;
        private DateTime _date;
        private TreatmentType _type;

        private TreatmentReport _treatmentReport;
        private String _creator;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public TreatmentType TreatmentType
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public TreatmentReport TreatmentReport
        {
            get { return _treatmentReport; }
            set
            {
                _treatmentReport = value;
                OnPropertyChanged();
            }
        }

        public String Creator
        {
            get { return _creator; }
            set
            {
                _creator = value;
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