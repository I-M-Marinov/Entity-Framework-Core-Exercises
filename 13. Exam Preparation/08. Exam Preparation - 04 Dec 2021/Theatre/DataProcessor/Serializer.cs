using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Theatre.Data.Models.Enums;
using Theatre.DataProcessor.ExportDto;

namespace Theatre.DataProcessor
{

    using System;
    using Theatre.Data;
    using Theatre.Utilities;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatresToExport = context.Theatres
                .Where(t => t.NumberOfHalls >= numbersOfHalls)
                .Where(t => t.Tickets.Count >= 20)
                .Select(t => new ExportTheatresDto
                {
                    Name = t.Name,
                    Halls = t.NumberOfHalls,
                    TotalIncome = t.Tickets
                        .Where(ticket => ticket.RowNumber >= 1 && ticket.RowNumber <= 5)
                        .Sum(ticket => ticket.Price),
                    Tickets = t.Tickets
                        .Where(ticket => ticket.RowNumber >= 1 && ticket.RowNumber <= 5)
                        .OrderByDescending(ticket => ticket.Price)
                        .Select(ticket => new ExportTicketsDto
                        {
                            Price = ticket.Price,
                            RowNumber = ticket.RowNumber
                        })
                        .ToArray()
                })
                .OrderByDescending(t => t.Halls)
                .ThenBy(t => t.Name)
                .ToArray();


            return JsonConvert.SerializeObject(theatresToExport, Formatting.Indented);

        }

        public static string ExportPlays(TheatreContext context, double raiting)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRoot = "Plays";

            var playsToExport = context.Plays
                .Where(p => p.Rating <= raiting)
                .Select(p => new
                {
                    p.Title,
                    p.Duration,
                    p.Rating,
                    Genre = p.Genre, 
                    Actors = p.Casts
                        .Where(c => c.IsMainCharacter == true) 
                        .Select(c => new
                        {
                            c.FullName,
                            PlayTitle = p.Title 
                        })
                        .OrderByDescending(a => a.FullName) 
                        .ToArray()
                })
                .AsEnumerable() 
                .Select(p => new ExportPlaysDto()
                {
                    Title = p.Title,
                    Duration = p.Duration.ToString("c"), 
                    Rating = p.Rating == 0 ? "Premier" : p.Rating.ToString(), 
                    Genre = p.Genre.ToString(), 
                    Actors = p.Actors
                        .Select(a => new ExportActorDto()
                        {
                            FullName = a.FullName,
                            MainCharacter = $"Plays main character in '{a.PlayTitle}'."
                        })
                        .ToArray()
                })
                .OrderBy(p => p.Title) 
                .ThenByDescending(p => p.Genre) 
                .ToArray();


            return xmlHelper.Serialize(playsToExport, xmlRoot);

        }
    }
}
