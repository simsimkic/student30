/***********************************************************************
 * Module:  HospitalThreatment.cs
 * Purpose: Definition of the Class HospitalThreatment
 ***********************************************************************/

using Model.Rooms;
using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.MedicalRecords
{
   public class HospitalTreatment : Refferal
   {
        private DateTime _date;
        private DateTime _dateTo;
        private string _note;
        private bool _completed = false;
        private Patient _patient;
        private Room _room;
        public Sector _sector;

        public Room Room
        {
            get { return _room; }
            set
            {
                _room = value;
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

        public DateTime DateTo
        {
            get { return _dateTo; }
            set
            {
                _dateTo = value;
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

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                OnPropertyChanged();
            }
        }


        public Sector Sector
        {
            get { return _sector; }
            set
            {
                _sector = value;
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