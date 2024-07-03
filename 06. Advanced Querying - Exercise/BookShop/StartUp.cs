using System.Diagnostics.CodeAnalysis;
using System.Text;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace BookShop
{
    using Data;
    using Initializer;
    using System.Diagnostics;
    using System.Globalization;

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
            //var result = GetBooksByPrice(db);
            //Console.WriteLine(result);

            // 5. Not Released In
            //var result = GetBooksNotReleasedIn(db, 2000);
            //Console.WriteLine(result);

            // 6. Book Titles by Category
            //var result = GetBooksByCategory(db, "horror mystery drama");
            //Console.WriteLine(result);

            // 7. Released Before Date
            //var result = GetBooksReleasedBefore(db, "12-04-1992");
            //Console.WriteLine(result);

            //// 8. Author Search
            //var result = GetAuthorNamesEndingIn(db, "dy");
            //Console.WriteLine(result);

            // 8. Author Search
            var result = GetBookTitlesContaining(db, "sK");
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

        /*
         5.	Not Released In
        Return in a single string with all titles of books that are NOT released in a given year. Order them by bookId 
         */

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var sqlQueryRaw = "SELECT * FROM Books WHERE YEAR(ReleaseDate) != {0}";

            var booksNotReleasedIn = context.Books
                .FromSqlRaw(sqlQueryRaw, year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();


            var sb = new StringBuilder();

            foreach (var bookTitle in booksNotReleasedIn)
            {
                sb.AppendLine($"{bookTitle}");
            }

            return sb.ToString().Trim();

            // Solution that works for SoftUni Judge, but the solution above works as well 

            //var booksNotReleasedIn = context.Books
            //    .Where(b => b.ReleaseDate.Value.Year != year)
            //    .Select(b => new { b.Title, b.BookId })
            //    .OrderBy(b => b.BookId)
            //    .ToList();


            //return string.Join(Environment.NewLine, booksNotReleasedIn.Select(b => b.Title));

        }

        /* 6. Book Titles by Category
         Return in a single string the titles of books by a given list of categories. 
        The list of categories will be given in a single line separated by one or more spaces. 
        Ignore casing. Order by title alphabetically.
         */

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = Regex.Split(input.Trim(), @"\s+")
                .Select(c => c.ToLower())  
                .ToList();

            var books = context.Books
                .Where(b => b.BookCategories
                    .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)  
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        /*
          7. Released Before Date
            Return the title, edition type and price of all books that are released before a given date. 
            The date will be a string in the format "dd-MM-yyyy".
            Return all of the rows in a single string, ordered by release date (descending).
         */

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            string format = "dd-MM-yyyy";
            DateTime dateFormatted = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            
            var books = context.Books
                .Where(b => b.ReleaseDate < dateFormatted)
                .Select(b => new{b.Title, b.EditionType, b.Price, b.ReleaseDate})
                .OrderByDescending(b => b.ReleaseDate)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return sb.ToString().Trim();
        }

        /*
         8. Author Search 
            Return the full names of authors, whose first name ends with a given string.
            Return all names in a single string, each on a new row, ordered alphabetically.
         */

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            //string sqlQuery = "SELECT TOP 1000 FirstName, LastName FROM Authors WHERE FirstName LIKE {0} ORDER BY FirstName, LastName";
            //string likePattern = "%" + input;

            //var authors = context.Authors
            //    .FromSqlRaw(sqlQuery, likePattern)
            //    .Select(a => new { a.FirstName, a.LastName})
            //    .ToList();

            //var sb = new StringBuilder();

            //foreach (var author in authors)
            //{
            //    sb.AppendLine($"{author.FirstName} {author.LastName}");
            //}

            //return sb.ToString().Trim();

            var authors = context.Authors
                .Where(a => EF.Functions.Like(a.FirstName, "%" + input))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => new { a.FirstName, a.LastName })
                .ToList();

            var sb = new StringBuilder();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName}");
            }

            return sb.ToString().Trim();
        }

        /*
         9. Book Search 
            Return the titles of the book, which contain a given string. Ignore casing.
            Return all titles in a single string, each on a new row, ordered alphabetically.
        */

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Title, "%" + input.ToLower() + "%"))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);

        }
    }
}



