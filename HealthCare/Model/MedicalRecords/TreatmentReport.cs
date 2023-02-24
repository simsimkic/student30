/***********************************************************************
 * Module:  IzvestajPregleda.cs
 * Purpose: Definition of the Class IzvestajPregleda
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class TreatmentReport
   {
      private string _anamnesis;
      private string _note;
      private string _diagnosis;
      private string _theraphy;
      private int _patientId;
      
      private Prescription _prescription;
      private List<Refferal> _refferal = new List<Refferal>();
      
        public int PatientId
        {
            get { return _patientId; }
            set
            {
                _patientId = value;
                OnPropertyChanged();
            }
        }

        public string Anamnesis
        {
            get { return _anamnesis; }
            set
            {
                _anamnesis = value;
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

        public string Theraphy
        {
            get { return _theraphy; }
            set
            {
                _theraphy = value;
                OnPropertyChanged();
            }
        }



        public Prescription Prescription
        {
            get { return _prescription; }
            set
            {
                _prescription = value;
                OnPropertyChanged();
            }
        }

        public List<Refferal> Refferal
        {
            get { return _refferal; }
            set
            {
                _refferal = value;
                OnPropertyChanged();
            }
        }

        public string Diagnosis
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