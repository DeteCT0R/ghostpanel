using System.Net;
using System.Threading.Tasks;
using GhostPanel.BackgroundServices;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using GhostPanel.Rcon;
using GhostPanel.Rcon.Steam;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Core.Management
{
    public class ServerStatServiceShould
    {
        private ILoggerFactory _logger;
        private Mock<IServerProcessManagerProvider> _mockProcManagerProvider;
        private Mock<IRepository> _mockRepo;
        private Mock<IGameQueryFactory> _mockGameQueryFact;
        private Mock<IQueryProtocol> _mockGameQuery;

        public ServerStatServiceShould()
        {
            _logger = new LoggerFactory();
            _mockProcManagerProvider = new Mock<IServerProcessManagerProvider>();
            var mockProcManager = new Mock<IServerProcessManager>();
            mockProcManager
                .Setup(pm => pm.IsRunning(1))
                .Returns(false);
            _mockProcManagerProvider
                .Setup(pmp => pmp.GetProcessManagerProvider())
                .Returns(mockProcManager.Object);

            _mockRepo = new Mock<IRepository>();
            _mockRepo
                .Setup(r => r.Update(new GameServer()))
                .Returns((GameServer) null);

            _mockGameQueryFact = new Mock<IGameQueryFactory>();
            _mockGameQuery = new Mock<IQueryProtocol>();

            _mockGameQuery
                .Setup(gq => gq.GetServerInfoAsync())
                .ReturnsAsync(GetSteamServerInfo());
            
        }

        [Fact]
        public async void ValidateUpdateQueryStats()
        {
            var gameServer = GetGameServer();

            _mockGameQueryFact
                .Setup(gqf => gqf.GetQueryProtocol(gameServer))
                .Returns(_mockGameQuery.Object);

            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object, _mockGameQueryFact.Object);

            var result = await statService.UpdateServerQueryStatsAsync(gameServer);
            Assert.Equal("Test server", result.GameServerCurrentStats.Name);
            Assert.Equal("de_dust", result.GameServerCurrentStats.Map);
            Assert.Equal(32, result.GameServerCurrentStats.MaxPlayers);
            Assert.Equal(10, result.GameServerCurrentStats.CurrentPlayers);
        }

        [Fact]
        public void CheckServerProcSetStopped()
        {
            var gameServer = GetGameServer();
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object, _mockGameQueryFact.Object);
            var result = statService.CheckServerProc(gameServer);
            Assert.Equal(ServerStatusStates.Stopped, result.GameServerCurrentStats.Status);

        }

        [Fact]
        public void CheckServerProcSetRunning()
        {
            var gameServer = GetGameServer();
            gameServer.GameServerCurrentStats.Pid = 111;
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object, _mockGameQueryFact.Object);
            var result = statService.CheckServerProc(gameServer);
            Assert.Equal(ServerStatusStates.Running, result.GameServerCurrentStats.Status);

        }

        private GameServer GetGameServer()
        {
            var gameServer = new GameServer();
            gameServer.GameServerCurrentStats.Status = ServerStatusStates.Running;
            gameServer.GameServerCurrentStats = new GameServerCurrentStats();
            gameServer.Game.GameProtocol = new GameProtocol()
            {
                FullTypeName = "GhostPanel.Rcon.Steam.SteamQueryProtocol"
            };
            return gameServer;
        }

        private ServerInfoBase GetSteamServerInfo()
        {
            return new SteamServerInfo()
            {
                Name = "Test server",
                Bots = 2,
                Game = "CSGO",
                Map = "de_dust",
                MaxPlayers = 32,
                CurrentPlayers = 10
            };
        }
    }
}
