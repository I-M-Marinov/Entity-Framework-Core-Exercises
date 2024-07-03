using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace BookShop
{
    using BookShop.Models;
    using Data;
    using Initializer;
    using System.Diagnostics;
    using System.Globalization;

    public class StartUp
    {
        [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db); // ----- UNCOMMENT IF YOU WANT TO RESET THE DATABASE ( ensuring it will always be in it's original state ) 

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

            // 9. Book Search
            //var result = GetBookTitlesContaining(db, "sK");
            //Console.WriteLine(result);

            // 10. Book Search By Author
            //var result = GetBooksByAuthor(db, "R");
            //Console.WriteLine(result);

            // 11. Count Books
            //var result = CountBooks(db, 40);
            //Console.WriteLine(result);

            // 12. Total Book Copies
            //var result = CountCopiesByAuthor(db);
            //Console.WriteLine(result);

            // 13. Profit By Category
            //var result = GetTotalProfitByCategory(db);
            //Console.WriteLine(result);

            // 14. Most Recent Books
            //var result = GetMostRecentBooks(db);
            //Console.WriteLine(result);

            // 15. Increase Prices
            // IncreasePrices(db);

            // 16. Remove Books
            Console.WriteLine(RemoveBooks(db));

        }

        /* 2. Age Restriction
         Return in a single string all book titles, each on a new line, that have an age restriction, equal to the given command.
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
                .Select(b => new {b.Title, b.EditionType, b.Price, b.ReleaseDate})
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

        /*
          10. Book Search by Author
            Return all titles of books and their authors' names for books, 
            which are written by authors whose last names start with the given string.
            Return a single string with each title on a new row. Ignore casing. Order by BookId ascending.
         */

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var result = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName,  input.ToLower() + "%"))
                .OrderBy(b => b.BookId)
                .Select(b => new {b.Title, b.Author.FirstName, b.Author.LastName})
                .ToList();

           var sb = new StringBuilder();

           foreach (var property in result)
           {
               sb.AppendLine($"{property.Title} ({property.FirstName} {property.LastName})");
           }

           return sb.ToString().Trim();
        }

        /*
          11. Count Books
            Return the number of books, which have a title longer than the number given as an input.
         */

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int count = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return count;
        }

        /*
          12. Total Book Copies
            Return the total number of book copies for each author. Order the results descending by total book copies.
            Return all results in a single string, each on a new line.
        */

        public static string CountCopiesByAuthor(BookShopContext context)
        {

                                                        /* FIRST SOLUTION OF THIS ISSUE ( Judge does not like it ) */

            //    var groupedBooks = context.Books
            //        .GroupBy(b => new { b.Author.FirstName, b.Author.LastName })
            //        .OrderByDescending(g => g.Sum(b => b.Copies))
            //        .Select(g => new
            //        {
            //            AuthorFirstName = g.Key.FirstName,
            //            AuthorLastName = g.Key.LastName,
            //            TotalCopies = g.Sum(b => b.Copies)
            //        })
            //        .ToList();

            //    var sb = new StringBuilder();

            //    foreach (var group in groupedBooks)
            //    {
            //        sb.AppendLine($"{group.AuthorFirstName} {group.AuthorLastName} - {group.TotalCopies}");
            //    }

            //    return sb.ToString().Trim();
            //

                                                    /*SECOND SOLUTION OF THIS ISSUE ( Judge does not like it AS WELL ) */

            //var totalBookCopies = context.Books
            //    .GroupBy(b => new { b.Author.FirstName, b.Author.LastName })
            //    .OrderByDescending(g => g.Sum(b => b.Copies))
            //    .Select(g => $"{g.Key.FirstName} {g.Key.LastName} - {g.Sum(b => b.Copies)}")
            //    .ToList();

            //var sb = new StringBuilder();

            //foreach (var result in totalBookCopies)
            //{
            //    sb.AppendLine(result);
            //}

            //return sb.ToString().Trim();

                                                                    /* THIRD SOLUTION ... */

            //var booksWithAuthors = context.Books
            //    .Include(b => b.Author)
            //    .ToList();

            //var groupedBooks = booksWithAuthors
            //    .GroupBy(b => new { b.Author.FirstName, b.Author.LastName })
            //    .OrderByDescending(g => g.Sum(b => b.Copies))
            //    .ToList();

            //var sb = new StringBuilder();
            //foreach (var group in groupedBooks)
            //{
            //    var totalCopies = group.Sum(b => b.Copies);
            //    sb.AppendLine($"{group.Key.FirstName} {group.Key.LastName} - {totalCopies}");
            //}

            //return sb.ToString().Trim();

/* FOURTH SOLUTION ... FINALLY JUDGE gives us the points ... */

             var authorsCopies = context.Authors
                 .Select(a => new
                 {
                     a.FirstName,
                     a.LastName,
                     Copies = a.Books.Sum(b => b.Copies)
                 })
                 .OrderByDescending(a => a.Copies)
                 .ToList();

             return string.Join(Environment.NewLine,
                 authorsCopies.Select(ac => $"{ac.FirstName} {ac.LastName} - {ac.Copies}"));
        }


        /*
          13. Total Book Copies
            Return the total profit of all books by category. 
            Profit for a book can be calculated by multiplying its number of copies by the price per single book. 
            Order the results by descending by total profit for a category and ascending by category name. 
            Print the total profit formatted to the second digit.
        */

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            //var categoryProfits = context.BooksCategories
            //    .Include(bc => bc.Category)
            //    .Include(bc => bc.Book)
            //    .GroupBy(bc => bc.Category.Name)
            //    .Select(g => new
            //    {
            //        CategoryName = g.Key,
            //        TotalProfit = g.Sum(bc => bc.Book.Price * bc.Book.Copies)
            //    })
            //    .OrderByDescending(g => g.TotalProfit)
            //    .ThenBy(g => g.CategoryName)
            //    .ToList();

            // return string.Join(Environment.NewLine,
            //    categoryProfits.Select(cp => $"{cp.CategoryName} ${cp.TotalProfit:F2}"));

            var categoriesByProfits = context.Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks.Sum(cb => cb.Book.Price * cb.Book.Copies)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .ToList();


            return string.Join(Environment.NewLine,
                categoriesByProfits.Select(cbp => $"{cbp.Name} ${cbp.TotalProfit:F2}"));
        }

        /*
          14. Most Recent Books
            Get the most recent books by categories. 
            The categories should be ordered by name alphabetically. 
            Only take the top 3 most recent books from each category – ordered by release date (descending). 
            Select and print the category name and for each book – its title and release year.
        */

        public static string GetMostRecentBooks(BookShopContext context)
        {
            
            var categoriesWithThreeLatestBooks = context.Categories
                .Select(c => new
                {
                    c.Name,
                    MostRecentBooks = c.CategoryBooks
                        .OrderByDescending(b => b.Book.ReleaseDate)
                        .Take(3)
                        .Select(b => $"{b.Book.Title} ({b.Book.ReleaseDate.Value.Year})")
                        .ToList()
                        
                }).OrderBy(c => c.Name)
                .ToList();


            var sb = new StringBuilder();

            foreach (var category in categoriesWithThreeLatestBooks)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.MostRecentBooks)
                {
                    sb.AppendLine(book);
                }
            }

            return sb.ToString().Trim();
        }

        /*
          15. Increase Prices
            Increase the prices of all books released before 2010 by 5.
        */

        public static void IncreasePrices(BookShopContext context)
        {
            var booksToUpdate = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in booksToUpdate)
            {
                book.Price += 5;
            }

            context.SaveChanges();

        }

        /*
          16. Remove Books
            Remove all books, which have less than 4200 copies. Return an int - the number of books that were deleted from the database.
        */

        public static int RemoveBooks(BookShopContext context)
        {
            var booksToDelete = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var counter = 0;
            foreach (var book in booksToDelete)
            {
                context.Books.Remove(book);
                counter++;

            }

            context.SaveChanges();

            return counter;
        }

    }
}



