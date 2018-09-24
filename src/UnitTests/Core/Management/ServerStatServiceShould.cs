﻿using System.Net;
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
            var gameServer = GetGameServer();
            _mockGameQueryFact
                .Setup(gqf => gqf.GetQueryProtocol(gameServer))
                .Returns(new SteamQueryProtocol(IPAddress.Parse("111.111.111.111"), 27015));
        }

        [Fact]
        public void ValidateUpdateQueryStats()
        {
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object, _mockGameQueryFact.Object);
            var gameServer = GetGameServer();
            var query = _mockGameQueryFact.Object.GetQueryProtocol(gameServer);
            var result = statService.UpdateServerQueryStats(gameServer);
            Assert.Equal("Test server", gameServer.CurrentStats.Name);
            Assert.Equal("de_dust", gameServer.CurrentStats.Map);
            Assert.Equal(32, gameServer.CurrentStats.MaxPlayers);
            Assert.Equal(10, gameServer.CurrentStats.CurrentPlayer);
        }

        [Fact]
        public void CheckServerProcSetStopped()
        {
            var gameServer = GetGameServer();
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object, _mockGameQueryFact.Object);
            var result = statService.CheckServerProc(gameServer);
            Assert.Equal(ServerStatusStates.Stopped, result.Status);

        }

        [Fact]
        public void CheckServerProcSetRunning()
        {
            var gameServer = GetGameServer();
            gameServer.CurrentStats.Pid = 111;
            var statService = new ServerStatService(_logger, _mockProcManagerProvider.Object, _mockRepo.Object, _mockGameQueryFact.Object);
            var result = statService.CheckServerProc(gameServer);
            Assert.Equal(ServerStatusStates.Running, result.Status);

        }

        private GameServer GetGameServer()
        {
            var gameServer = new GameServer();
            gameServer.Status = ServerStatusStates.Running;
            gameServer.CurrentStats = new GameServerCurrentStat();
            gameServer.Protocol = new GameProtocol()
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
