using Microsoft.EntityFrameworkCore;
using Trucks.DataProcessor.ExportDto;

namespace Trucks.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Linq;
    using Trucks.Utilities;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Despatchers";

            ExportDispatchersDto[] dispatchersToExport = context.Despatchers.AsNoTracking()
                .Where(d => d.Trucks.Any())
                .Select(d => new ExportDispatchersDto()
                {
                    DespatcherName = d.Name,
                    TrucksCount = d.Trucks.Count,
                    Trucks = d.Trucks
                        .OrderBy(t => t.RegistrationNumber)
                        .Select(t => new ExportTruckXmlDto()
                        {
                            RegistrationNumber = t.RegistrationNumber,
                            Make = t.MakeType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(d => d.TrucksCount)
                .ThenBy(d => d.DespatcherName)
                .ToArray();

            return xmlHelper.Serialize(dispatchersToExport, xmlRoot);

        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clientsToExport = context.Clients
                .Where(c => c.ClientsTrucks.Any(ct => ct.Truck.TankCapacity >= capacity))
                .Select(c => new
                {
                    c.Name,
                    Trucks = c.ClientsTrucks
                        .Where(ct => ct.Truck.TankCapacity >= capacity)
                        .Select(x => new
                        {
                            TruckRegistrationNumber = x.Truck.RegistrationNumber,
                            VinNumber = x.Truck.VinNumber,
                            TankCapacity = x.Truck.TankCapacity,
                            CargoCapacity = x.Truck.CargoCapacity,
                            CategoryType = x.Truck.CategoryType,
                            MakeType = x.Truck.MakeType
                        })
                        .OrderBy(t => t.MakeType) 
                        .ThenByDescending(t => t.CargoCapacity)
                        .ToArray()
                })
                .AsEnumerable() 
                .Select(c => new ExportClientsDto()
                {
                    Name = c.Name,
                    Trucks = c.Trucks
                        .Select(x => new ExportTrucksDto()
                        {
                            TruckRegistrationNumber = x.TruckRegistrationNumber,
                            VinNumber = x.VinNumber,
                            TankCapacity = x.TankCapacity,
                            CargoCapacity = x.CargoCapacity,
                            CategoryType = x.CategoryType.ToString(),
                            MakeType = x.MakeType.ToString()
                        })
                        .OrderBy(t => t.MakeType)
                        .ThenByDescending(t => t.CargoCapacity)
                        .ToArray()
                })
                .OrderByDescending(c => c.Trucks.Count) 
                .ThenBy(c => c.Name)
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(clientsToExport, Formatting.Indented);
        }
    }
}
