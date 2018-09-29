using GhostPanel.Core.Data;
using Moq;
using System.Collections.Generic;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;
using Xunit;
using GhostPanel.Core.Exceptions;

namespace UnitTests.Core.ProviderTests
{
    public class PortAndIpProviderShould
    {

        private readonly ILogger<PortAndIpProvider> _logger;

        public PortAndIpProviderShould()
        {
            _logger = new LoggerFactory().CreateLogger<PortAndIpProvider>();
        }

        [Fact]
        public void ReturnNextHighestPort()
        {
            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo
                .Setup(x => x.List(It.IsAny<GameServerPolicy>()))
                .Returns(GetGameServer());

            IPortAndIpProvider portProvider = new PortAndIpProvider(mockRepo.Object, _logger);

            var port = portProvider.GetNextAvailablePort(27015, "192.168.1.1", 10);
            Assert.Equal(27015, port);
        }

        
        public List<GameServer> GetGameServer()
        {
            return new List<GameServer>();
        }

        public Game GetGame()
        {
            return new Game()
            {
                Id = 1,
                Name = "Test Game",
                PortIncrement = 10,
                GamePort = 27015
            };
        }
    }
}
