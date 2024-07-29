
using Artillery.DataProcessor.ExportDto;

namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models.Enums;
    using Artillery.Utilities;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shellsToExport = context.Shells
                .Where(s => s.ShellWeight > shellWeight)
                .OrderBy(s => s.ShellWeight)
                .Select(s => new ExportShellsDto()
                {
                    ShellWeight = s.ShellWeight,
                    Caliber = s.Caliber,
                    Guns = s.Guns
                        .Where(g => g.GunType == GunType.AntiAircraftGun) 
                        .Select(g => new ExportGunsDto()
                        {
                            GunType = g.GunType.ToString(), 
                            GunWeight = g.GunWeight,
                            BarrelLength = g.BarrelLength,
                            Range = g.Range > 3000 ? "Long-range" : "Regular range" 
                        })
                        .OrderByDescending(g => g.GunWeight) 
                        .ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(shellsToExport, Formatting.Indented);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Guns";

            ExportGunsXmlDto[] gunsToExport = context.Guns
                .Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                .Select(g => new ExportGunsXmlDto()
                {
                    Manufacturer = g.Manufacturer.ManufacturerName,
                    GunType = g.GunType.ToString(),
                    GunWeight = g.GunWeight,
                    BarrelLength = g.BarrelLength,
                    Range = g.Range,
                    Countries = g.CountriesGuns
                        .Where(cg => cg.Country.ArmySize > 4500000)
                        .Select(cg => new ExportCountryDto()
                        {
                            Country = cg.Country.CountryName,
                            ArmySize = cg.Country.ArmySize
                        })
                        .OrderBy(cg => cg.ArmySize)
                        .ToArray()

                })
                .OrderBy(g => g.BarrelLength)
                .ToArray();

            return xmlHelper.Serialize(gunsToExport, xmlRoot);
        }

    }
}

