using System.Globalization;
using Footballers.DataProcessor.ExportDto;

namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Data.Models.Enums;
    using Footballers.Utilities;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Coaches";



        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teamsToExport = context.Teams
                .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
                .Select(t => new
                {
                    t.Name,
                    Footballers = t.TeamsFootballers
                        .Where(tf => tf.Footballer.ContractStartDate >= date)
                        .Select(tf => new
                        {
                            FootballerName = tf.Footballer.Name,
                            tf.Footballer.ContractStartDate,
                            tf.Footballer.ContractEndDate,
                            tf.Footballer.BestSkillType,
                            tf.Footballer.PositionType
                        })
                        .OrderByDescending(f => f.ContractEndDate)
                        .ThenBy(f => f.FootballerName)
                        .ToArray()
                })
                .AsEnumerable()
                .Select(t => new ExportTeamsDto()
                {
                    Name = t.Name,
                    Footballers = t.Footballers
                        .Where(t => t.ContractStartDate >= date)
                        .Select(f => new ExportFootballerDto()
                        {
                            FootballerName = f.FootballerName,
                            ContractStartDate = f.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                            ContractEndDate = f.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                            BestSkillType = f.BestSkillType.ToString(),
                            PositionType = f.PositionType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Count) 
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();


            return JsonConvert.SerializeObject(teamsToExport, Formatting.Indented);
        }
    }
}
