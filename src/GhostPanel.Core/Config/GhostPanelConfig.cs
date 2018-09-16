namespace GhostPanel.Core.Config
{
    public class GhostPanelConfig
    {
        public string DatabaseConnectionString { get; set; }
        public string BaseDirectory { get; set; }
        public SteamConfig SteamSettings { get; set; }
        public DefaultFilePaths FilePaths { get; set; }
    }
}
