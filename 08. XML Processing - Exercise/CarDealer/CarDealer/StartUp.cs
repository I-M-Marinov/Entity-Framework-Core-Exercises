using CarDealer.Data;
using CarDealer.Models;
using System.Xml.Serialization;
using CarDealer.DTOs.Import;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext db = new();

            var xmlSuppliersFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\suppliers.xml";
            string inputXmlSuppliers = File.ReadAllText(xmlSuppliersFilePath);

            Console.WriteLine(ImportSuppliers(db, inputXmlSuppliers));
        }

        // Query 9. Import Suppliers

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SuppliersDto[]),
                new XmlRootAttribute("Suppliers"));

            using StringReader stringReader = new StringReader(inputXml);

            SuppliersDto[] suppliers = (SuppliersDto[])serializer.Deserialize(stringReader);

            List<Supplier> suppliersToAdd = suppliers
                .Select(s => new Supplier()
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                    
                })
                .ToList();

            context.Suppliers.AddRange(suppliersToAdd);
            context.SaveChanges();


            return $"Successfully imported {suppliersToAdd.Count}";
        }
    }
}