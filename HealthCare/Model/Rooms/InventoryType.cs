/***********************************************************************
 * Module:  InventoryType.cs
 * Purpose: Definition of the Class InventoryType
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model.Rooms
{
   public class InventoryType
   {
      private string name;
      
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

    }
}