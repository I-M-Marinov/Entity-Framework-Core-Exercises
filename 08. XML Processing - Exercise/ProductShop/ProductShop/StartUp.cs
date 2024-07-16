using ProductShop.Data;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using ProductShop.DTOs.Export;
using System.Xml;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            var xmlUsersFilePath = "D:\\Lcfr\\DEVELOPER\\Entity Framework Core Exercises\\08. XML Processing - Exercise\\ProductShop\\ProductShop\\Datasets\\users.xml";
            string inputXmlUsers = File.ReadAllText(xmlUsersFilePath);

            var xmlProductsFilePath = "D:\\Lcfr\\DEVELOPER\\Entity Framework Core Exercises\\08. XML Processing - Exercise\\ProductShop\\ProductShop\\Datasets\\products.xml";
            string inputXmlProducts = File.ReadAllText(xmlProductsFilePath);

            var xmlCategoriesFilePath = "D:\\Lcfr\\DEVELOPER\\Entity Framework Core Exercises\\08. XML Processing - Exercise\\ProductShop\\ProductShop\\Datasets\\categories.xml";
            string inputXmlCategories = File.ReadAllText(xmlCategoriesFilePath);

            var xmlCategoryProductFilePath = "D:\\Lcfr\\DEVELOPER\\Entity Framework Core Exercises\\08. XML Processing - Exercise\\ProductShop\\ProductShop\\Datasets\\categories-products.xml";
            string inputXmlCategoryProduct = File.ReadAllText(xmlCategoryProductFilePath);



            //Console.WriteLine(ImportUsers(db, inputXmlUsers));

            //Console.WriteLine(ImportProducts(db, inputXmlProducts));

            //Console.WriteLine(ImportCategories(db, inputXmlCategories));

            //Console.WriteLine(ImportCategoryProducts(db, inputXmlCategoryProduct));

            //Console.WriteLine(GetProductsInRange(db
            
            //Console.WriteLine(GetSoldProducts(db));

            //Console.WriteLine(GetCategoriesByProductsCount(db));

            Console.WriteLine(GetUsersWithProducts(db));
        }

        // Query 1. Import Users
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
         
            XmlSerializer serializer = new XmlSerializer(typeof(UsersDTO[]), 
                new XmlRootAttribute("Users"));

            using StringReader stringReader = new StringReader(inputXml);

            UsersDTO[] users = (UsersDTO[])serializer.Deserialize(stringReader);

            List<User> usersToAdd = users
                .Select(u => new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age
                })
                .ToList();

            context.Users.AddRange(usersToAdd);
            context.SaveChanges();

            return $"Successfully imported {usersToAdd.Count}";
        }

        // Query 2. Import Products
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductsDTO[]),
                new XmlRootAttribute("Products"));

            using StringReader stringReader = new StringReader(inputXml);

            ProductsDTO[] products = (ProductsDTO[])serializer.Deserialize(stringReader);

          var productsToAdd = products
                .Select(p => new Product()
                {
                    Name = p.Name,
                    Price = p.Price,
                    SellerId = p.SellerId,
                    BuyerId = p.BuyerId
                })
                .ToList();

            context.Products.AddRange(productsToAdd);
            context.SaveChanges();

            return $"Successfully imported {productsToAdd.Count}";
        }

        // Query 3. Import Categories
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoriesDTO[]),
                new XmlRootAttribute("Categories"));

            using StringReader stringReader = new StringReader(inputXml);

            CategoriesDTO[] categories = (CategoriesDTO[])serializer.Deserialize(stringReader);

            var categoriesToAdd = categories
                .Select(p => new Category()
                {
                    Name = p.Name
                })
                .ToList();

            context.Categories.AddRange(categoriesToAdd);
            context.SaveChanges();

            return $"Successfully imported {categoriesToAdd.Count}";
        }

        // Query 4. Import Categories and Products

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CategoryProductDTO[]),
                new XmlRootAttribute("CategoryProducts"));

            List<CategoryProduct> validCategoryProducts = new List<CategoryProduct>();

            using StringReader stringReader = new StringReader(inputXml);

            CategoryProductDTO[] categoriesProducts = (CategoryProductDTO[])serializer.Deserialize(stringReader);

            foreach (CategoryProductDTO categoryProductDTO in categoriesProducts)
            {

                if (!DoesProductAndCategoryIdExist(context, categoryProductDTO))
                {
                    continue;
                }

                CategoryProduct validCategoryProduct = new CategoryProduct()
                {
                    CategoryId = categoryProductDTO.CategoryId,
                    ProductId = categoryProductDTO.ProductId
                };

                validCategoryProducts.Add(validCategoryProduct);
            }

            context.CategoryProducts.AddRange(validCategoryProducts);
            context.SaveChanges();

            return $"Successfully imported {validCategoryProducts.Count}";


        }

        private static bool DoesProductAndCategoryIdExist(ProductShopContext context, CategoryProductDTO categoryProductDTO)
        {
            if (!context.Products.Any(p => p.Id == categoryProductDTO.ProductId) ||
                !context.Categories.Any(p => p.Id == categoryProductDTO.CategoryId))
            {
                return false;
            }

            return true;
        }

        // Query 5. Export Products In Range
        public static string GetProductsInRange(ProductShopContext context)
        {
            ProductInRangeExportDto[] productsInRangeExportDtos = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .Select(p => new ProductInRangeExportDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer != null ? string.Join(" ", p.Buyer.FirstName, p.Buyer.LastName) : null
                })
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ProductInRangeExportDto[]), new XmlRootAttribute("Products"));

            using StringWriter writer = new StringWriter();

            using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(xmlWriter, productsInRangeExportDtos, emptyNamespaces);

            return writer.ToString().Trim();
        }

        // Query 6. Export Sold Products
        public static string GetSoldProducts(ProductShopContext context)
        {
            UserExportDto[] usersToExport = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .Select(u => new UserExportDto()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold.Select(p => new ProductInRangeExportDto()
                    {
                        Name = p.Name,
                        Price = p.Price
                    }).ToArray()
                }).ToArray();


            XmlSerializer serializer = new XmlSerializer(typeof(UserExportDto[]), new XmlRootAttribute("Users"));

            using StringWriter writer = new StringWriter();

            using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(xmlWriter, usersToExport, emptyNamespaces);

            return writer.ToString().Trim();
        }

        // Query 7. Export Categories By Products Count
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            List<CategoryExportDto> categoriesAndProducts = context.Categories
                .Select(c => new CategoryExportDto
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Count > 0 ? c.CategoryProducts.Average(c => c.Product.Price) : 0,
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                })
                .OrderByDescending(p => p.Count)
                .ThenBy(p => p.TotalRevenue)
                .ToList();


            XmlSerializer serializer = new XmlSerializer(typeof(List<CategoryExportDto>), new XmlRootAttribute("Categories"));

            using StringWriter writer = new StringWriter();

            using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(xmlWriter, categoriesAndProducts, emptyNamespaces);

            return writer.ToString().Trim();
        }

        // Query 8. Export Users and Products
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            UsersAndProductsExportDto usersAndProducts = new UsersAndProductsExportDto()
            {
                Count = context.Users
                    .Where(u => u.ProductsSold.Count > 0).Count(),
                Users = context.Users
                    .Where(u => u.ProductsSold.Count > 0)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .Select(u => new UserExportDto()
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        Products = new SoldProductsExportDto()
                        {
                            Count = u.ProductsSold.Count,
                            Producst = u.ProductsSold.OrderByDescending(p => p.Price)
                                .Select(p => new ProductInRangeExportDto()
                                {
                                    Name = p.Name,
                                    Price = p.Price
                                }).ToArray()

                        }
                    })
                    .Take(10)
                    .ToArray()
            };

            XmlSerializer serializer = new XmlSerializer(typeof(UsersAndProductsExportDto), new XmlRootAttribute("Users"));

            using StringWriter writer = new StringWriter();

            using XmlWriter xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings { Indent = true });

            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(xmlWriter, usersAndProducts, emptyNamespaces);

            return writer.ToString().Trim();
        }


    }
}

