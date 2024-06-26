using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data
{


    public  class FootballBettingContext : DbContext
    {

        private const string ConnectionString = "Server=MARINOV-GAME-PC\\SQLEXPRESS; Database = FootballBookmakerSystem; Integrated Security = true; Encrypt = False; TrustServerCertificate = true;";

        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions<FootballBettingContext> options)
            : base(options)
        {


        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Town> Towns { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Color> Colors { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<Bet> Bets { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<PlayerStatistic> PlayersStatistics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured == false)
            {

                optionsBuilder.UseSqlServer(ConnectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PlayerStatistic>()
                .HasKey(ps => new { ps.PlayerId, ps.GameId });

            base.OnModelCreating(modelBuilder);
        }


    }
}
