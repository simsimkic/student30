/***********************************************************************
 * Module:  Appointment.cs
 * Purpose: Definition of the Class Appointment
 ***********************************************************************/

using Model.MedicalRecords;
using System;

namespace Model.Appointment
{
    public class Appointment
    {
        private int _id;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private Model.MedicalRecords.TreatmentType _type;

        private Model.Rooms.Room _room;
        private Model.Users.Doctor _doctor;
        private Model.Users.Patient _patient;


        public Model.Users.Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
            }
        }

        public Model.Rooms.Room Room
        {
            get { return _room; }
            set
            {
                _room = value;
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }
        public TreatmentType Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set { _startDateTime = value; }
        }
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }


        public Model.Users.Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

    }
}