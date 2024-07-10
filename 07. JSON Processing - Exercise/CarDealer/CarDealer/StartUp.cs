using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Newtonsoft.Json;

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


            string jsonDataSuppliers = File.ReadAllText(suppliersFilePath);
            string jsonDataParts = File.ReadAllText(partsFilePath);
            string jsonDataCars = File.ReadAllText(carsFilePath);
            string jsonDataCustomers = File.ReadAllText(customersFilePath);


            //   Console.WriteLine(ImportSuppliers(db, jsonDataSuppliers));

            //   Console.WriteLine(ImportParts(db, jsonDataParts));

            //   Console.WriteLine(ImportCars(db, jsonDataCars));

            //   Console.WriteLine(ImportCars(db, jsonDataCustomers));

            Console.WriteLine(ImportCustomers(db, jsonDataCustomers));


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

    }
}
