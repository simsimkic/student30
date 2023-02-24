/***********************************************************************
 * Module:  Patient.cs
 * Purpose: Definition of the Class Patient
 ***********************************************************************/

using Model.MedicalRecords;
using System;

namespace Model.Users
{
   public class Patient : User
   {
      private bool _guest;
      private Bloodtype _bloodType;
      private Rhfaktor _rhFactor;
      
      private MedicalRecord _medicalRecord;

        public MedicalRecord MedicalRecord
        {
            get { return _medicalRecord; }
            set
            {
                if (_medicalRecord != value)
                {
                    _medicalRecord = value;
                }
            }
        }
        public Rhfaktor RhFactor
        {
            get { return _rhFactor; }
            set
            {
                if (_rhFactor != value)
                {
                    _rhFactor = value;
                }
            }
        }

        public Bloodtype BloodType
        {
            get { return _bloodType; }
            set
            {
                if (_bloodType != value)
                {
                    _bloodType = value;
                }
            }
        }

        public bool Guest
        {
            get { return _guest; }
            set
            {
                if (_guest != value)
                {
                    _guest = value;
                }
            }
        }

    }
}