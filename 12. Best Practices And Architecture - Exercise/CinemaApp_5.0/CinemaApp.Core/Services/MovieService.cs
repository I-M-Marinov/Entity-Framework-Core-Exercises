using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApp.Core.Contracts;
using CinemaApp.Infrastructure.Data.Common;
using CinemaApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace CinemaApp.Core.Services
{
    public class MovieService: IMovieService
    {
        private readonly IRepository _repository;

        public MovieService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Movie> GetAllMovies()
        {
            var movies = _repository.All<Movie>().ToList();

            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var json = JsonConvert.SerializeObject(movies, serializerSettings);

            File.WriteAllText("listOfMovies.json", json);

            return movies;
        }
    }
}
