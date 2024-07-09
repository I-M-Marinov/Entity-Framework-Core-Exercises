using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Models;
using System.Text.Json;

namespace ProductShop
{
    public class StartUp
    {


        public static void Main()
        {
            using var db = new ProductShopContext();

            string filePath =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\users.json";
            string filePathCategories =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\categories.json";

            string filePathProducts =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\products.json";

            string filePathCategoriesProducts =
                "C:\\Users\\Ivan Marinov\\Desktop\\Exercise\\07.JSON-Processing-Exercises-ProductShop-6.0\\ProductShop\\Datasets\\categories-products.json";

            string jsonData = File.ReadAllText(filePath);
            string jsonDataCategories = File.ReadAllText(filePathCategories);
            string jsonDataProducts = File.ReadAllText(filePathProducts);
            string jsonDataCategoriesProducts = File.ReadAllText(filePathCategoriesProducts);


            //Console.WriteLine(ImportUsers(db, jsonData));

            //Console.WriteLine(ImportCategories(db, jsonDataCategories));

            //Console.WriteLine(ImportProducts(db, jsonDataProducts));
            
            //Console.WriteLine(ImportCategoryProducts(db, jsonDataCategoriesProducts));

            Console.WriteLine(GetProductsInRange(db));
        }


        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(inputJson);
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count} users";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);

            var validCategories = categories.Where(c => c.Name != null).ToList();

            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count}";
        }
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoriesProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);


            context.CategoriesProducts.AddRange(categoriesProducts);
            context.SaveChanges();

            return $"Successfully imported {categoriesProducts.Count}";
        }



        public static string GetProductsInRange(ProductShopContext context)
        {

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = (p.Seller.FirstName + " " + p.Seller.LastName)
                });

            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy() 
                }
            };

            var json = JsonConvert.SerializeObject(products, serializerSettings);

            // Write to file
            File.WriteAllText("products-in-range.json", json);

            return json;
        }
    }
}