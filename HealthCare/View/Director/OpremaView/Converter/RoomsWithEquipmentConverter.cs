using HCIProjekat.View.OpremaView.DataView;
using Model.Rooms;
using System.Collections.Generic;

namespace HealthCare.View.Director.OpremaView.Converter
{
    class RoomsWithEquipmentConverter : AbstractConverter
    {
        public static RoomWithSelectedEquipmentItem ConvertRoomInventoryToRoomView(InventoryAmount inventory)
            => new RoomWithSelectedEquipmentItem
            {
                RoomNumber = inventory.room.RoomNumber,
                RoomType = inventory.room.RoomType,
                AmountInRoom = inventory.Amount,
                RoomSector = inventory.room.RoomSector,
                Floor = inventory.room.Floor
            };

        public static IList<RoomWithSelectedEquipmentItem> ConvertRoomInventoryListToInventoryViewList(IList<InventoryAmount> inventories)
            => ConvertEntityListToViewList(inventories, ConvertRoomInventoryToRoomView);

    }
}
