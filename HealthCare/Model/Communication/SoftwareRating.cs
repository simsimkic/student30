// File:    SoftwareRating.cs
// Created: Thursday, April 16, 2020 20:45:38
// Purpose: Definition of Class SoftwareRating

using System;

namespace Model.Communication
{
   public class SoftwareRating
   {
      private Grades functionality;
      private Grades speed;
      private Grades reliabillity;
      private Grades design;
      private string note;

        public string Note
        {
            get { return note; }
            set
            {
                note = value;
            }

        }

        public Grades Design
        {
            get { return design; }
            set
            {
                design = value;
            }

        }
        public Grades Reliabillity
        {
            get { return reliabillity; }
            set
            {
                reliabillity = value;
            }

        }
        public Grades Speed
        {
            get { return speed; }
            set
            {
                speed = value;
            }

        }

        public Grades Functionality
        {
            get { return functionality; }
            set
            {
                functionality = value;
            }

        }

    }
}