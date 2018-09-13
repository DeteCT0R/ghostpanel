namespace GhostPanel.Core.Providers
{
    public class SteamCredentialProvider : ISteamCredentialProvider
    {
        private readonly GhostPanelConfig _config;

        public SteamCredentialProvider(GhostPanelConfig config)
        {
            _config = config;
        }

        public string GetPassword()
        {
            return _config.SteamPassword;
        }

        public string GetUsername()
        {
            return _config.SteamUserName;
        }
    }
}
