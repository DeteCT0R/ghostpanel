using GhostPanel.Core.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Management.Server
{
    public class CommandlineProcessorShould
    {

        [Fact]
        public void ReturnCommandline()
        {

        }


        private GameServer GetGameServer()
        {
            return new GameServer()
            {
                GameId = 1,
                CommandLine = "-game csgo -console -ip {ipAddress} -port {gamePort}",
                GamePort = 27015,
                IpAddress = "192.168.1.50"
            };
        }

    }
}
