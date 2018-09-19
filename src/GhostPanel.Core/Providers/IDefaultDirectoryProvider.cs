using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Providers
{
    public interface IDefaultDirectoryProvider
    {
        string GetBaseInstallDirectory();
        string GetGameFileDirectory();
        string GetSteamCmdDirectory();
    }
}
