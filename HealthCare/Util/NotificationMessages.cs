using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Util
{
    public static class NotificationMessages
    {
        public static readonly string ABSENCE_APPROVAL_APPROVED_TITLE = "Zahtev za odsustvo je odobren";
        public static readonly string ABSENCE_APPROVAL_REJECTED_TITLE = "Zahtev za odsustvo je odbijen";

        public static readonly string ABSENCE_APPROVAL_APPROVED_TEXT = "Vas zahtev za odsustvo je prihvacen! ";
        public static readonly string ABSENCE_APPROVAL_REJECTED_TEXT = "Vas zahtev za odsustvo je odbijen! ";

        public static readonly string DRUG_CREATE_TITLE = "Zahtev za odobravanje leka";
        public static readonly string DRUG_CREATE_TEXT = "Novi lek je evidentiran! ";

        public static readonly string DRUG_UPDATE_TITLE = "Zahtev za odobravanje leka";
        public static readonly string DRUG_UPDATE_TEXT = "Lek je izmenjen i treba ga proveriti! ";

        public static readonly string ABSENCE_CREATE_TITLE = "Zahtev za odsustvo";
        public static readonly string ABSENCE_CREATE_TEXT = "Novi zahtev za odsustvo je evidentiran!";

        public static readonly string DRUG_APPROVAL_APPROVED_TITLE = "Lek je odobren";
        public static readonly string DRUG_APPROVAL_APPROVED_TEXT = "Lekar je odobrio lek ";

        public static readonly string DRUG_APPROVAL_REJECTED_TITLE = "Lek je odbijen";
        public static readonly string DRUG_APPROVAL_REJECTED_TEXT = "Lekar je odbio lek ";

        public static readonly string DRUG_AMOUNT_TITLE = "Mala kolicina leka";
        public static readonly string DRUG_AMOUNT_TEXT = "Kolicina leka je pala ispod granice ";

        public static readonly string REFFERAL_HOSPITALTREATMENT_CREATE_TITLE = "Zahtev za bolnicko lecenje";
        public static readonly string REFFERAL_HOSPITALTREATMENT_CREATE_TEXT = "Lekar je dodao novi zahtev za bolnicko lecenje za datum: ";

        public static readonly string REFFERAL_SURGERY_CREATE_TITLE = "Zahtev za hitnu operaciju";
        public static readonly string REFFERAL_SURGERY_CREATE_TEXT = "Lekar je dodao novi zahtev za hitnu operaciju";
    }
}
