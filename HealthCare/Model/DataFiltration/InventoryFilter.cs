// File:    InventoryFilter.cs
// Created: Friday, May 8, 2020 23:53:59
// Purpose: Definition of Class InventoryFilter

using Model.Rooms;
using System;

namespace Model.DataFiltration
{
   public class InventoryFilter
   {
      private string name;
      private InventoryType inventoryType;
      private int amountFrom;
      private int amountTo;

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

        public int AmountFrom
        {
            get { return amountFrom; }
            set
            {
                if (amountFrom != value)
                {
                    amountFrom = value;
                }
            }
        }

        public int AmountTo
        {
            get { return amountTo; }
            set
            {
                if (amountTo != value)
                {
                    amountTo = value;
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