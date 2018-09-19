using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management.GameFiles;
using GhostPanel.Core.Managment.GameFiles;
using Xunit;

namespace UnitTests.Core.ProviderTests
{
    public class GameFileManagerProviderShould
    {
        [Fact]
        public void ReturnLocalGameFileManager()
        {
            var logger = Mock.Of<ILoggerFactory>();
            var mockDirectoryProvider = new Mock<IDefaultDirectoryProvider>();
            mockDirectoryProvider
                .Setup(dp => dp.GetGameFileDirectory())
                .Returns("C:\\Server Files");

            var mockSteamCredProvider = new Mock<ISteamCredentialProvider>();


            IGameFileManager localFileManager = new LocalGameFileManager(logger, mockDirectoryProvider.Object);
            IGameFileManager steamFileManager = new SteamCmdGameFiles(mockSteamCredProvider.Object, logger, mockDirectoryProvider.Object);

            List<IGameFileManager> fileManagers = new List<IGameFileManager>();
            fileManagers.Add(localFileManager);
            fileManagers.Add(steamFileManager);

            IGameFileManagerProvider fileProvider = new GameFileManagerProvider(logger, fileManagers);

            var result = fileProvider.GetGameFileManager(GetGameServerWithoutGameId());
            Assert.IsType<LocalGameFileManager>(result);
        }

        public GameServer GetGameServerWithoutGameId()
        {
            Game game = new Game()
            {
                Name = "Test Game"
            };
            return new GameServer()
            {
                Game = game
            };
        }
    }
}
