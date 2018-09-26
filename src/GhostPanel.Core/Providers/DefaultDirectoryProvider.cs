using GhostPanel.Core.Config;

namespace GhostPanel.Core.Providers
{
    public class DefaultDirectoryProvider : IDefaultDirectoryProvider
    {
        private readonly GhostPanelConfig _config;
        // TODO: Refactor to be smarter about searching for values instead of a method for each
        // TODO: Make config and interface
        public DefaultDirectoryProvider(GhostPanelConfig config)
        {
            _config = config;
        }

        public string GetBaseInstallDirectory()
        {
            return _config.DefaultFilePaths.BaseInstallDirectory;
        }

        public string GetGameFileDirectory()
        {
            return _config.DefaultFilePaths.GameFileDirectory;
        }

        public string GetSteamCmdDirectory()
        {
            return _config.DefaultFilePaths.StreamCmdPath;
        }
    }
}
