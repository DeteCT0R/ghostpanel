using System.Collections.Generic;
using System.IO;
using GhostPanel.Core.GameServerUtils;
using Xunit;

namespace UnitTests.Core.GameServerUtils
{
    public class ConfigFileUtilsShould
    {
        [Fact]
        public void GetVariablesFromGameServerShould()
        {
            var gameServer = new GameServerFactory().GetGameServer();
            var expected = new Dictionary<string, string>()
            {
                {"![GameId]","0" },
                {"![IpAddress]","192.168.1.50" },
                {"![GamePort]","29365" },
                {"![QueryPort]","0" },
                {"![ServerName]", "Test Server" },
                {"![IsEnabled]","True" },
                {"![HomeDirectory]",@"C:\dev\Server1" },
                {"![CommandLine]","-game csgo -console -usercon" },
                {"![Slots]","0" },
                {"![RconPassword]", "password" },
                {"![TestVar1]", "thing1" },
                {"![TestVar2]", "thing2" },
                {"![TestVar3]", "thing3" }
            };

            var result = ConfigFileUtils.GetVariablesFromGameServer(gameServer);

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
