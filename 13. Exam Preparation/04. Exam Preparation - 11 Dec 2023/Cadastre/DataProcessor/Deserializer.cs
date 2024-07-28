using System.Reflection.Metadata.Ecma335;
using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using Cadastre.DataProcessor.ImportDtos;
using Microsoft.EntityFrameworkCore;

namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Utilities;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlTypes;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Districts";

            ICollection<District> districtsToImport = new List<District>();

            ImportDistrictDto[] deserializedDistricts = xmlHelper.Deserialize<ImportDistrictDto[]>(xmlDocument, xmlRoot);

            foreach (ImportDistrictDto districtDto in deserializedDistricts)
            {
                if (!IsValid(districtDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Region region;

                if (districtDto.Region == "NorthEast")
                {
                    region = Region.NorthEast;
                }
                else if (districtDto.Region == "NorthWest")
                {
                    region = Region.NorthWest;
                }
                else if (districtDto.Region == "SouthEast")
                {
                    region = Region.SouthEast;
                }
                else if (districtDto.Region == "SouthWest")
                {
                    region = Region.SouthWest;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                District newDistrict = new District()
                {
                    Name = districtDto.Name,
                    PostalCode = districtDto.PostalCode,
                    Region = region
                };

                foreach (ImportPropertyDto propertyDto in districtDto.Properties)
                {

                    if (!IsValid(propertyDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (propertyDto.Area < 0)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    bool isDateOfAcquisitionValid = DateTime.TryParseExact(propertyDto.DateOfAcquisition, "dd/MM/yyyy" , CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime acquisitionDateTime);


                    if (!isDateOfAcquisitionValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    Property newProperty = new Property()
                    {
                        PropertyIdentifier = propertyDto.PropertyIdentifier,
                        Area = propertyDto.Area,
                        Details = propertyDto.Details,
                        Address = propertyDto.Address,
                        DateOfAcquisition = acquisitionDateTime
                    };

                    if (newDistrict.Properties.Any(p => p.PropertyIdentifier == newProperty.PropertyIdentifier) || 
                        dbContext.Properties.AsNoTracking().Any(p => p.PropertyIdentifier == newProperty.PropertyIdentifier))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (newDistrict.Properties.Any(p => p.Address == newProperty.Address) ||
                        dbContext.Properties.AsNoTracking().Any(p => p.Address == newProperty.Address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    newDistrict.Properties.Add(newProperty);
                }

                if (districtsToImport.Contains(newDistrict) || dbContext.Districts.AsNoTracking().Contains(newDistrict))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                districtsToImport.Add(newDistrict);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, newDistrict.Name, newDistrict.Properties.Count));
            }

            dbContext.Districts.AddRange(districtsToImport);
            dbContext.SaveChanges();

            return sb.ToString();
        }
        
        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Citizen> citizensToImport = new List<Citizen>();

            ImportCitizenDto[] deserializedCitizens = JsonConvert.DeserializeObject<ImportCitizenDto[]>(jsonDocument)!;

            foreach (ImportCitizenDto citizenDto in deserializedCitizens)
            {
                if (!IsValid(citizenDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                MaritalStatus maritalStatus;

                if (citizenDto.MaritalStatus == "Unmarried")
                {
                     maritalStatus = MaritalStatus.Unmarried;
                }
                else if (citizenDto.MaritalStatus == "Married")
                {
                     maritalStatus = MaritalStatus.Married;
                }
                else if (citizenDto.MaritalStatus == "Divorced")
                {
                     maritalStatus = MaritalStatus.Divorced;
                }
                else if (citizenDto.MaritalStatus == "Widowed")
                {
                     maritalStatus = MaritalStatus.Widowed;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                bool isBirthDateValid = DateTime.TryParseExact(citizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime birthDate);

                if (!isBirthDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen newCitizen = new Citizen()
                {
                    FirstName = citizenDto.FirstName,
                    LastName = citizenDto.LastName,
                    BirthDate = birthDate,
                    MaritalStatus = maritalStatus,
                };

                ICollection<PropertyCitizen> propertiesCitizensToImport = new List<PropertyCitizen>();

                foreach (var propertyId in citizenDto.Properties.Distinct())
                {
                    PropertyCitizen propertyCitizen = new PropertyCitizen()
                    {
                        Citizen = newCitizen,
                        PropertyId = propertyId
                    };

                    propertiesCitizensToImport.Add(propertyCitizen);
                }

                newCitizen.PropertiesCitizens = propertiesCitizensToImport;

                citizensToImport.Add(newCitizen);

                sb.AppendLine(string.Format(SuccessfullyImportedCitizen, newCitizen.FirstName, newCitizen.LastName, newCitizen.PropertiesCitizens.Count));

            }

            dbContext.Citizens.AddRange(citizensToImport);
            dbContext.SaveChanges();

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
