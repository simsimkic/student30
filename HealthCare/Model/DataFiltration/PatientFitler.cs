// File:    PatientFitler.cs
// Created: Monday, May 11, 2020 21:29:07
// Purpose: Definition of Class PatientFitler

using System;

namespace Model.DataFiltration
{
   public class PatientFitler
   {
        private string _name;
        private string _surname;
        private int _recordNumber;
        private string _jmbg;
      
        public Model.Diagnosis.Diagnosis diagnosis;


        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }
        public String Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                }
            }
        }

        public String JMBG
        {
            get { return _jmbg; }
            set
            {
                if (_jmbg != value)
                {
                    _jmbg = value;
                }
            }
        }

        public int RecordNumber
        {
            get { return _recordNumber; }
            set
            {
                if (_recordNumber != value)
                {
                    _recordNumber = value;
                }
            }
        }
    }
}