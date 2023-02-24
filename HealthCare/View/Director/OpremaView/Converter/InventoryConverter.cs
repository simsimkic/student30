using HCIProjekat.View.OpremaView.DataView;
using Model.Rooms;
using System.Collections.Generic;

namespace HealthCare.View.Director.OpremaView.Converter
{
    class InventoryConverter : AbstractConverter
    {
        public static EquipmentItem ConvertInventoryToInventoryView(Inventory inventory)
            => new EquipmentItem
            {
                Id = inventory.Id,
                EquipmentName = inventory.Name,
                EquipmentType = inventory.InventoryType,
                AmountInStorage = inventory.QuantityStorage,
                AmountInHospital = inventory.QuantityHospital
            };

        public static IList<EquipmentItem> ConvertInventoryListToInventoryViewList(IList<Inventory> inventories)
            => ConvertEntityListToViewList(inventories, ConvertInventoryToInventoryView);

    }
}
