using System.Globalization;
using System.Text;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.ImportDto;

namespace VaporStore.DataProcessor
{
    using System.ComponentModel.DataAnnotations;

    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Utilities;

    public static class Deserializer
    {
        public const string ErrorMessage = "Invalid Data";

        public const string SuccessfullyImportedGame = "Added {0} ({1}) with {2} tags";

        public const string SuccessfullyImportedUser = "Imported {0} with {1} cards";

        public const string SuccessfullyImportedPurchase = "Imported {0} for {1}";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportGamesDto[] deserializedGames = JsonConvert.DeserializeObject<ImportGamesDto[]>(jsonString)!;

            ICollection<Game> gamesToImport = new List<Game>();
            List<Developer> developers = new List<Developer>();
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();


            foreach (ImportGamesDto gameDto in deserializedGames)
            {
                if (!IsValid(gameDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime releaseDate;
                bool isReleaseDateValid = DateTime.TryParseExact(gameDto.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                if (!isReleaseDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (gameDto.Tags.Length == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Game g = new Game()
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = releaseDate
                };

                Developer gameDev = developers
                    .FirstOrDefault(d => d.Name == gameDto.Developer);

                if (gameDev == null)
                {
                    Developer newGameDev = new Developer()
                    {
                        Name = gameDto.Developer
                    };
                    developers.Add(newGameDev);

                    g.Developer = newGameDev;
                }
                else
                {
                    g.Developer = gameDev;
                }

                Genre gameGenre = genres
                    .FirstOrDefault(g => g.Name == gameDto.Genre);

                if (gameGenre == null)
                {
                    Genre newGenre = new Genre()
                    {
                        Name = gameDto.Genre
                    };

                    genres.Add(newGenre);
                    g.Genre = newGenre;
                }
                else
                {
                    g.Genre = gameGenre;
                }

                foreach (string tagName in gameDto.Tags)
                {
                    if (String.IsNullOrEmpty(tagName))
                    {
                        continue;
                    }

                    Tag gameTag = tags
                        .FirstOrDefault(t => t.Name == tagName);

                    if (gameTag == null)
                    {
                        Tag newGameTag = new Tag()
                        {
                            Name = tagName
                        };

                        tags.Add(newGameTag);
                        g.GameTags.Add(new GameTag()
                        {
                            Game = g,
                            Tag = newGameTag
                        });
                    }
                    else
                    {
                        g.GameTags.Add(new GameTag()
                        {
                            Game = g,
                            Tag = gameTag
                        });
                    }
                }

                if (g.GameTags.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                gamesToImport.Add(g);
                sb.AppendLine(String.Format(SuccessfullyImportedGame, g.Name, g.Genre.Name, g.GameTags.Count));
            }

            context.Games.AddRange(gamesToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ICollection<User> usersToImport = new List<User>();

            ImportUsersDto[] deserializedUsers = JsonConvert.DeserializeObject<ImportUsersDto[]>(jsonString)!;

            foreach (ImportUsersDto userDto in deserializedUsers)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                User newUser = new User()
                {
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Age = userDto.Age
                };

                ICollection<Card> cardsToImport = new List<Card>();
                bool allCardsValid = true;

                foreach (ImportCardsDto cardDto in userDto.Cards)
                {
                    if (!IsValid(cardDto))
                    {
                        allCardsValid = false;
                        break;
                    }

                    CardType type;
                    if (cardDto.Type == "Debit")
                    {
                        type = CardType.Debit;
                    }
                    else if (cardDto.Type == "Credit")
                    {
                        type = CardType.Credit;
                    }
                    else
                    {
                        allCardsValid = false;
                        break;
                    }

                    Card newCard = new Card()
                    {
                        Number = cardDto.Number,
                        Cvc = cardDto.CVC,
                        Type = type
                    };

                    cardsToImport.Add(newCard);
                }

                if (!allCardsValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                newUser.Cards = cardsToImport;
                usersToImport.Add(newUser);
                sb.AppendLine(string.Format(SuccessfullyImportedUser, newUser.Username, newUser.Cards.Count));
            }

            context.Users.AddRange(usersToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Purchases";

            ICollection<Purchase> purchasesToImport = new List<Purchase>();

            ImportPurchasesDto[] deserializedPurchases = xmlHelper.Deserialize<ImportPurchasesDto[]>(xmlString, xmlRoot);

            foreach (ImportPurchasesDto purchaseDto in deserializedPurchases)
            {
                bool isValidDate = DateTime.TryParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date);

                if (!IsValid(purchaseDto) || !isValidDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Title);
                var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);

                if (game == null || card == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                PurchaseType purchaseType;
                if (purchaseDto.Type == "Retail")
                {
                    purchaseType = PurchaseType.Retail;
                }
                else if (purchaseDto.Type == "Digital")
                {
                    purchaseType = PurchaseType.Digital;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Purchase newPurchase = new Purchase()
                {
                    Game = game,
                    Type = purchaseType,
                    Card = card,
                    Date = date,
                    ProductKey = purchaseDto.ProductKey
                };

                purchasesToImport.Add(newPurchase);
                sb.AppendLine(string.Format(SuccessfullyImportedPurchase, newPurchase.Game.Name, newPurchase.Card.User.Username));
            }

            context.Purchases.AddRange(purchasesToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}