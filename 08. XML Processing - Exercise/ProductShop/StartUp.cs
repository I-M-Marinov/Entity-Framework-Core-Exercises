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



            Console.WriteLine(ImportUsers(db, inputXmlUsers));
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

            return $"Successfully imported {usersToAdd.Count}.";
        }
    }
}
