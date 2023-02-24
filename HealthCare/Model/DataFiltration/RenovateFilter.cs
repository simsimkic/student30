// File:    RenovateFilter.cs
// Created: Friday, May 8, 2020 23:22:07
// Purpose: Definition of Class RenovateFilter

using System;

namespace Model.DataFiltration
{
   public class RenovateFilter
   {
      private DateTime startDate;
      private DateTime endDate;
      private int roomNumber;
      private Model.Rooms.Sector sector;
      private int roomType;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                }
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                }
            }
        }
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
        public Model.Rooms.Sector Sector
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
    }
}