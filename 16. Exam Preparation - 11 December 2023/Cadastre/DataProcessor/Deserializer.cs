using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using Cadastre.DataProcessor.ImportDtos;
using Microsoft.EntityFrameworkCore;

namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Utilities;
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

                Region region = districtDto.Region switch
                {
                    "NorthEast" => Region.NorthEast,
                    "NorthWest" => Region.NorthWest,
                    "SouthEast" => Region.SouthEast,
                    "SouthWest" => Region.SouthWest,
                    _ => throw new InvalidOperationException("Invalid region value")
                };

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
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
