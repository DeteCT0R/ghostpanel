using System;
using System.Collections.Generic;
using System.IO;
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
            if (!repository.List<User>().Any())
            {
                repository.Create(GetInitialUser());
            }
            if (!repository.List<Game>().Any())
            {
                repository.Create(GetInitialGame());
            }
            if (!repository.List<GameServer>().Any())
            {
                repository.Create(GetInitialGameServer());
            }
            
            if (!repository.List<ScheduledTask>().Any())
            {
                repository.Create(GetInitialScheduledTask());
            }
            

            if (!repository.List<GameDefaultConfigFile>().Any())
            {
                repository.Create(GetconfigFile());
            }


        }

        private static ScheduledTask GetInitialScheduledTask()
        {
            return new ScheduledTask()
            {
                TaskName = "Test message",
                Minute = "*/3",
                TaskType = ScheduledTaskType.Message,
                GameServerId = 1
            };
        }

        private static User GetInitialUser()
        {
            return new User()
            {
                Username = "BarryCarey",
                FirstName = "Barry",
                LastName = "Carey"
            };
        }

        private static List<Game> GetInitialGame()
        {
            return new List<Game>
            {
                new Game
                {
                    Name = "Counter Strike Global Offensive",
                    SteamAppId = 740,
                    ArchiveName = "csgo.zip",
                    ExeName = "srcds.exe",
                    MaxSlots = 32,
                    MinSlots = 8,
                    DefaultSlots = 8,
                    PortIncrement = 10,
                    GamePort = 27015,
                    QueryPort = 27015,
                    DefaultCommandline = "-game csgo -console -usercon -rcon_password ![RconPassword] +game_type 0 +game_mode 0 -maxplayers_override ![Slots] +maxplayers ![Slots] +exec server.cfg +mapgroup mg_bomb +map de_dust -ip ![IpAddress] -port ![GamePort]",
                    GameProtocol = new GameProtocol()
                    {
                        FullTypeName = "GhostPanel.Rcon.Steam.SteamQueryProtocol",
                        Name = "Steam"
                    },
                    
                    
                }
            };
        }

        private static GameServer GetInitialGameServer()
        {

            return new GameServer
            {
                GameId = 1,
                IpAddress = "192.168.1.10",
                GamePort = 27015,
                ServerName = "Test Server",
                IsEnabled = true,
                HomeDirectory = @"C:\dev\gservers\server1",
                CommandLine = "-game csgo -console -usercon",
                OwnerId = 1
            };

        }

        private static GameDefaultConfigFile GetconfigFile()
        {
            FileStream fileStream = new FileStream("csgoconfig.cfg", FileMode.Open);
            string content;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                content = reader.ReadToEnd();
            }

            return new GameDefaultConfigFile()
            {
                Template = content,
                Description = "Main config",
                FilePath = @"csgo\cfg\server.cfg",
                GameId = 1
                
            };
        }

    }
}
