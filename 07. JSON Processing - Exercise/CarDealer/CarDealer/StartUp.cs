using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {

            using var db = new CarDealerContext();

            string suppliersFilePath =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\suppliers.json";

            string partsFilePath =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\parts.json";

            string carsFilePath =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\cars.json";

            string customersFilePath =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\customers.json";

            string salesFilePath =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\sales.json";


            string jsonDataSuppliers = File.ReadAllText(suppliersFilePath);
            string jsonDataParts = File.ReadAllText(partsFilePath);
            string jsonDataCars = File.ReadAllText(carsFilePath);
            string jsonDataCustomers = File.ReadAllText(customersFilePath);
            string jsonDataSales = File.ReadAllText(salesFilePath);


            //   Console.WriteLine(ImportSuppliers(db, jsonDataSuppliers));

            //   Console.WriteLine(ImportParts(db, jsonDataParts));

            //   Console.WriteLine(ImportCars(db, jsonDataCars));

            //   Console.WriteLine(ImportCustomers(db, jsonDataCustomers));

            //   Console.WriteLine(ImportSales(db, jsonDataSales));

            //   Console.WriteLine(GetOrderedCustomers(db));

            //   Console.WriteLine(GetCarsFromMakeToyota(db));

            //   Console.WriteLine(GetLocalSuppliers(db));

            //   Console.WriteLine(GetCarsWithTheirListOfParts(db));

            Console.WriteLine(GetTotalSalesByCustomer(db));
        }

        // Query 9. Import Suppliers 
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        // Query 10. Import Parts 
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson);

            var validSuppliersIds = context.Suppliers.Select(s => s.Id).ToList();

            var validParts = parts.Where(p => validSuppliersIds.Contains(p.SupplierId)).ToList();

            context.Parts.AddRange(validParts);
            context.SaveChanges();

            return $"Successfully imported {validParts.Count}.";
        }

        // Query 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            CarInputDto[] cars = JsonConvert.DeserializeObject<CarInputDto[]>(inputJson);
            ICollection<Car> carsToAdd = new List<Car>();
            foreach (CarInputDto car in cars)
            {
                Car currentCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TraveledDistance = car.TraveledDistance
                };
                foreach (int partId in car.PartsId.Distinct())
                {
                    List<int> validIds = context.Parts.Select(p => p.Id).ToList();

                    if (validIds.Contains(partId))
                    {
                        currentCar.PartsCars.Add(new PartCar { PartId = partId });
                    }
                }
                carsToAdd.Add(currentCar);
            }
            context.Cars.AddRange(carsToAdd);
            context.SaveChanges();

            return $"Successfully imported {carsToAdd.Count}.";
        }

        // Query 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>> (inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();


            return string.Format($"Successfully imported {customers.Count}.");

        }

        // Query 13. Import Sales 
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();


            return string.Format($"Successfully imported {sales.Count}.");

        }

        // Query 14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                DateFormatString = "dd/MM/yyyy",
            };

            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(p => new
                { 
                    p.Name,
                    p.BirthDate,
                    p.IsYoungDriver
                }).ToList();


            var json = JsonConvert.SerializeObject(customers, Formatting.Indented, settings);

            File.WriteAllText("ordered-customers.json", json);

            return json;
        }

        // Query 15. Export Cars from Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TraveledDistance
                })
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .ToList();

            var json = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);

            File.WriteAllText("toyota-cars.json", json);

            return json;
        }

        // Query 16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliersWithNoAbroadCars = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var json = JsonConvert.SerializeObject(suppliersWithNoAbroadCars, Formatting.Indented);

            File.WriteAllText("local-suppliers.json", json);

            return json;
        }

        // Query 17. Export Cars with Their List of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance
                    },
                    parts = c.PartsCars
                        .Select(pc => new
                        {
                            pc.Part.Name,
                            Price = pc.Part.Price.ToString("F2")
                        })
                        .ToList()
                })
                .ToList();

            var json = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);

            File.WriteAllText("cars-and-parts.json", json);


            return json;
        }

        // Query 18. Export Total Sales by Customer 
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var customersWithACarBought = context.Customers
                .Where(c => c.Sales.Any())
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales
                        .SelectMany(s => s.Car.PartsCars)
                        .Sum(pc => pc.Part.Price)
                })
                .ToList()
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();




            var json = JsonConvert.SerializeObject(customersWithACarBought, settings);

            File.WriteAllText("customers-total-sales", json);


            return json;

            //public static string GetTotalSalesByCustomer(CarDealerContext context)
            //{
            //    var customersWithSales = context.Customers
            //        .Where(c => c.Sales.Any())
            //        .Select(c => new CustomerSalesDto
            //        {
            //            FullName = c.Name,
            //            BoughtCars = c.Sales.Count,
            //            SpentMoney = c.Sales
            //                .SelectMany(s => s.Car.PartsCars.Select(pc => pc.Part.Price))
            //                .Sum()
            //        })
            //        .ToList()
            //        .OrderByDescending(c => c.SpentMoney)
            //        .ThenByDescending(c => c.BoughtCars)
            //        .ToList();
            //    var jsonResult = JsonConvert.SerializeObject(customersWithSales, Formatting.Indented);
            //    return jsonResult;
            //}
        }
    }
}
