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

            //var resultMinor = GetBooksByAgeRestriction(db, "MiNoR");
            //Console.WriteLine(resultMinor);

            var result = GetGoldenBooks(db);
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
            var goldenEditionBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, goldenEditionBooks);

        }

    }
}



