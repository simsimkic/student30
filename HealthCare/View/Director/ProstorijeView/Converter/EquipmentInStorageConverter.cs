using HCIProjekat.View.ProstorijeView.DataView;
using Model.Rooms;
using System.Collections.Generic;

namespace HealthCare.View.Director.ProstorijeView.Converter
{
    class EquipmentInStorageConverter : AbstractConverter
    {
        public static EquipmentInStorageItem ConvertInventoryAmountToEquipmentInRoomView(Inventory inventory)
            => new EquipmentInStorageItem
            {
                EquipmentAmount = inventory.QuantityStorage,
                EquipmentName = inventory.Name,
                EquipmentType = inventory.InventoryType,
                Id = inventory.Id
            };

        public static IList<EquipmentInStorageItem> ConvertInventoryAmountListToEquipmentInRoomViewList(IList<Inventory> inventories)
            => ConvertEntityListToViewList(inventories, ConvertInventoryAmountToEquipmentInRoomView);
    }
}
