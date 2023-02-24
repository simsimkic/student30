/***********************************************************************
 * Module:  Drug.cs
 * Purpose: Definition of the Class Drug
 ***********************************************************************/

using Model.Drug;
using Model.Users;
using System;
using System.Collections.Generic;

namespace Model.Drug
{
   public class Drug
   {
        private int id;
        private string name;
        private string purpose;
        private double quantity;
        private string sideEffect;
        private Formatdrug format;
        private string instruction;
        private int amount;
        private string manufacturer;
        private DrugStatus status;
        private String rejectReason;


        public User whoApprovesDrug;

        public List<Allergen> allergens;
        public List<Ingredient> ingredients;


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

        public string Purpose
        {
            get { return purpose; }
            set
            {
                if (purpose != value)
                {
                    purpose = value;
                }
            }
        }

        public double Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                }
            }
        }

        public string SideEffect
        {
            get { return sideEffect; }
            set
            {
                if (sideEffect != value)
                {
                    sideEffect = value;
                }
            }
        }

        public Formatdrug Format
        {
            get { return format; }
            set
            {
                if (format != value)
                {
                    format = value;
                }
            }
        }

        public string Instruction
        {
            get { return instruction; }
            set
            {
                if (instruction != value)
                {
                    instruction = value;
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

        public DrugStatus Status
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

        public string RejectReason
        {
            get { return rejectReason; }
            set
            {
                if (rejectReason != value)
                {
                    rejectReason = value;
                }
            }
        }


    }
}