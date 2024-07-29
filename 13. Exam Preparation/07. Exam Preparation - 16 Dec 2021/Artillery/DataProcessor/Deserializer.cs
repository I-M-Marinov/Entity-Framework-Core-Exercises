using System.ComponentModel.DataAnnotations;
using Artillery.Data.Models;
using Artillery.DataProcessor.ImportDto;

namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Utilities;
    using Microsoft.EntityFrameworkCore;
    using System.Text;
    using System.Xml;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Countries";

            ICollection<Country> countriesToImport = new List<Country>();

            ImportCountryDto[] deserializedCountries = xmlHelper.Deserialize<ImportCountryDto[]>(xmlString, xmlRoot);

            foreach (ImportCountryDto countryDto in deserializedCountries)
            {
                if (!IsValid(countryDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Country newCountry = new Country()
                {
                    CountryName = countryDto.CountryName,
                    ArmySize = countryDto.ArmySize
                };

                countriesToImport.Add(newCountry);
                sb.AppendLine(string.Format(SuccessfulImportCountry, newCountry.CountryName, newCountry.ArmySize));
            }

            context.Countries.AddRange(countriesToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            throw new NotImplementedException();
        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}