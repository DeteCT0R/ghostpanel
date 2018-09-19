using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Config;

namespace UnitTests
{
    public static class FakeConfig
    {
        public static GhostPanelConfig GetFakeConfig()
        {
            return new GhostPanelConfig()
            {
                DatabaseConnectionString =
                    "Server=(localdb)\\mssqllocaldb;Database=GhostPanel;Trusted_Connection=True;MultipleActiveResultSets=true",
                SteamSettings = new SteamConfig()
                {
                    Username = "anonymous",
                    Password = ""
                },
                DefaultFilePaths = new DefaultFilePathSettings()
                {
                    BaseInstallDirectory = "C:\\Game Servers",
                    GameFileDirectory = "C:\\Server Files",
                    SteamCmdDirectory = "C:\\SteamCmd"
                }
            };
        }
    }
}
