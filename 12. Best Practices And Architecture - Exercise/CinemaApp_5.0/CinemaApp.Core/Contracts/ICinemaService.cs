using CinemaApp.Core.Models;
using CinemaApp.Core.Models.DTOs;
using CinemaApp.Infrastructure.Data.Models;

namespace CinemaApp.Core.Contracts
{
    public interface ICinemaService
    {
        Task AddCinemaAsync(CinemaModel model);
        Task InsertAdditionalMovies(List<Movie> movies);
        List<Cinema> GetAllCinemas();
        List<CinemaHallsExportDto> GetAllCinemasByCityName(string cityName);
    }
}
