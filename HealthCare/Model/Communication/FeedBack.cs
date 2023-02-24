// File:    FeedBack.cs
// Created: Thursday, April 16, 2020 20:50:35
// Purpose: Definition of Class FeedBack

using Model.Users;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model.Communication
{
   public class FeedBack
   {
        private Grades kindness;
        private Grades expertise;
        private Grades communication;
        private Grades organization;
        private double average;
        private string note;
        private DateTime date;
        private string patientFullName = "Anonimno";

        public Model.Users.Doctor ForDoctor;

        public string PatientFullName
        {
            get { return patientFullName; }
            set
            {
                patientFullName = value;
            }
        }

        public Grades Kindness
        {
            get { return kindness; }
            set
            {
                kindness = value;
            }

        }

        public Grades Expertise
        {
            get { return expertise; }
            set
            {
                expertise = value;
            }

        }

        public Grades Communication
        {
            get { return communication; }
            set
            {
                communication = value;
            }

        }
        public Grades Organization
        {
            get { return organization; }
            set
            {
                organization = value;
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
            }
        }

        public string Note
        {
            get { return note; }
            set
            {
                note = value;
            }
        }

        public double Average
        {
            get { return average; }
            set
            {
                average = value;
            }
        }

    }
}