using System.Text;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Boardgames.Utilities;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using Boardgames.Data;
   
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();

            const string xmlRoot = "Creators";

            ICollection<Creator> creatorsToImport = new List<Creator>();

            ImportCreatorsDto[] deserializedCreators = xmlHelper.Deserialize<ImportCreatorsDto[]>(xmlString, xmlRoot);

            foreach (var creatorDto in deserializedCreators)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                ICollection<Boardgame> boardgamesToImport = new List<Boardgame>();

                foreach (var boardgameDto in creatorDto.Boardgames)
                {
                    if (!IsValid(boardgameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame newBoardgame = new Boardgame()
                    {
                        Name = boardgameDto.Name,
                        Rating = boardgameDto.Rating,
                        YearPublished = boardgameDto.YearPublished,
                        CategoryType = (CategoryType)boardgameDto.CategoryType,
                        Mechanics = boardgameDto.Mechanics
                    };

                    boardgamesToImport.Add(newBoardgame);
                }

                Creator newCreator = new Creator()
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName,
                    Boardgames = boardgamesToImport
                };

                creatorsToImport.Add(newCreator);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, creatorDto.FirstName, creatorDto.LastName, newCreator.Boardgames.Count));

            }

            context.Creators.AddRange(creatorsToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Seller> sellersToImport = new List<Seller>();
            ICollection<int> validBoardgameIds = context.Boardgames.Select(b => b.Id).ToList();

            ImportSellerDto[] deserializedSellers = JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString)!;

            foreach (ImportSellerDto sellersDto in deserializedSellers)
            {
                if (!IsValid(sellersDto) || !IsValidWebsite(sellersDto.Website))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller newSeller = new Seller()
                {
                    Name = sellersDto.Name,
                    Address = sellersDto.Address,
                    Country = sellersDto.Country,
                    Website = sellersDto.Website
                };

                ICollection<BoardgameSeller> boardgamesSellersToImport = new List<BoardgameSeller>();

                foreach (var boardgameId in sellersDto.Boardgames.Distinct())
                {
                    if (!validBoardgameIds.Any(b => b == boardgameId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    BoardgameSeller newBoardgameSeller = new BoardgameSeller()
                    {
                        Seller = newSeller,
                        BoardgameId = boardgameId
                    };

                    boardgamesSellersToImport.Add(newBoardgameSeller);
                }

                newSeller.BoardgamesSellers = boardgamesSellersToImport;

                sellersToImport.Add(newSeller);

                sb.AppendLine(string.Format(SuccessfullyImportedSeller, newSeller.Name, newSeller.BoardgamesSellers.Count));

            }

            context.Sellers.AddRange(sellersToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static bool IsValidWebsite(string website)
        {
            const string WebsitePattern = @"^www\.[A-Za-z0-9-]+\.com$";
            return Regex.IsMatch(website, WebsitePattern);
        }
    }
}
