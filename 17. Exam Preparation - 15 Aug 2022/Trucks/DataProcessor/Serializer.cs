using Trucks.DataProcessor.ExportDto;

namespace Trucks.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Linq;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            throw new NotImplementedException();
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
