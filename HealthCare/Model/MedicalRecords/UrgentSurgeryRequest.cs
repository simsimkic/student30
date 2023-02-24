/***********************************************************************
 * Module:  SpecialistInstruction.cs
 * Purpose: Definition of the Class SpecialistInstruction
 ***********************************************************************/

using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class UrgentSurgeryRequest : Refferal
   {
        private bool _approved = false;
        private string _note;
        private DateTime _date;
        private Patient _patient;
        private Doctor _doctor;

        private WorkPlace _specialization;

        public Doctor Doctor
        {
            get { return _doctor; }
            set
            {
                _doctor = value;
                OnPropertyChanged();
            }
        }

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
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

        public bool Approved
        {
            get { return _approved; }
            set { 
    
                _approved = value;
                OnPropertyChanged();
            }
        }
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public WorkPlace Specialization
        {
            get { return _specialization; }
            set
            {
                _specialization = value;
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