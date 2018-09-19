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
using System.Data;
using GhostPanel.Core.Exceptions;

namespace UnitTests.Core.ProviderTests
{
    public class GameFileManagerProviderShould
    {
        private LoggerFactory logger;
        private IDefaultDirectoryProvider mockDirectoryProvider;
        private ISteamCredentialProvider mockSteamCredProvider;
        private IList<IGameFileManager> fileManagers;
        private IGameFileManagerProvider fileProvider;

        public GameFileManagerProviderShould()
        {
            var logger = new LoggerFactory();
            var mockDirectoryProvider = new Mock<IDefaultDirectoryProvider>();
            mockDirectoryProvider
                .Setup(dp => dp.GetGameFileDirectory())
                .Returns("C:\\Server Files");

            var mockSteamCredProvider = new Mock<ISteamCredentialProvider>();
            IGameFileManager localFileManager = new LocalGameFileManager(logger, mockDirectoryProvider.Object);
            IGameFileManager steamFileManager = new SteamCmdGameFiles(mockSteamCredProvider.Object, logger, mockDirectoryProvider.Object);
            fileManagers = new List<IGameFileManager>();
            fileManagers.Add(localFileManager);
            fileManagers.Add(steamFileManager);
            fileProvider = new GameFileManagerProvider(logger, fileManagers);
        }

        [Fact]
        public void ReturnLocalGameFileManager()
        {
            var result = fileProvider.GetGameFileManager(GetGameServerWithoutAppId());
            Assert.IsType<LocalGameFileManager>(result);
        }

        [Fact]
        public void ReturnLocalSteamFileManager()
        {
            var result = fileProvider.GetGameFileManager(GetGameServerWithAppId());
            Assert.IsType<SteamCmdGameFiles>(result);
        }

        [Fact]
        public void ThrowExceptionForGameWithoutAppIdOrArchive()
        {
            Assert.Throws<NoNullAllowedException>(() =>
                fileProvider.GetGameFileManager(GetGameServerWithoutAppIdOrArchive()));
        }

        [Fact]
        public void ThrowFailedToLocateGameFileManager()
        {
            fileManagers.Clear();
            Assert.Throws<FailedToLocateGameFileManager>(() =>
                fileProvider.GetGameFileManager(GetGameServerWithoutAppId()));
        }
        public GameServer GetGameServerWithoutAppId()
        {
            Game game = new Game()
            {
                Name = "Test Game",
                ArchiveName = "test.zip"
            };
            return new GameServer()
            {
                Game = game
            };
        }

        public GameServer GetGameServerWithAppId()
        {
            Game game = new Game()
            {
                Name = "Test Game",
                ArchiveName = "test.zip",
                SteamAppId = 720
            };
            return new GameServer()
            {
                Game = game
            };
        }

        public GameServer GetGameServerWithoutAppIdOrArchive()
        {
            Game game = new Game()
            {
                Name = "Test Game",
            };
            return new GameServer()
            {
                Game = game
            };
        }


    }
}
