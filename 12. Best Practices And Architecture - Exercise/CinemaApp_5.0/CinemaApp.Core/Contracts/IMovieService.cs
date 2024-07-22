using CinemaApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Core.Contracts
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        List<Movie> GetAllMoviesFromGenre(string genre);
    }
}
