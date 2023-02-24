using HCIProjekat.View.ZaposleniView.DataView;
using Model.Users;
using System.Collections.Generic;

namespace HealthCare.View.Director.ZaposleniView.Converter
{
    class AbsenceHistoryItemConverter : AbstractConverter
    {
        public static AbsenceHistoryItem ConvertAbsenceToAbsenceHistoryView(Absence absence)
            => new AbsenceHistoryItem
            {
                AbsenceKind = absence.AbsenceType,
                DateStartAbsence = absence.StartDate.Date,
                DateEndAbsence = absence.EndDate.Date
            };

        public static IList<AbsenceHistoryItem> ConvertAbsenceListToAbsenceViewList(IList<Absence> absences)
            => ConvertEntityListToViewList(absences, ConvertAbsenceToAbsenceHistoryView);

    }
}
