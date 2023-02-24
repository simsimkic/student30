using HCIProjekat.View.RenoviranjaView.DataView;
using Model.Rooms;
using System.Collections.Generic;

namespace HealthCare.View.Director.RenoviranjaView.Converter
{
    class RenovationItemConverter : AbstractConverter
    {
        public static RenovationItem ConvertRenovateToRenovateView(Renovate renovate)
           => new RenovationItem
           {
               Id = renovate.Id,
               StartDate = renovate.DateStart.Date,
               EndDate = renovate.DateEnd.Date,
               RoomNumber = renovate.room.RoomNumber,
               RenovationDescription = renovate.Note
           };

        public static IList<RenovationItem> ConvertRenovateListToRenovateViewList(IList<Renovate> renovates)
            => ConvertEntityListToViewList(renovates, ConvertRenovateToRenovateView);
    }
}
