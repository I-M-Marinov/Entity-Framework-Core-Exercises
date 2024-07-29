using System.ComponentModel.DataAnnotations;
using Artillery.Data.Models;
using Artillery.DataProcessor.ImportDto;

namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
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

            return sb.ToString().TrimEnd();

        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Manufacturers";

            ICollection<Manufacturer> manufacturersToImport = new List<Manufacturer>();

            ImportManufacturerDto[] deserializedManufacturers = xmlHelper.Deserialize<ImportManufacturerDto[]>(xmlString, xmlRoot);

            foreach (ImportManufacturerDto manufacturerDto in deserializedManufacturers.Distinct())
            {
                if (!IsValid(manufacturerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (manufacturersToImport.Any(m => m.ManufacturerName == manufacturerDto.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer newManufacturer = new Manufacturer()
                {
                    ManufacturerName = manufacturerDto.ManufacturerName,
                    Founded = manufacturerDto.Founded
                };

                manufacturersToImport.Add(newManufacturer);

                var manufacturerFounded = newManufacturer.Founded.Split(", ").ToArray();

               var stateFounded = "";
                for (int i = 0; i < manufacturerFounded.Length; i++)
                { 
                    stateFounded = manufacturerFounded[manufacturerFounded.Length - 2] + ", " + manufacturerFounded[manufacturerFounded.Length - 1];
                }

                sb.AppendLine(string.Format(SuccessfulImportManufacturer, newManufacturer.ManufacturerName, stateFounded));
            }

            context.Manufacturers.AddRange(manufacturersToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

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

            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Gun> gunsToImport = new List<Gun>();

            ImportGunDto[] deserializedGuns = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString)!;

           // var validCountriesIds = context.Countries.Select(c => c.Id).ToList();

            foreach (ImportGunDto gunDto in deserializedGuns)
            {
                if (!IsValid(gunDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                GunType gunType;

                if (gunDto.GunType == "Howitzer")
                {
                    gunType = GunType.Howitzer;
                }
                else if (gunDto.GunType == "Mortar")
                {
                    gunType = GunType.Mortar;
                }
                else if (gunDto.GunType == "FieldGun")
                {
                    gunType = GunType.FieldGun;
                }
                else if (gunDto.GunType == "AntiAircraftGun")
                {
                    gunType = GunType.AntiAircraftGun;
                }
                else if (gunDto.GunType == "MountainGun")
                {
                    gunType = GunType.MountainGun;
                }
                else if (gunDto.GunType == "AntiTankGun")
                {
                    gunType = GunType.AntiTankGun;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Gun newGun = new Gun()
                {
                    ManufacturerId = gunDto.ManufacturerId,
                    GunWeight = gunDto.GunWeight,
                    BarrelLength = gunDto.BarrelLength,
                    NumberBuild = gunDto.NumberBuild,
                    Range = gunDto.Range,
                    GunType = gunType,
                    ShellId = gunDto.ShellId
                };

                ICollection<CountryGun> countryGunToImport = new List<CountryGun>();

                foreach (ImportCountriesDto countryDto in gunDto.Countries)
                {

                    if (!IsValid(countryDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //if (!validCountriesIds.Contains(countryDto.Id))
                    //{
                    //    sb.AppendLine(ErrorMessage);
                    //    continue;
                    //}

                    CountryGun newCountryGun = new CountryGun()
                    {
                        CountryId = countryDto.Id,
                        Gun = newGun
                    };

                    countryGunToImport.Add(newCountryGun);
                }

                newGun.CountriesGuns = countryGunToImport;

                gunsToImport.Add(newGun);

                sb.AppendLine(string.Format(SuccessfulImportGun, newGun.GunType, newGun.GunWeight, newGun.BarrelLength));


            }

            context.Guns.AddRange(gunsToImport);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
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