/***********************************************************************
 * Module:  Rooms.cs
 * Purpose: Definition of the Class Rooms
 ***********************************************************************/

using Model.Users;
using System;
using System.Collections.Generic;

namespace Model.Rooms
{
   public class Room
   {
      private Roomtype roomType;
      private int roomNumber;
      private double roomSize;
      private int floor;
      
      public Sector sector;
      public List<Patient> patients;

        public int RoomNumber
        {
            get { return roomNumber; }
            set
            {
                if (roomNumber != value)
                {
                    roomNumber = value;
                }
            }
        }

        public int Floor
        {
            get { return floor; }
            set
            {
                if (floor != value)
                {
                    floor = value;
                }
            }
        }

        public Roomtype RoomType
        {
            get { return roomType; }
            set
            {
                if (roomType != value)
                {
                    roomType = value;
                }
            }
        }

        public Sector RoomSector
        {
            get { return sector; }
            set
            {
                if (sector != value)
                {
                    sector = value;
                }
            }
        }

        public double RoomSize
        {
            get { return roomSize; }
            set
            {
                if (roomSize != value)
                {
                    roomSize = value;
                }
            }
        }

    }
}