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

            var resultMinor = GetBooksByAgeRestriction(db, "MiNoR");
            Console.WriteLine(resultMinor);
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

    }
}



