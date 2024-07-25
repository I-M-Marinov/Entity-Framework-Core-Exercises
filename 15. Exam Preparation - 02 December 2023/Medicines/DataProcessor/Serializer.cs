using Medicines.Data.Models.Enums;
using Medicines.DataProcessor.ExportDtos;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            throw new NotImplementedException();
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {

            var medicines = context.Medicines
                .Where(m => m.Category == (Category)medicineCategory)
                .Where(m => m.Pharmacy.IsNonStop == true)
                .Select(m => new
                {
                    m.Name,
                    m.Price,
                    Pharmacy = new
                    {
                        m.Pharmacy.Name,
                        m.Pharmacy.PhoneNumber
                    }
                })
                .OrderBy(m => m.Price)
                .ThenBy(m => m.Name)
                .ToList();


            ExportMedicineDto[] medicinesToExport = medicines
                .Select(m => new ExportMedicineDto()
                {
                    Name = m.Name,
                    Price = m.Price.ToString("f2", CultureInfo.InvariantCulture),
                    Pharmacy = new ExportPharmacyDto()
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }
                })
                .ToArray();

            return JsonConvert.SerializeObject(medicinesToExport, Formatting.Indented);
        }
    }
}
