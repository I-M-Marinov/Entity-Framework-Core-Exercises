using System.Globalization;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using Footballers.DataProcessor.ImportDto;

namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Utilities;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Security.AccessControl;
    using System.Text;
    using static Footballers.DataProcessor.ImportDto.ImportCoachesDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Coaches";

            ICollection<Coach> coachesToImport = new List<Coach>();

            ImportCoachesDto[] deserializedCoaches =
                xmlHelper.Deserialize<ImportCoachesDto[]>(xmlString, xmlRoot);

            foreach (ImportCoachesDto coachDto in deserializedCoaches)
            {
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach newCoach = new Coach()
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality
                };

                foreach (ImportFootballerDto footballerDto in coachDto.Footballers)
                {

                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    BestSkillType bestSkillType;

                    if (footballerDto.BestSkillType == 0)
                    {
                        bestSkillType = BestSkillType.Defence;
                    }
                    else if (footballerDto.BestSkillType == 1)
                    {
                        bestSkillType = BestSkillType.Dribble;
                    }
                    else if (footballerDto.BestSkillType == 2)
                    {
                        bestSkillType = BestSkillType.Pass;
                    }
                    else if (footballerDto.BestSkillType == 3)
                    {
                        bestSkillType = BestSkillType.Shoot;
                    }
                    else if (footballerDto.BestSkillType == 4)
                    {
                        bestSkillType = BestSkillType.Speed;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    PositionType positionType;

                    if (footballerDto.PositionType == 0)
                    {
                        positionType = PositionType.Goalkeeper;
                    }
                    else if (footballerDto.PositionType == 1)
                    {
                        positionType = PositionType.Defender;
                    }
                    else if (footballerDto.PositionType == 2)
                    {
                        positionType = PositionType.Midfielder;
                    }
                    else if (footballerDto.PositionType == 3)
                    {
                        positionType = PositionType.Forward;
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isContractStartDateValid = DateTime.TryParseExact(footballerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime contractStartDate);

                    bool isContractEndDateValid = DateTime.TryParseExact(footballerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out DateTime contractEndDate);


                    if (!isContractStartDateValid || !isContractEndDateValid || contractStartDate > contractEndDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer newFootballer = new Footballer()
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = contractStartDate,
                        ContractEndDate = contractEndDate,
                        BestSkillType = bestSkillType,
                        PositionType = positionType,
                    };

                    newCoach.Footballers.Add(newFootballer);

                }

                coachesToImport.Add(newCoach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, newCoach.Name, newCoach.Footballers.Count));

            }
            context.Coaches.AddRange(coachesToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<Team> teamsToImport = new List<Team>();

            var validFootballersIds = context.Footballers.Select(f => f.Id).ToList();


            ImportTeamDto[] deserializedTeams = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString)!;


            foreach (ImportTeamDto teamDto in deserializedTeams)
            {
                if (!IsValid(teamDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (teamDto.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team team = new Team()
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies
                };

                ICollection<TeamFootballer> teamFootballersToImport = new List<TeamFootballer>();

                foreach (var footballerId in teamDto.Footballers.Distinct())
                {

                    if (!validFootballersIds.Contains(footballerId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    TeamFootballer newTeamFootballer = new TeamFootballer()
                    {
                        Team = team,
                        FootballerId = footballerId
                    };

                    teamFootballersToImport.Add(newTeamFootballer);

                }

                team.TeamsFootballers = teamFootballersToImport;

                teamsToImport.Add(team);

                sb.AppendLine(string.Format(SuccessfullyImportedTeam, team.Name, team.TeamsFootballers.Count));
            }

            context.Teams.AddRange(teamsToImport);
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
