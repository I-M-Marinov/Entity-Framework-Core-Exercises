using CarDealer.Data;
using CarDealer.Models;
using System.Xml.Serialization;
using CarDealer.DTOs.Import;
using System.IO;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext db = new();

            var xmlSuppliersFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\suppliers.xml";
            string inputXmlSuppliers = File.ReadAllText(xmlSuppliersFilePath);

            var xmlPartsFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\parts.xml";
            string inputXmlParts = File.ReadAllText(xmlPartsFilePath);

            // Console.WriteLine(ImportSuppliers(db, inputXmlSuppliers));

            Console.WriteLine(ImportParts(db, inputXmlParts));
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

        // Query 10. Import Parts

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PartsDto[]),
                new XmlRootAttribute("Parts")); // Adjust the root element name as needed

            using StringReader stringReader = new StringReader(inputXml);

            PartsDto[] parts = (PartsDto[])serializer.Deserialize(stringReader);

            ICollection<Part> partsToAdd = new List<Part>();

            foreach (PartsDto part in parts)
            {
                if (context.Suppliers.Any(s => s.Id == part.SupplierId))
                {
                    partsToAdd.Add(new Part()
                    {
                        Name = part.Name,
                        Price = part.Price,
                        Quantity = part.Quantity,
                        SupplierId = part.SupplierId
                    });
                }
            }

            context.Parts.AddRange(partsToAdd);
            context.SaveChanges();

            return $"Successfully imported {partsToAdd.Count}";
        }
    }
}