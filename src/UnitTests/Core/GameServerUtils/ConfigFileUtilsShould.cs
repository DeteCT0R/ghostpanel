using System.Collections.Generic;
using System.IO;
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

            using (StreamReader reader = new StreamReader(fileStream))
            {
                expected = reader.ReadToEnd();
            }

            var result = ConfigFileUtils.InterpolateConfigFromGameServer(gameServer);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void InterpolateConfigFromDict()
        {
            string expected;
            FileStream fileStream = new FileStream("expectedconfig.cfg", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                expected = reader.ReadToEnd();
            }

            fileStream = new FileStream("csgoconfig.cfg", FileMode.Open);
            string template;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                template = reader.ReadToEnd();
            }

            var values = new Dictionary<string, string>()
            {
                {"![ServerName]", "Test Server" },
                {"![RconPassword]", "password" },
                {"![TestVar1]", "thing1" },
                {"![TestVar2]", "thing2" },
                {"![TestVar3]", "thing3" }
            };
            var result = ConfigFileUtils.InterpolateConfigFromDict(values, template);
            Assert.Equal(expected, result);
        }
    }
}
