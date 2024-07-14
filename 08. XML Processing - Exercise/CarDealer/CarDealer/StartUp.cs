using CarDealer.Data;
using CarDealer.Models;
using System.Xml.Serialization;
using CarDealer.DTOs.Import;
using System.IO;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;
using CarDealer.DTOs.Export;
using System.Xml;

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

            var xmlSalesFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-CarDealer-6.0\\CarDealer\\Datasets\\sales.xml";
            string inputXmlSales = File.ReadAllText(xmlSalesFilePath);

            // Console.WriteLine(ImportSuppliers(db, inputXmlSuppliers));

            // Console.WriteLine(ImportParts(db, inputXmlParts));

            // Console.WriteLine(ImportCars(db, inputXmlCars));

            // Console.WriteLine(ImportCustomers(db, inputXmlCustomers));

            // Console.WriteLine(ImportSales(db, inputXmlSales));

            // Console.WriteLine(GetCarsWithDistance(db));

            // Console.WriteLine(GetCarsFromMakeBmw(db));

            Console.WriteLine(GetLocalSuppliers(db));
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
                new XmlRootAttribute("Parts")); 

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

        // Query 13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SalesDto[]),
                new XmlRootAttribute("Sales"));

            using StringReader reader = new StringReader(inputXml);

            SalesDto[] sales = (SalesDto[])serializer.Deserialize(reader);

            ICollection<Sale> salesToAdd = new List<Sale>();

            foreach (SalesDto saleDto in sales)
            {
                bool carExists = context.Cars.Any(c => c.Id == saleDto.CarId);

                if (!carExists)
                {
                    continue;
                }

                Sale sale = new Sale()
                {
                    CarId = saleDto.CarId,
                    CustomerId = saleDto.CustomerId,
                    Discount = saleDto.Discount
                };

                salesToAdd.Add(sale);
            }
            context.Sales.AddRange(salesToAdd);
            context.SaveChanges();

            return $"Successfully imported {salesToAdd.Count}";
        }

        // Query 14. Export Cars With Distance ( they are about to breakdown... 2 million kilometers distance :) 
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            List<CarExportDto> aboutToBreakdownCars = context.Cars
                .Where(c => c.TraveledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .Select(c => new CarExportDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                })
                .ToList();


            XmlSerializer serializer = new XmlSerializer(typeof(List<CarExportDto>), new XmlRootAttribute("cars"));

            using StringWriter writer = new StringWriter();

            using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(xmlWriter, aboutToBreakdownCars, emptyNamespaces);

            return writer.ToString().TrimEnd();
        }

        // Query 15. Export Cars from Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
           List<CarExportAttributeDto> beamerCars = context.Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new CarExportAttributeDto
                {
                    Id = c.Id,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                }).ToList();

           XmlSerializer serializer = new XmlSerializer(typeof(List<CarExportAttributeDto>), new XmlRootAttribute("cars"));

           using StringWriter writer = new StringWriter();

           using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

           XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
           emptyNamespaces.Add(string.Empty, string.Empty);

           serializer.Serialize(xmlWriter, beamerCars, emptyNamespaces);

            return writer.ToString();
        }

        // Query 16. Export Local Suppliers

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            List<SuppliersExportDto> suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new SuppliersExportDto
                {
                  Id = s.Id,
                  Name = s.Name,
                  Parts = s.Parts.Count
                }).ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<SuppliersExportDto>), new XmlRootAttribute("suppliers"));

            using StringWriter writer = new StringWriter();

            using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(xmlWriter, suppliers, emptyNamespaces);

            return writer.ToString();
        }
    }
}
