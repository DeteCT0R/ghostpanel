using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GhostPanel.Core.GameServerUtils;
using Xunit;

namespace UnitTests.Core.GameServerUtils
{
    public class ConfigFileUtilsShould
    {
        [Fact]
        public void InterpolateConfigReplaceVars()
        {
            var gameServer = new GameServerFactory().GetGameServer();
            string expected;
            FileStream fileStream = new FileStream("expectedconfig.cfg", FileMode.Open);
            string content;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                expected = reader.ReadToEnd();
            }

            var result = ConfigFileUtils.InterpolateConfigFromGameServer(gameServer);
            Assert.Equal(expected, result);
        }
    }
}
