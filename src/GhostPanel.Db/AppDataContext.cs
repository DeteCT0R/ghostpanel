using System.Collections.Generic;
using GhostPanel.Core.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace GhostPanel.Db
{
    public class AppDataContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameServer> GameServers { get; set; }

        public AppDataContext()
        {
        }

        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["DatabaseConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameServer>()
                .Property(gs => gs.CustomCommandLineArgs)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));


            modelBuilder.Entity<GameServer>()
                .HasOne(s => s.GameServerCurrentStats)
                .WithOne(c => c.GameServer)
                .HasForeignKey<GameServerCurrentStats>(gs => gs.Id)
                .IsRequired();


        }

        public class AppDataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
        {
            public AppDataContext CreateDbContext(string[] args)
            {
                string connectionString =
                    new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["DatabaseConnectionString"];

                var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
                optionsBuilder.UseSqlServer(connectionString);

                return new AppDataContext(optionsBuilder.Options);
            }
        }
    }
}
