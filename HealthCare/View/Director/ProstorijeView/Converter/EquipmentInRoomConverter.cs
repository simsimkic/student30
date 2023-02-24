using HCIProjekat.View.ProstorijeView.DataView;
using Model.Rooms;
using System.Collections.Generic;

namespace HealthCare.View.Director.ProstorijeView.Converter
{
    class EquipmentInRoomConverter : AbstractConverter
    {
        public static EquipmentInRoomItem ConvertInventoryAmountToEquipmentInRoomView(InventoryAmount inventoryAmount)
            => new EquipmentInRoomItem
            {
                EquipmentAmount = inventoryAmount.Amount,
                EquipmentName = inventoryAmount.inventory.Name,
                EquipmentType = inventoryAmount.inventory.InventoryType,
                Id = inventoryAmount.inventory.Id
            };

        public static IList<EquipmentInRoomItem> ConvertInventoryAmountListToEquipmentInRoomViewList(IList<InventoryAmount> inventoryAmount)
            => ConvertEntityListToViewList(inventoryAmount, ConvertInventoryAmountToEquipmentInRoomView);

    }
}
