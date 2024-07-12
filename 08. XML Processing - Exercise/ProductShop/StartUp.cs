using ProductShop.Data;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            var xmlUsersFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\users.xml";
            string inputXmlUsers = File.ReadAllText(xmlUsersFilePath);

            var xmlProductsFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\products.xml";
            string inputXmlProducts = File.ReadAllText(xmlProductsFilePath);

            var xmlCategoriesFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\categories.xml";
            string inputXmlCategories = File.ReadAllText(xmlCategoriesFilePath);

            var xmlCategoryProductFilePath = "C:\\Users\\Ivan Marinov\\Desktop\\XML Exercise\\08.XML-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\categories-products.xml";
            string inputXmlCategoryProduct = File.ReadAllText(xmlCategoryProductFilePath);



            //Console.WriteLine(ImportUsers(db, inputXmlUsers));

            //Console.WriteLine(ImportProducts(db, inputXmlProducts));

            //Console.WriteLine(ImportCategories(db, inputXmlCategories));

            Console.WriteLine(ImportCategories(db, inputXmlCategoryProduct));
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


    }
}
