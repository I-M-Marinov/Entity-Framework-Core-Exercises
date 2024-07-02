using System.Text;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            // 2. Age Restriction -- 
            //var resultMinor = GetBooksByAgeRestriction(db, "MiNoR");
            //Console.WriteLine(resultMinor);

            // 3.Golden Books
            //var result = GetGoldenBooks(db);
            //Console.WriteLine(result);

            // 4. Get Books By Price
            var result = GetBooksByPrice(db);
            Console.WriteLine(result);

        }

        // 2. Age Restriction -- 
        /*  Return in a single string all book titles, each on a new line, that have an age restriction, equal to the given command.
            Order the titles alphabetically.
            Read input from the console in your main method and call your method with the necessary arguments. 
             Print the returned string to the console. Ignore the casing of the input. */
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            command = command.ToLower();
            var input = 0;

            if (command == "minor")
            {
                input = 0;
            }
            else if (command == "teen")
            {
                input = 1;
            }
            else if (command == "adult")
            {
                input = 2;
            }

            var bookTitles = context.Books
                .Where(b => b.AgeRestriction == (AgeRestriction)input)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, bookTitles);
        }


        /*
         3.	Golden Books
         Return in a single string the titles of the golden edition books that have less than 5000 copies, each on a new line. 
        Order them by BookId ascending.
        Call the GetGoldenBooks(BookShopContext context) method in your Main() and print the returned string to the console.

         */

         public static string GetGoldenBooks(BookShopContext context)
         {
             var copies = 5000;

            var goldenEditionBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < copies)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, goldenEditionBooks);

        }

        /*
         4.	Books by Price
         Return in a single string all titles and prices of books with a price higher than 40, 
        each on a new row in the format given below. Order them by price descending.
         */


        public static string GetBooksByPrice(BookShopContext context)
        {
            decimal price = 40;

            var booksByPrice = context.Books
                .Where(b => b.Price > price)
                .OrderByDescending(b => b.Price)
                .Select(b => new { b.Title, b.Price})
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in booksByPrice)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().Trim();
        }
    }
}



