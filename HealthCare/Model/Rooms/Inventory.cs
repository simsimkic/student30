/***********************************************************************
 * Module:  Inventory.cs
 * Purpose: Definition of the Class Inventory
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model.Rooms
{
   public class Inventory
   {
      private int id; 
      private string name;
      private int quantityStorage = 0;
      private int quantityHospital = 0;
      
      public InventoryType inventoryType;

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

        public int QuantityStorage
        {
            get { return quantityStorage; }
            set
            {
                if (quantityStorage != value)
                {
                    quantityStorage = value;
                }
            }
        }

        public int QuantityHospital
        {
            get { return quantityHospital; }
            set
            {
                if (quantityHospital != value)
                {
                    quantityHospital = value;
                }
            }
        }

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

        public InventoryType InventoryType
        {
            get { return inventoryType; }
            set
            {
                if (inventoryType != value)
                {
                    inventoryType = value;
                }
            }
        }
    }
}