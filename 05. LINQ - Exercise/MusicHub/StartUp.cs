using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;

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

            Console.WriteLine(ExportAlbumsInfo(context, producerId));
            
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
            throw new NotImplementedException();
        }
    }
}
