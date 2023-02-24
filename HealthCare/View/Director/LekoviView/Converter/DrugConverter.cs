using Model.Drug;
using HCIProjekat.View.LekoviView.DataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.View.Director.LekoviView.Converter
{
    class DrugConverter : AbstractConverter
    {
        public static DrugItem ConvertDrugToDrugView(Drug drug)
            => new DrugItem
            {
                Id = drug.Id,
                Manufacturer = drug.Manufacturer,
                DrugName = drug.Name,
                DrugQuantity = drug.Quantity,
                Format = drug.Format,
                Instruction = drug.Instruction,
                Amount = drug.Amount,
                Status = drug.Status,
                Purpose = drug.Purpose,
                SideEffect = drug.SideEffect,
                Ingredients = drug.ingredients,
                Allergens = drug.allergens,
                RejectReason = drug.RejectReason
            };

        public static IList<DrugItem> ConvertDrugListToDrugViewList(IList<Drug> drugs)
            => ConvertEntityListToViewList(drugs, ConvertDrugToDrugView);

    }
}
