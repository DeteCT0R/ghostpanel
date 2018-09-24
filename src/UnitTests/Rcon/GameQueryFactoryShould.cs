﻿
using GhostPanel.Core.Data.Model;
using GhostPanel.Rcon;
using GhostPanel.Rcon.Steam;
using Xunit;

namespace UnitTests.Rcon
{
    public class GameQueryFactoryShould
    {
        [Fact]
        public void GetQueryProtocolSteam()
        {
            var gameServer = GetGameServer();
            var result = new GameQueryFactory().GetQueryProtocol(gameServer);
            Assert.IsType<SteamQueryProtocol>(result);

        }

        private GameServer GetGameServer()
        {
            var protocol = new GameProtocol()
            {
                FullTypeName = "GhostPanel.Rcon.Steam.SteamQueryProtocol"
            };

            return new GameServer()
            {
                IpAddress = "192.168.1.1",
                QueryPort = 27015,
                Protocol = protocol
            };
        }
    }
}
