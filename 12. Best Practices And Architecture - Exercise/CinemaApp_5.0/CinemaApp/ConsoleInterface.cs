using CinemaApp.Core.Contracts;
using CinemaApp.Core.Models;
using CinemaApp.Infrastructure.Data;
using CinemaApp.Infrastructure.Data.Models;
using CinemaApp.Infrastructure.Data.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public static class ConsoleInterface
{
    public static void Run(ICinemaService cinemaService, IMovieService movieService)
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("-----------Welcome to CinemaApp!------------");
        Console.WriteLine("--------------------------------------------");

        while (true)
        {
            PrintSelectionOptions();
            string? input = Console.ReadLine();

            if (input == "0")
            {
                List<Movie> extractedMovies = ExtractAdditionalMoviesFromJson();

                if(extractedMovies == null)
                {
                    continue;
                }

                cinemaService.InsertAdditionalMovies(extractedMovies);
                Console.WriteLine($"{extractedMovies.Count} movies have been inserted successfully.");
            }
            else if (input == "x")
            {
                Console.WriteLine("Goodbye !");
                return;
            }
            else if (input == "1")
            {
                List<Movie> movies = movieService.GetAllMovies();

                if (movies.Count == 0)
                {
                    Console.WriteLine("No movies available.");
                    continue;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Movies:");

                    foreach (Movie movie in movies)
                    {
                        stringBuilder.AppendLine($"Title: {movie.Title}");
                        stringBuilder.AppendLine($"Genre: {movie.Genre}");

                        if(movie.Description != null)
                        {
                            stringBuilder.AppendLine($"Description: {movie.Description}");
                        }
                        else
                        {
                            stringBuilder.AppendLine("Description: N/A");
                        }
                        stringBuilder.AppendLine();
                    }
                    Console.WriteLine(stringBuilder.ToString().Trim());
                }
            }
            else if (input == "2")
            {
                List<Cinema> cinemas = cinemaService.GetAllCinemas();

                if (cinemas.Count == 0)
                {
                    Console.WriteLine("No cinemas available.");
                    continue;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Cinemas:");

                    foreach (Cinema cinema in cinemas)
                    {
                        stringBuilder.AppendLine($"Name: {cinema.Name}");
                        stringBuilder.AppendLine($"Address: {cinema.Address}");
                        stringBuilder.AppendLine();
                    }

                    Console.WriteLine(stringBuilder.ToString().Trim());
                }
            }
            else if (input == "3")
            {
                var animationMovies = movieService.GetAllMovies().Where(m => m.Genre == Enum.Parse<Genre>("Animation")).ToList();

                if (animationMovies.Count == 0)
                {
                    Console.WriteLine("No animation movies available.");
                    continue;
                }

                StringBuilder sb = new();

                sb.AppendLine($"Animation Movies available:");
                sb.AppendLine();

                foreach (var movie in animationMovies)
                {
                    sb.AppendLine($"{movie.Title}");
                    sb.AppendLine($"--{movie.Genre}");
                    sb.AppendLine($"---{movie.Description}");
                    sb.AppendLine();
                }

                Console.WriteLine(sb);

            }
            else if (input == "4")
            {
                var actionMovies = movieService.GetAllMovies().Where(m => m.Genre == Enum.Parse<Genre>("Action")).ToList();

                if (actionMovies.Count == 0)
                {
                    Console.WriteLine("No animation movies available.");
                    continue;
                }

                StringBuilder sb = new();

                sb.AppendLine($"Action Movies available:");
                sb.AppendLine();

                foreach (var movie in actionMovies)
                {
                    sb.AppendLine($"{movie.Title}");
                    sb.AppendLine($"--{movie.Genre}");
                    sb.AppendLine($"---{movie.Description}");
                    sb.AppendLine();
                }

                Console.WriteLine(sb);

            }
            else if (input == "5")
            {
                Console.WriteLine("Please enter the city name you are looking for ?");
                var city = Console.ReadLine();

                var cinemas = cinemaService.GetAllCinemasByCityName(city);

                if (cinemas.Count == 0)
                {
                    Console.WriteLine("There are no cinemas in this city!");
                    continue;
                }

                Console.WriteLine();
                if (cinemas.Count == 1)
                {
                    Console.WriteLine($"There is {cinemas.Count} cinema in the city of {city}");
                }
                else
                {
                    Console.WriteLine($"There are {cinemas.Count} cinemas in the city of {city}");
                }
                Console.WriteLine();

                foreach (var cinema in cinemas)
                {
                    Console.WriteLine($"-- {cinema.Name} is located in {cinema.Address} and has {cinema.NumberOfHalls} halls.");
                    
                }
                Console.WriteLine();
            }
            else if (input == "6")
            {
                Console.WriteLine("Please enter the genre name you are looking for ?");
                var genre = Console.ReadLine();

                
                var validGenres = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList(); // get a list of all the valid Genres 

                try
                {
                    if (!validGenres.Contains(Enum.Parse<Genre>(genre)))
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{genre} is not a valid genre name!");
                    continue;
                }

                var movies = movieService.GetAllMoviesFromGenre(genre);

                if (movies.Count == 0)
                {
                    Console.WriteLine($"There are no movies from the {genre} genre!");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine($"There are {movies.Count} movies from the {genre} genre.");
                Console.WriteLine();

                foreach (var movie in movies)
                {
                    Console.WriteLine($"-Title: {movie.Title}");
                    Console.WriteLine($"--- Description: {movie.Description}");
                    Console.WriteLine();
                }
            }

            else
            {
                Console.WriteLine("Invalid option chosen! Try again...");
                continue;
            }
        } 
    }

    private static List<Movie> ExtractAdditionalMoviesFromJson()
    {
        string jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "Datasets", "additionalMovies.json");

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            var movieModels = JsonSerializer.Deserialize<MovieModel[]>(jsonData);

            List<Movie> movies = new List<Movie>();

            if (movieModels != null && movieModels.Any())
            {
                foreach (var movieModel in movieModels)
                {
                    if (!IsValid(movieModel))
                    {
                        Console.WriteLine("Invalid movie.");
                        continue;
                    }

                    Genre genre;
                    if (!Enum.TryParse(movieModel.Genre, out genre))
                    {
                        Console.WriteLine("Invalid movie. Not an existing genre!");
                        continue;
                    }

                    Movie movie = new Movie()
                    {
                        Title = movieModel.Title,
                        Genre = Enum.Parse<Genre>(movieModel.Genre),
                        Description = movieModel.Description
                    };

                    movies.Add(movie);
                }
                
                return movies;
            }
            else
            {
                Console.WriteLine("No movies found in the JSON file.");
                return null;
            }
        }
        else
        {
            Console.WriteLine("File not found.");
            return null;
        }
    }
    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
    private static void PrintSelectionOptions()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Select an option:");
        sb.AppendLine("x => EXIT");
        sb.AppendLine("0. Insert additional movies from JSON");
        sb.AppendLine("1. List all movies");
        sb.AppendLine("2. List all cinemas");
        sb.AppendLine("3. List all animation movies");
        sb.AppendLine("4. List all action movies");
        sb.AppendLine("5. List all cinemas in a certain city");
        sb.AppendLine("6. List all movies in a certain genre");

        Console.WriteLine(sb.ToString().Trim());
    }
}