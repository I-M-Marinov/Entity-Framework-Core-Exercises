using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MusicHub
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Initializer;
    using MusicHub.Data;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context = new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            // Test your solutions here

            int producerId = 9; // Replace with actual producerId

          //  Console.WriteLine(ExportAlbumsInfo(context, producerId));

            int duration = 4;

            Console.WriteLine(ExportSongsAboveDuration(context, duration));


        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var sb = new StringBuilder();

            var albums = context.Albums
                .Include(a => a.Producer)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Writer)
                .Where(a => a.ProducerId == producerId)
                .OrderByDescending(a => a.Songs.Sum(s => s.Price))
                .ToList();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.Name}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}");
                sb.AppendLine($"-ProducerName: {album.Producer.Name}");
                sb.AppendLine($"-Songs:");

                var sortedSongs = album.Songs
                    .OrderByDescending(s => s.Name)
                    .ThenBy(s => s.Writer.Name);

                int count = 1;
                foreach (var song in sortedSongs)
                {
                    sb.AppendLine($"---#{count}");
                    sb.AppendLine($"---SongName: {song.Name}");
                    sb.AppendLine($"---Price: {song.Price:F2}");
                    sb.AppendLine($"---Writer: {song.Writer.Name}");
                    count++;
                }

                sb.AppendLine($"-AlbumPrice: {album.Songs.Sum(s => s.Price):F2}");
                count = 1;
            }

            return sb.ToString().TrimEnd();

        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {

            var sb = new StringBuilder();

            var songs = context.Songs
                .Include(s => s.SongPerformers)
                .ThenInclude(sp => sp.Performer)
                .Include(s => s.Writer)
                .Include(s => s.Album)
                .ThenInclude(a => a.Producer)
                .ToList()
                .Where(a => (int)a.Duration.TotalSeconds > duration)
                .OrderBy(a => a.Name)
                .ThenBy(s => s.Writer.Name)
                .ToList();

            var counter = 1;
            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{counter}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.Writer.Name}");

                if (song.SongPerformers.Any())
                {
                    var sortedPerformers = song.SongPerformers
                        .Select(sp => sp.Performer)
                        .OrderBy(p => p.FirstName)
                        .ThenBy(p => p.LastName);

                    foreach (var performer in sortedPerformers)
                    {
                        sb.AppendLine($"---Performer: {performer.FirstName} {performer.LastName}");
                    }
                } 
                sb.AppendLine($"---AlbumProducer: {song.Album.Producer.Name}");
                sb.AppendLine($"---Duration: {song.Duration.ToString("c")}");
                counter++;
            }

            return sb.ToString().TrimEnd();
        }
    }
}
