
using GhostPanel.Communication.Query;
using GhostPanel.Communication.Query.Steam;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Rcon;
using GhostPanel.Rcon.Steam;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Rcon
{
    public class GameQueryFactoryShould
    {
        [Fact]
        public void GetQueryProtocolSteam()
        {
            var gameServer = GetGameServer();
            var mockRepo = new Mock<IRepository>();
            var result = new GameQueryFactory(new LoggerFactory(), mockRepo.Object).GetQueryProtocol(gameServer);
            Assert.IsType<SteamQueryProtocol>(result);

        }

        private GameServer GetGameServer()
        {
            var protocol = new GameProtocol()
            {
                FullTypeName = "GhostPanel.Rcon.Steam.SteamQueryProtocol"
            };

            var game = new Game()
            {
                GameProtocol = protocol
            };

            return new GameServer()
            {
                IpAddress = "192.168.1.1",
                QueryPort = 27015,
            };
        }
    }
}
