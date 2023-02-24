/***********************************************************************
 * Module:  Renovate.cs
 * Purpose: Definition of the Class Renovate
 ***********************************************************************/

using System;

namespace Model.Rooms
{
   public class Renovate
   {
      private int id;
      private DateTime dateStart;
      private DateTime dateEnd;
      private string note;
      
      public Room room;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }

        public DateTime DateStart
        {
            get { return dateStart; }
            set
            {
                if (dateStart != value)
                {
                    dateStart = value;
                }
            }
        }

        public DateTime DateEnd
        {
            get { return dateEnd; }
            set
            {
                if (dateEnd != value)
                {
                    dateEnd = value;
                }
            }
        }
        public string Note
        {
            get { return note; }
            set
            {
                if (note != value)
                {
                    note = value;
                }
            }
        }
    }
}