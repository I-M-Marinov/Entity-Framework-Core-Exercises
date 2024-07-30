using System.Text;
using Theatre.Data.Models;
using Theatre.Data.Models.Enums;
using Theatre.DataProcessor.ImportDto;
using Theatre.Utilities;

namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using Theatre.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";



        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Plays";

            ICollection<Play> playsToImport = new List<Play>();

            ImportPlaysDto[] deserializedPlays = xmlHelper.Deserialize<ImportPlaysDto[]>(xmlString, xmlRoot);

            foreach (ImportPlaysDto playDto in deserializedPlays)
            {
                var IsItValidDuration = TimeSpan.TryParse(playDto.Duration, out TimeSpan duration);

                if (!IsValid(playDto) || !IsItValidDuration)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Genre genre;

                if (playDto.Genre == "Drama")
                {
                    genre = Genre.Drama;
                }
                else if (playDto.Genre == "Comedy")
                {
                    genre = Genre.Comedy;
                }
                else if (playDto.Genre == "Romance")
                {
                    genre = Genre.Romance;
                }
                else if (playDto.Genre == "Musical")
                {
                    genre = Genre.Musical;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Play newPlay = new Play()
                {
                    Title = playDto.Title,
                    Duration = duration,
                    Rating = (float)playDto.Raiting,
                    Genre = genre,
                    Description = playDto.Description,
                    Screenwriter = playDto.Screenwriter
                };

                playsToImport.Add(newPlay);
                sb.AppendLine(string.Format(SuccessfulImportPlay, newPlay.Title, newPlay.Genre.ToString(), newPlay.Rating));

            }

            context.Plays.AddRange(playsToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Casts";

            ICollection<Cast> castsToImport = new List<Cast>();

            ImportCastsDto[] deserializedCasts = xmlHelper.Deserialize<ImportCastsDto[]>(xmlString, xmlRoot);

            foreach (ImportCastsDto castDto in deserializedCasts)
            {

                if (!IsValid(castDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isMainCharacter = false;

                if (castDto.IsMainCharacter == "true")
                {
                    isMainCharacter = true;
                }
                else if (castDto.IsMainCharacter == "false")
                {
                    isMainCharacter = false;
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Cast newCast = new Cast()
                {
                    FullName = castDto.FullName,
                    IsMainCharacter = isMainCharacter,
                    PhoneNumber = castDto.PhoneNumber,
                    PlayId = castDto.PlayId
                };

                var typeCharacter = newCast.IsMainCharacter == true ? "main" : "lesser";

                castsToImport.Add(newCast);
                sb.AppendLine(string.Format(SuccessfulImportActor, newCast.FullName, typeCharacter));
            }

            context.Casts.AddRange(castsToImport);
            context.SaveChanges();

            return sb.ToString();

        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Data.Models.Theatre> theatresToImport = new List<Data.Models.Theatre>();


            ImportTheatresDto[] deserializedTheatres = JsonConvert.DeserializeObject<ImportTheatresDto[]>(jsonString)!;

            foreach (ImportTheatresDto theatreDto in deserializedTheatres)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Data.Models.Theatre newTheatre = new Data.Models.Theatre()
                {
                    Name = theatreDto.Name,
                    NumberOfHalls = (sbyte)theatreDto.NumberOfHalls,
                    Director = theatreDto.Director,
                };

                ICollection<Ticket> ticketsToImport = new List<Ticket>();

                foreach (ImportTicketsDto ticketDto in theatreDto.Tickets)
                {

                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Ticket ticket = new Ticket()
                    {
                        Price = ticketDto.Price,
                        RowNumber = ticketDto.RowNumber,
                        PlayId = ticketDto.PlayId
                    };

                    ticketsToImport.Add(ticket);
                }

                newTheatre.Tickets = ticketsToImport;

                theatresToImport.Add(newTheatre);

                sb.AppendLine(string.Format(SuccessfulImportTheatre, newTheatre.Name, newTheatre.Tickets.Count));
            }

            context.Theatres.AddRange(theatresToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
    