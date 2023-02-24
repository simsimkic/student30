// File:    InventoryAmount.cs
// Created: Thursday, April 16, 2020 2:14:48
// Purpose: Definition of Class InventoryAmount

using System;

namespace Model.Rooms
{
   public class InventoryAmount
   {
      private int amount;
      private int usage = 0;
      
      public Inventory inventory;
      public Room room;
   
        public Inventory Inventory
        {
            get { return inventory; }
            set
            {
                if (inventory != value)
                {
                    inventory = value;
                }
            }
        }

        public int Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                }
            }
        }
        public int Usage
        {
            get { return usage; }
            set
            {
                if (usage != value)
                {
                    usage = value;
                }
            }
        }
    }
}