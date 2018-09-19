using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Config;
using GhostPanel.Core.Providers;
using Xunit;

namespace UnitTests.Core.ProviderTests
{
    public class DefaultDirectoryProviderShould
    {
        [Fact]
        public void GetBaseInstallDirectory()
        {
            GhostPanelConfig config = FakeConfig.GetFakeConfig();
            var provider = new DefaultDirectoryProvider(config);
            Assert.Equal("C:\\Game Servers", provider.GetBaseInstallDirectory());
        }

        [Fact]
        public void GetGameFileDirectory()
        {
            GhostPanelConfig config = FakeConfig.GetFakeConfig();
            var provider = new DefaultDirectoryProvider(config);
            Assert.Equal("C:\\Server Files", provider.GetGameFileDirectory());
        }

        [Fact]
        public void GetSteamCmdDirectory()
        {
            GhostPanelConfig config = FakeConfig.GetFakeConfig();
            var provider = new DefaultDirectoryProvider(config);
            Assert.Equal("C:\\SteamCmd", provider.GetSteamCmdDirectory());
        }
    }
}
