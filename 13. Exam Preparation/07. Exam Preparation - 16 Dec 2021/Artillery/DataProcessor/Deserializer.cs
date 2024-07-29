using System.ComponentModel.DataAnnotations;
using Artillery.Data.Models;
using Artillery.DataProcessor.ImportDto;

namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Utilities;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
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
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Manufacturers";

            ICollection<Manufacturer> manufacturersToImport = new List<Manufacturer>();

            ImportManufacturerDto[] deserializedManufacturers = xmlHelper.Deserialize<ImportManufacturerDto[]>(xmlString, xmlRoot);

            foreach (ImportManufacturerDto manufacturerDto in deserializedManufacturers)
            {
                if (!IsValid(manufacturerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer newManufacturer = new Manufacturer()
                {
                    ManufacturerName = manufacturerDto.ManufacturerName,
                    Founded = manufacturerDto.Founded
                };

                if (manufacturersToImport.Any(m => m.ManufacturerName == newManufacturer.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                manufacturersToImport.Add(newManufacturer);
                sb.AppendLine(string.Format(SuccessfulImportManufacturer, newManufacturer.ManufacturerName, newManufacturer.Founded));
            }

            context.Manufacturers.AddRange(manufacturersToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Shells";

            ICollection<Shell> shellsToImport = new List<Shell>();

            ImportShellDto[] deserializedShells = xmlHelper.Deserialize<ImportShellDto[]>(xmlString, xmlRoot);

            foreach (ImportShellDto shellDto in deserializedShells)
            {
                if (!IsValid(shellDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Shell newShell = new Shell()
                {
                    ShellWeight = shellDto.ShellWeight,
                    Caliber = shellDto.Caliber
                };


                shellsToImport.Add(newShell);
                sb.AppendLine(string.Format(SuccessfulImportShell, newShell.Caliber, newShell.ShellWeight));
            }

            context.Shells.AddRange(shellsToImport);
            context.SaveChanges();

            return sb.ToString();
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