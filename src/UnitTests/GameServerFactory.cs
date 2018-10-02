using System.Collections.Generic;
using GhostPanel.Core.Data.Model;
using System.IO;

namespace UnitTests
{
    class GameServerFactory
    {
        public GameServer GetGameServer()
        {
            return new GameServer
            {
                IpAddress = "192.168.1.50",
                GamePort = 29365,
                ServerName = "Test Server",
                IsEnabled = true,
                HomeDirectory = @"C:\dev\Server1",
                CommandLine = "-game csgo -console -usercon",
                Game = GetGame(),
                CustomVariables = GetVariables()
            };
        }

        public List<CustomVariable> GetVariables()
        {
            return new List<CustomVariable>()
            {
                new CustomVariable()
                {
                    VariableName = "TestVar1",
                    VariableValue = "thing1"
                },
                new CustomVariable()
                {
                    VariableName = "TestVar2",
                    VariableValue = "thing2"
                },
                new CustomVariable()
                {
                    VariableName = "TestVar3",
                    VariableValue = "thing3"
                }
            };
        }

        public Game GetGame()
        {
            return new Game
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
                },
                GameDefaultConfigFile = GetconfigFile()

            };
        }

        private GameDefaultConfigFile GetconfigFile()
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

            };
        }
    }
}
