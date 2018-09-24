using GhostPanel.Core.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Management.Server;
using Microsoft.Extensions.Logging;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
using Xunit;
using GhostPanel.Core.Management;
using System.Diagnostics;

namespace UnitTests.Core.Management
{
    public class ServerProcManagerWinShould
    {
        private ILoggerFactory _logger;
        private Mock<IRepository> _mockRepo;
        private Mock<ICommandlineProcessor> _clProces;

        public ServerProcManagerWinShould()
        {
            _logger = new LoggerFactory();
            _mockRepo = new Mock<IRepository>();
            _clProces = new Mock<ICommandlineProcessor>();
            _mockRepo
                .Setup(r => r.Update(new GameServer()))
                .Returns((GameServer)null);
        }

        [Fact]
        public void MarkServerAsCrashed()
        {
            var gameServer = GetGameServer();
            var procManager = new ServerProcessManagerWin(_logger, _mockRepo.Object, _clProces.Object);
            gameServer.CurrentStats.Pid = Process.GetCurrentProcess().Id; ; // TODO: This is hacky and just prevnts the it attempting to start the server
            var result = procManager.HandleCrashedServer(gameServer);
            Assert.Equal(ServerStatusStates.Crashed, result.Status);
        }
        
        [Fact]
        public void MarkServerAsStoppedAfterThreeAttempts()
        {
            var gameServer = GetGameServer();
            var procManager = new ServerProcessManagerWin(_logger, _mockRepo.Object, _clProces.Object);
            gameServer.CurrentStats.RestartAttempts = 3;
            var result = procManager.HandleCrashedServer(gameServer);
            Assert.Equal(ServerStatusStates.Stopped, result.Status);
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
