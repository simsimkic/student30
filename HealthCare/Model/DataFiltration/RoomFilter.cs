// File:    RoomFilter.cs
// Created: Friday, May 8, 2020 23:05:02
// Purpose: Definition of Class RoomFilter

using Model.Rooms;
using System;

namespace Model.DataFiltration
{
   public class RoomFilter
   {
      private int roomType;
      private int roomNumber;
      private int floor;
      private Sector sector;
      private double roomSizeFrom;
      private double roomSizeTo;

        public int RoomType
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
        public double RoomSizeTo
        {
            get { return roomSizeTo; }
            set
            {
                if (roomSizeTo != value)
                {
                    roomSizeTo = value;
                }
            }
        }
        public double RoomSizeFrom
        {
            get { return roomSizeFrom; }
            set
            {
                if (roomSizeFrom != value)
                {
                    roomSizeFrom = value;
                }
            }
        }
        public Sector Sector
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
    }
}