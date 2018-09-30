using System;
using System.Collections.Generic;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GhostPanel.Web
{
    public static class SetUpDatabase
    {
        public static IRepository SetUpRepository(string connectionString)
        {
            DbContextOptions<AppDataContext> options = new DbContextOptionsBuilder<AppDataContext>()
                .UseSqlServer(connectionString)
                .Options;

            var appDataContext = new AppDataContext(options);

            EnsureDatabase(appDataContext);
            IRepository repository = new EfGenericRepo(appDataContext);
            EnsureInitialData(repository);

            return repository;
        }

        private static void EnsureDatabase(AppDataContext dataContext)
        {
            dataContext.Database.Migrate();
        }

        private static void EnsureInitialData(IRepository repository)
        {
            var test = repository.List<Game>();
            if (!repository.List<Game>().Any())
            {
                repository.Create(GetInitialGame());
            }


        }

        private static List<Game> GetInitialGame()
        {
            return new List<Game>
            {
                new Game
                {
                    Name = "Counter Strike Global Offensive",
                    //SteamAppId = 740,
                    ArchiveName = "csgo.zip",
                    ExeName = "srcds.exe",
                    MaxSlots = 32,
                    MinSlots = 8,
                    DefaultSlots = 8,
                    PortIncrement = 10,
                    GamePort = 27015,
                    QueryPort = 27015,
                    GameProtocol = new GameProtocol()
                    {
                        FullTypeName = "GhostPanel.Rcon.Steam.SteamQueryProtocol",
                        Name = "Steam"
                    }
                    
                }
            };
        }

        private static List<GameServer> GetInitialGameServer()
        {
            return new List<GameServer>
            {
                new GameServer
                {
                    GameId = 1,
                    IpAddress = "192.168.1.50",
                    GamePort = 29365,
                    ServerName = "Test Server",
                    IsEnabled = true,
                    HomeDirectory = @"C:\dev\Server1",
                    CommandLine = "-game csgo -console -usercon"
                }
            };
        }

    }
}
