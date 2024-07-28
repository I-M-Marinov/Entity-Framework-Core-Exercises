using System.Globalization;
using Castle.Core.Internal;
using Medicines.Data.Models;
using Medicines.Data.Models.Enums;
using Medicines.DataProcessor.ImportDtos;

namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Utilities;
    using Microsoft.VisualBasic;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Patient> patientsToImport = new List<Patient>();


            ImportPatientDto[] deserializedPatients = JsonConvert.DeserializeObject<ImportPatientDto[]>(jsonString)!;

            foreach (ImportPatientDto patientDto in deserializedPatients)
            {
                if (!IsValid(patientDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Patient newPatient = new Patient()
                {
                    FullName = patientDto.FullName,
                    AgeGroup = (AgeGroup)patientDto.AgeGroup,
                    Gender = (Gender)patientDto.Gender,
                };

                foreach (int medicineId in patientDto.Medicines)
                {
                    if (newPatient.PatientsMedicines.Any(pm => pm.MedicineId == medicineId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    PatientMedicine patientMedicine = new PatientMedicine()
                    {
                        Patient = newPatient,
                        MedicineId = medicineId,
                    };

                    newPatient.PatientsMedicines.Add(patientMedicine);
                }

                patientsToImport.Add(newPatient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, newPatient.FullName, newPatient.PatientsMedicines.Count));
            }

            context.Patients.AddRange(patientsToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Pharmacies";

            ICollection<Pharmacy> pharmaciesToImport = new List<Pharmacy>();

            ImportPharmacyDto[] deserializedPharmacies = xmlHelper.Deserialize<ImportPharmacyDto[]>(xmlString, xmlRoot);

            foreach (ImportPharmacyDto pharmacyDto in deserializedPharmacies)
            {
                if (!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Pharmacy newPharmacy = new Pharmacy()
                {
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,
                    IsNonStop = bool.Parse(pharmacyDto.IsNonStop),
                };

                foreach (ImportMedicineDto medicineDto in pharmacyDto.Medicines)
                {

                    if (!IsValid(medicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    bool isProductionDateValid = DateTime.TryParseExact(medicineDto.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, 
                        DateTimeStyles.None, out DateTime productionDate);
                    bool isExpiryDateValid = DateTime.TryParseExact(medicineDto.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate);

                    if (!isProductionDateValid || !isExpiryDateValid || productionDate >= expiryDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (newPharmacy.Medicines.Any(m => m.Name == medicineDto.Name && m.Producer == medicineDto.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Medicine newMedicine = new Medicine()
                    {
                        Name = medicineDto.Name,
                        Price = (decimal)medicineDto.Price,
                        Category = (Category)medicineDto.Category,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        Producer = medicineDto.Producer
                    };

                    newPharmacy.Medicines.Add(newMedicine);
                }

                pharmaciesToImport.Add(newPharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, newPharmacy.Name, newPharmacy.Medicines.Count));
            }

            context.Pharmacies.AddRange(pharmaciesToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

    }
}
