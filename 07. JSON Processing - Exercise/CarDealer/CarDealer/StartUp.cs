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

            string jsonDataSuppliers = File.ReadAllText(suppliersFilePath);


            Console.WriteLine(ImportSuppliers(db, jsonDataSuppliers));


        }

        // Query 9. Import Suppliers 

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }
    }
}
