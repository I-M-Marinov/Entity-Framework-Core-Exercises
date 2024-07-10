using CarDealer.Data;
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


            string jsonDataSuppliers = File.ReadAllText(suppliersFilePath);
            string jsonDataParts = File.ReadAllText(partsFilePath);
            string jsonDataCars = File.ReadAllText(carsFilePath);


         //   Console.WriteLine(ImportSuppliers(db, jsonDataSuppliers));

         //   Console.WriteLine(ImportParts(db, jsonDataParts));

            Console.WriteLine(ImportCars(db, jsonDataCars));


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

            var cars = JsonConvert.DeserializeObject<List<Car>>(inputJson);

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

    }
}
