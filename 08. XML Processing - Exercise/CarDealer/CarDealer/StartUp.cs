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


            var xmlCarsFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\cars.xml";
            string inputXmlCars = File.ReadAllText(xmlCarsFilePath);

            var xmlCustomersFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\customers.xml";
            string inputXmlCustomers = File.ReadAllText(xmlCustomersFilePath);

            // Console.WriteLine(ImportSuppliers(db, inputXmlSuppliers));

            // Console.WriteLine(ImportParts(db, inputXmlParts));

            //  Console.WriteLine(ImportCars(db, inputXmlCars));

            Console.WriteLine(ImportCustomers(db, inputXmlCustomers));
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

        // Query 11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute root = new XmlRootAttribute("Cars");
            XmlSerializer serializer = new XmlSerializer(typeof(CarsDto[]), root);

            using StringReader reader = new StringReader(inputXml);

            CarsDto[] cars = (CarsDto[])serializer.Deserialize(reader);

            ICollection<Car> carsToAdd = new List<Car>();

            foreach (CarsDto car in cars)
            {
                Car carToAdd = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TraveledDistance = car.TraveledDistance
                };

                ICollection<PartCar> partCars = new List<PartCar>();

                foreach (int partId in car.Parts.Select(p => p.Id).Distinct())
                {
                    if (context.Parts.Any(p => p.Id == partId))
                    {
                        PartCar partCar = new PartCar()
                        {
                            Car = carToAdd,
                            PartId = partId,
                        };
                        partCars.Add(partCar);
                    }
                }
                carToAdd.PartsCars = partCars;
                carsToAdd.Add(carToAdd);
            }
            context.Cars.AddRange(carsToAdd);
            context.SaveChanges();

            return $"Successfully imported {carsToAdd.Count}";
        }

        // Query 12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CustomersDto[]),
                new XmlRootAttribute("Customers"));

            using StringReader stringReader = new StringReader(inputXml);

            CustomersDto[] customers = (CustomersDto[])serializer.Deserialize(stringReader);

            List<Customer> customersToAdd = customers
                .Select(c => new Customer()
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();

            context.Customers.AddRange(customersToAdd);
            context.SaveChanges();


            return $"Successfully imported {customersToAdd.Count}";
        }
    }
}