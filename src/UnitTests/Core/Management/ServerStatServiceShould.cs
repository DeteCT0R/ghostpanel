using GhostPanel.BackgroundServices;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
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
        }

        [Fact]
        public void CheckServerProcSetStopped()
        {
            var gameServer = GetGameServer();
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object);
            var result = statService.CheckServerProc(gameServer);
            Assert.Equal(ServerStatusStates.Stopped, result.Status);

        }

        [Fact]
        public void CheckServerProcSetRunning()
        {
            var gameServer = GetGameServer();
            gameServer.CurrentStats.Pid = 111;
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object);
            var result = statService.CheckServerProc(gameServer);
            Assert.Equal(ServerStatusStates.Running, result.Status);

        }

        private GameServer GetGameServer()
        {
            var gameServer = new GameServer();
            gameServer.Status = ServerStatusStates.Running;
            gameServer.CurrentStats = new GameServerCurrentStat();
            return gameServer;
        }
    }
}
