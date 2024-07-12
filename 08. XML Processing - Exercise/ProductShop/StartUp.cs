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


           Console.WriteLine(ImportUsers(db, inputXmlUsers));

           Console.WriteLine(ImportProducts(db, inputXmlProducts));
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
                    BuyerId = p.BuyerId.HasValue ? p.BuyerId.Value : 0
                })
                .ToList();

            context.Products.AddRange(productsToAdd);
            context.SaveChanges();

            return $"Successfully imported {productsToAdd.Count}";
        }
    }
}
