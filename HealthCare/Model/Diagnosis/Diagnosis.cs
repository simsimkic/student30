/***********************************************************************
 * Module:  Diagnosis.cs
 * Purpose: Definition of the Class Diagnosis
 ***********************************************************************/

using Model.Drug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Diagnosis
{
   public class Diagnosis
   {
        private DateTime _date;
        private string _code;
        private string _name;
        //treba da bude _theraphy koja ce se generisati na osnovu lekova koji su stavljeni
        private string _theraphy;

        private List<Symptom> _symptoms = new List<Symptom>();
        private List<Model.Drug.Drug> _theraphyDrugs= new List<Model.Drug.Drug>();

        private string _doctorCreated;

        public string DoctorCreated
        {
            get { return _doctorCreated; }
            set
            {
                _doctorCreated = value;
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

        public string Theraphy
        {
            get { return _theraphy; }
            set
            {
                _theraphy = value;
                OnPropertyChanged();
            }
        }
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged();
            }
        }

        public List<Symptom> Symptoms
        {
            get { return _symptoms; }
            set
            {
                _symptoms = value;
                OnPropertyChanged();
            }
        }

        public List<Model.Drug.Drug> TheraphyDrug
        {
            get { return _theraphyDrugs; }
            set
            {
                _theraphyDrugs = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
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