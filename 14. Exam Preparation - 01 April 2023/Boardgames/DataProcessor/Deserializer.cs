using System.Text;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Boardgames.Utilities;

namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
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
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
