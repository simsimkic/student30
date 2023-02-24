/***********************************************************************
 * Module:  Prescription.cs
 * Purpose: Definition of the Class MedicalRecords.Prescription
 ***********************************************************************/

using Model.Drug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class Prescription
   {
      private bool _reserved;
      private DateTime _reservedFrom;
      private DateTime _reservedTo;
      
      private List<Model.Drug.Drug> _theraphy = new List<Model.Drug.Drug>();


        public bool Reserved
        {
            get { return _reserved; }
            set
            {
                _reserved = value;
                OnPropertyChanged();
            }
        }

        public DateTime ReservedFrom
        {
            get { return _reservedFrom; }
            set
            {
                _reservedFrom = value;
                OnPropertyChanged();
            }
        }



        public DateTime ReservedTo
        {
            get { return _reservedTo; }
            set
            {
                _reservedTo = value;
                OnPropertyChanged();
            }
        }

        public List<Model.Drug.Drug> Theraphy
        {
            get { return _theraphy; }
            set
            {
                _theraphy = value;
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