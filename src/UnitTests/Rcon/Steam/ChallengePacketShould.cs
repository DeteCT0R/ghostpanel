using GhostPanel.Rcon.Steam.Packets;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Rcon.Steam
{
    public class ChallengePacketShould
    {
        [Fact]
        public void GetChallangePacket()
        {
            var expected = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF };
            Assert.Equal(expected, ChallangePacket.GetChallangePacket());
        }

        [Fact]
        public void CheckCleanedChallangePacketResponse()
        {
            var beforeCleaning = new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0x41, 0xC1, 0x2F, 0xB5, 0x07};
            var expected = new byte[] {0xC1, 0x2F, 0xB5, 0x07};
            Assert.Equal(expected, ChallangePacket.CleanChallangeResponse(beforeCleaning));
        }
    }
}
