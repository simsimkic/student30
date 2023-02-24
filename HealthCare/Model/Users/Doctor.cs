/***********************************************************************
 * Module:  Doctor.cs
 * Purpose: Definition of the Class Doctor
 ***********************************************************************/

using Model.Users;
using Model.Rooms;
using System;
using System.Collections.Generic;

namespace Model.Users
{
   public class Doctor : Staff
   {
      private string biography;
      
      private List<Specialization> specialization;
      private Room workRoom;
      private WorkPlace workPlace;


        public List<Specialization> Specialization
        {
            get { return specialization; }
            set
            {
                if (specialization != value)
                {
                    specialization = value;
                }
            }
        }

        public WorkPlace WorkPlace
        {
            get { return workPlace; }
            set
            {
                if (workPlace != value)
                {
                    workPlace = value;
                }
            }
        }
        public Room WorkRoom
        {
            get { return workRoom; }
            set
            {
                if (workRoom != value)
                {
                    workRoom = value;
                }
            }
        }

        public string Biography
        {
            get { return biography; }
            set
            {
                if (biography != value)
                {
                    biography = value;
                }
            }
        }
    }
}