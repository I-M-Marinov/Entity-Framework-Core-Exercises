using System.Globalization;
using Newtonsoft.Json;
using TravelAgency.Data;
using TravelAgency.Data.Models.Enums;
using TravelAgency.DataProcessor.ExportDtos;
using TravelAgency.Utilities;

namespace TravelAgency.DataProcessor
{
    public class Serializer
    {
        public static string ExportGuidesWithSpanishLanguageWithAllTheirTourPackages(TravelAgencyContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Guides";

            ExportGuidesDto[] guidesToExport = context.Guides
                .Where(g => g.Language == Language.Spanish)
                .Select(g => new ExportGuidesDto()
                {
                    FullName = g.FullName,
                    TourPackages = g.TourPackagesGuides
                        .Select(tpg => new ExportTourPackageDto()
                        {
                            Name = tpg.TourPackage.PackageName,
                            Description = tpg.TourPackage.Description,
                            Price = tpg.TourPackage.Price 
                        })
                        .OrderByDescending(tpg => tpg.Price)
                        .ThenBy(tpg => tpg.Name)
                        .ToArray()
                })
                .OrderByDescending(g => g.TourPackages.Length)
                .ThenBy(g => g.FullName)
                .ToArray();

            return xmlHelper.Serialize(guidesToExport, xmlRoot);


        }

        public static string ExportCustomersThatHaveBookedHorseRidingTourPackage(TravelAgencyContext context)
        {

            var customersToExport = context.Customers
                .Where(c => c.Bookings.Any(b => b.TourPackage.PackageName == "Horse Riding Tour"))
                .Select(c => new ExportCustomersDto()
                {
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    Bookings = c.Bookings
                        .Where(b => b.TourPackage.PackageName == "Horse Riding Tour")
                        .OrderBy(b => b.BookingDate)
                        .Select(b => new ExportBookingsDto()
                        {
                            TourPackageName = b.TourPackage.PackageName,
                            Date = b.BookingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        })
                        .ToArray()
                })
                .OrderByDescending(c => c.Bookings.Length)
                .ThenBy(c => c.FullName)
                .ToArray();

            return JsonConvert.SerializeObject(customersToExport, Formatting.Indented);

        }
    }
}
