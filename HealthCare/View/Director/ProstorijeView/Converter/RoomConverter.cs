using HCIProjekat.View.ProstorijeView.DataView;
using Model.Rooms;
using System.Collections.Generic;

namespace HealthCare.View.Director.LekoviView.Converter
{
    class RoomConverter : AbstractConverter
    {
        public static RoomItem ConvertRoomToRoomView(Room room)
            => new RoomItem
            {
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
                RoomSector = room.RoomSector,
                Floor = room.Floor
            };

        public static IList<RoomItem> ConvertRoomListToRoomViewList(IList<Room> drugs)
            => ConvertEntityListToViewList(drugs, ConvertRoomToRoomView);

    }
}
