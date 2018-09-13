using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace GhostPanel.Core.Managment
{
    public class SteamCmdGameFiles : IGameFileManager
    {
        private readonly string _steamCmdPath = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD", "steamcmd.exe");
        private readonly string steamCmdUrl = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";
        public readonly string _installDir;
        private SteamCmd _steamCmd;
        private readonly int _steamAppId;
        private readonly ILogger _logger;

        public SteamCmdGameFiles(string installDir, int steamAppId, SteamCmd steamCmd, ILogger<SteamCmdGameFiles> logger)
        {
            _installDir = installDir;
            _steamAppId = steamAppId;
            _logger = logger;
            _steamCmd = steamCmd;
        }

        public void DeleteGameServerFiles(string dir)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Uses SteamCMD to download the game server files for the provided Steam App ID
        /// Return the SteamCMD process for tracking
        /// </summary>
        /// <returns>Process</returns>
        public Process DownloadGameServerFiles()
        {

            if (!DetectSteamCmd())
            {
                InstallSteamCmd();
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = String.Format("+login {0} +force_install_dir \"{1}\" +app_update {2} +quit", _steamCmd.GetCredentialString(), _installDir, _steamAppId);
            start.FileName = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD", "steamcmd.exe");
            Process proc = Process.Start(start);
            return proc;
        }

        public Process UpdateGameServerFiles()
        {
            return DownloadGameServerFiles();
        }

        /// <summary>
        /// Checks if SteamCMD is available at the currently set _steamCmdPath
        /// </summary>
        /// <returns>boolean</returns>
        private bool DetectSteamCmd()
        {
            if (File.Exists(_steamCmdPath))
            {
                _logger.LogDebug("SteamCMD exists at path {path}", _steamCmdPath);
                return true;
            }
            else
            {
                _logger.LogDebug("SteamCMD does not exist at path {path}", _steamCmdPath);
                return false;
            }
        }

        /// <summary>
        /// If SteamCMD is not available at _steamCmdPath download and extract it
        /// </summary>
        private void InstallSteamCmd()
        {
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "steamcmd.zip");
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(steamCmdUrl, savePath);
            }

            ZipFile.ExtractToDirectory(savePath, extractPath);
        }

        


    }
}
