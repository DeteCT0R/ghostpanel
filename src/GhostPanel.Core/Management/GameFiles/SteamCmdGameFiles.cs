using GhostPanel.Core.Management.GameFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Providers;

namespace GhostPanel.Core.Managment.GameFiles
{
    public class SteamCmdGameFiles : GameFilesBase, IGameFileManager
    {

        private readonly string steamCmdUrl = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";
        private readonly IDefaultDirectoryProvider _defaultDirs;
        private SteamCredentialWrapper _steamCmd;
        private readonly ILogger _logger;

        public SteamCmdGameFiles(SteamCredentialWrapper steamCmd, ILoggerFactory logger, IDefaultDirectoryProvider defaultDirs) : base(logger)
        {
            _logger = logger.CreateLogger<SteamCmdGameFiles>();
            _steamCmd = steamCmd;
            _defaultDirs = defaultDirs;

        }

        

        /// <summary>
        /// Uses SteamCMD to download the game server files for the provided Steam App ID
        /// Return the SteamCMD process for tracking
        /// </summary>
        /// <returns>Process</returns>
        public void DownloadGameServerFiles(GameServer gameServer)
        {

            if (!DetectSteamCmd())
            {
                InstallSteamCmd();
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = String.Format("+login {0} +force_install_dir \"{1}\" +app_update {2} +quit", _steamCmd.GetCredentialString(), gameServer.HomeDirectory, gameServer.Game.SteamAppId);
            start.FileName = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD", "steamcmd.exe");
            Process proc = Process.Start(start);
            //return proc;
        }

        public void UpdateGameServerFiles(GameServer gameServer)
        {
            DownloadGameServerFiles(gameServer);
        }

        /// <summary>
        /// Checks if SteamCMD is available at the currently set _steamCmdPath
        /// </summary>
        /// <returns>boolean</returns>
        private bool DetectSteamCmd()
        {
            if (File.Exists(Path.Combine(_defaultDirs.GetStreamCmdPath(), "steamcmd.exe")))
            {
                _logger.LogDebug("SteamCMD exists at path {path}", _defaultDirs.GetStreamCmdPath());
                return true;
            }
            else
            {
                _logger.LogDebug("SteamCMD does not exist at path {path}", _defaultDirs.GetStreamCmdPath());
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

        public int GetInstallProgress()
        {
            throw new NotImplementedException();
        }
    }
}
