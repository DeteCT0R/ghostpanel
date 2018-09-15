using GhostPanel.Core.Providers;
using System.IO;

namespace GhostPanel.Core
{
    public class SteamCredentialWrapper
    {
        private string _username;
        private string _password;
        private string _steamCmdPath;
        private string steamCmdUrl = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";

        public SteamCredentialWrapper(ISteamCredentialProvider steamCredentails)
        {
            _username = steamCredentails.GetUsername();
            _password = steamCredentails.GetPassword();
            _steamCmdPath = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD", "steamcmd.exe");

        }

        public string GetCredentialString()
        {
            if (_username == "anonymous")
            {
                return _username;
            } else
            {
                return _username + " " + _password;
            }
        }

    }
}
