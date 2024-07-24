using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ExportDto;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Utilities;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Creators";

            ExportCreatorDto[] creatorsToExport = context.Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDto()
                {
                    BoardgamesCount = c.Boardgames.Count,
                    CreatorName = c.FirstName + " " + c.LastName,
                    Boardgames = c.Boardgames
                        .Select(bg => new ExportBoardgameToXmlDto()
                        {
                            BoardgameName = bg.Name,
                            BoardgameYearPublished = bg.YearPublished
                        })
                        .OrderBy(bg => bg.BoardgameName)
                        .ToArray()
                })
                .OrderByDescending(c => c.BoardgamesCount)
                .ThenBy(c => c.CreatorName)
                .ToArray();

            return xmlHelper.Serialize(creatorsToExport, xmlRoot);
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            ExportSellerDto[] sellersToExport = context.Sellers
                .Where(s => s.BoardgamesSellers.Any(bg => bg.Boardgame.YearPublished >= year))
                .Where(s => s.BoardgamesSellers.Any(bg => bg.Boardgame.Rating <= rating))
                .Select(s => new ExportSellerDto()
                {
                    Name = s.Name,
                    Website = s.Website,
                    Boardgames = s.BoardgamesSellers
                        .Where(bs => bs.Boardgame.YearPublished >= year)
                        .Where(bs => bs.Boardgame.Rating <= rating)
                        .OrderByDescending(bs => bs.Boardgame.Rating)
                        .ThenBy(bs => bs.Boardgame.Name)
                        .Select(bs => new ExportBoardgameDto()
                        {
                            Name = bs.Boardgame.Name,
                            Rating = bs.Boardgame.Rating,
                            Mechanics = bs.Boardgame.Mechanics,
                            Category = bs.Boardgame.CategoryType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(s => s.Boardgames.Count)
                .ThenBy(s => s.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(sellersToExport, Formatting.Indented);
        }
    }
}