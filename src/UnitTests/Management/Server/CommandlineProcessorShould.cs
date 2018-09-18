using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using GhostPanel.Core.Management.Server;

namespace UnitTests.Management.Server
{
    public class CommandlineProcessorShould
    {

        [Fact]
        public void PerformBaseCommandlineInterpolation()
        {
            var gameServer = GetGameServerWithoutCustomCommandline();
            var logger = Mock.Of <ILogger<CommandlineProcessor>>();
            CommandlineProcessor clprocess = new CommandlineProcessor(logger);

            var expectedResult = "-game csgo -console -ip 192.168.1.50 -port 27015";
            Assert.Equal(expectedResult, clprocess.InterpolateCommandline(gameServer));
        }

        [Fact]
        public void PerformCustomCommandlineInterpolation()
        {
            var gameServer = GetGameServerWithCustomCommandline();
            var logger = Mock.Of<ILogger<CommandlineProcessor>>();
            CommandlineProcessor clprocess = new CommandlineProcessor(logger);
            var expectedResult = "-secure -threads 3";
            var actualResult =
                clprocess.InterpolateCustomCommandline(gameServer.CustomCommandLineArgs);

            Assert.Equal(expectedResult, actualResult);

        }


        private GameServer GetGameServerWithoutCustomCommandline()
        {
            return new GameServer()
            {
                GameId = 1,
                CommandLine = "-game csgo -console -ip {IpAddress} -port {GamePort}",
                GamePort = 27015,
                IpAddress = "192.168.1.50"
            };
        }

        private GameServer GetGameServerWithCustomCommandline()
        {
            return new GameServer()
            {
                GameId = 1,
                CommandLine = "-game csgo -console -ip {IpAddress} -port {GamePort}",
                GamePort = 27015,
                IpAddress = "192.168.1.50",
                CustomCommandLineArgs = new Dictionary<string, string>()
                {
                    {"-secure", ""},
                    {"-threads", "3"}

                }
            };
        }

    }
}
