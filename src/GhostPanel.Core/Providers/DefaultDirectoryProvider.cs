using GhostPanel.Core.Config;

namespace GhostPanel.Core.Providers
{
    public class DefaultDirectoryProvider : IDefaultDirectoryProvider
    {
        private readonly GhostPanelConfig _config;

        public DefaultDirectoryProvider(GhostPanelConfig config)
        {
            _config = config;
        }

        public string GetBaseInstallDirectory()
        {
            return _config.FilePaths.BaseInstallDirectory;
        }

        public string GetGameFileDirectory()
        {
            return _config.FilePaths.GameFileDirectory;
        }
    }
}
