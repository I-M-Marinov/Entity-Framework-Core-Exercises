using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataConstraints
{
    public static class DataConstraints
    {
        public const byte PatientFullNameMinLength = 3;
        public const byte PatientFullNameMaxLength = 150;

        public const byte PharmacyNameMinLength = 2;
        public const byte PharmacyNameMaxLength = 50;
        public const byte PharmacyPhoneNumberLength = 14;

        public const byte MedicineNameMinLength = 3;
        public const byte MedicineNameMaxLength = 150;

        public const string MedicinePriceMinValue = "0.01";
        public const string MedicinePriceMaxValue = "1000.00";

        public const byte MedicineProducerMinLength = 3;
        public const byte MedicineProducerMaxLength = 100;
    }
}
