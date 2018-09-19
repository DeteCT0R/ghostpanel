using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Config;
using GhostPanel.Core.Providers;
using Xunit;

namespace UnitTests.Core.ProviderTests
{
    public class SteamCredentialProviderShould 
    {


        [Fact]
        public void GetCorrectCredentailStringForAnonymous()
        {
            GhostPanelConfig config = FakeConfig.GetFakeConfig();
            var credProvider = new SteamCredentialProvider(config);
            Assert.Equal("anonymous", credProvider.GetCredentialString());
        }

        [Fact]
        public void GetCorrectCredentailStringForUserAndPass()
        {
            GhostPanelConfig config = FakeConfig.GetFakeConfig();
            config.SteamSettings.Password = "Password123";
            config.SteamSettings.Username = "TestUser";
            var credProvider = new SteamCredentialProvider(config);
            Assert.Equal("TestUser Password123", credProvider.GetCredentialString());
        }

        [Fact]
        public void GetUserAndPassword()
        {
            GhostPanelConfig config = FakeConfig.GetFakeConfig();
            config.SteamSettings.Password = "Password123";
            config.SteamSettings.Username = "TestUser";
            var credProvider = new SteamCredentialProvider(config);
            Assert.Equal("Password123", credProvider.GetPassword());
            Assert.Equal("TestUser", credProvider.GetUsername());
        }

    }
}
