﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

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

            string jsonData = File.ReadAllText(filePath);
            string jsonDataCategories = File.ReadAllText(filePathCategories);
            string jsonDataProducts = File.ReadAllText(filePathProducts);


            // ImportUsers(db, jsonData);

            //Console.WriteLine(ImportCategories(db, jsonDataCategories));

            Console.WriteLine(ImportProducts(db, jsonDataProducts));
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

            return $"Successfully imported {validCategories.Count} categories";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(inputJson);

            var validProducts = products.Where(p => p.BuyerId != null).ToList();

            context.Products.AddRange(validProducts);
            context.SaveChanges();

            return $"Successfully imported {products.Count} products";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {




            return null;
        }
    }
}