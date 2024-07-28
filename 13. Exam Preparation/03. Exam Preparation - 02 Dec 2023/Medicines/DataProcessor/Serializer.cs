using Medicines.Data.Models.Enums;
using Medicines.DataProcessor.ExportDtos;
using Microsoft.EntityFrameworkCore;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Utilities;
    using Microsoft.Data.SqlClient.Server;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Patients";

            bool success = DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTimeDate);

            ExportPatientDto[] patients = context.Patients.AsNoTracking()
                .Where(p => p.PatientsMedicines.Any(pm => pm.Medicine.ProductionDate > dateTimeDate))
                .Select(p => new ExportPatientDto
                {
                    FullName = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Gender = p.Gender.ToString().ToLower(),
                    Medicines = p.PatientsMedicines
                        .Where(pm => pm.Medicine.ProductionDate > dateTimeDate)
                        .Select(pm => pm.Medicine)
                        .OrderByDescending(m => m.ExpiryDate)
                        .ThenBy(m => m.Price)
                        .Select(m => new ExportMedicineXmlDto()
                        {
                            Name = m.Name,
                            Price = m.Price.ToString("F2"),
                            Category = m.Category.ToString().ToLower(),
                            Producer = m.Producer,
                            ExpiryDate = m.ExpiryDate.ToString("yyyy-MM-dd")
                        })
                        .ToArray()
                })
                .OrderByDescending(p => p.Medicines.Length)
                .ThenBy(p => p.FullName)
                .ToArray();



            return xmlHelper.Serialize(patients, xmlRoot);
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
