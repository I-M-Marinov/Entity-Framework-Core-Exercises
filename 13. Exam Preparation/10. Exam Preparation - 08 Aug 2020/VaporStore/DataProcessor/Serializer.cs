using System.Text;
using VaporStore.DataProcessor.ExportDto;

namespace VaporStore.DataProcessor
{ 
    using Data;
    using Newtonsoft.Json;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    Games = g.Games
                        .Where(game => game.Purchases.Any())
                        .Select(game => new
                        {
                            Id = game.Id,
                            Name = game.Name,
                            Developer = game.Developer.Name,
                            Tags = string.Join(", ", game.GameTags.Select(gt => gt.Tag.Name)),
                            Players = game.Purchases.Count
                        })
                        .ToList()
                })
                .ToList();

            var genreDtos = genres
                .Select(g => new ExportGenresDto
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .OrderByDescending(game => game.Players)
                        .ThenBy(game => game.Id)
                        .Select(game => new ExportGameDto
                        {
                            Id = game.Id,
                            Title = game.Name,
                            Developer = game.Developer,
                            Tags = game.Tags,
                            Players = game.Players
                        })
                        .ToArray(),
                    TotalPlayers = g.Games.Sum(game => game.Players)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToArray();

            return JsonConvert.SerializeObject(genreDtos, Formatting.Indented);

        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string purchaseType)
        {
            throw new NotImplementedException();
        }
    }
}