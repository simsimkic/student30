// File:    DrugFilter.cs
// Created: Friday, May 8, 2020 23:25:32
// Purpose: Definition of Class DrugFilter

using Model.Drug;
using System;

namespace Model.DataFiltration
{
   public class DrugFilter
   {
      private string name;
      private int status;
      private string manufacturer;
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
        public int Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
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
        public string Manufacturer
        {
            get { return manufacturer; }
            set
            {
                if (manufacturer != value)
                {
                    manufacturer = value;
                }
            }
        }

    }
}