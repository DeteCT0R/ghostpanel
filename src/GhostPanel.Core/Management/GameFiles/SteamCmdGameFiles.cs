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
        private ISteamCredentialProvider _steamCmd;
        private readonly ILogger _logger;

        public SteamCmdGameFiles(ISteamCredentialProvider steamCmd, ILoggerFactory logger, IDefaultDirectoryProvider defaultDirs) : base(logger)
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
            start.FileName = Path.Combine(_defaultDirs.GetSteamCmdDirectory(), "steamcmd.exe");
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
            if (File.Exists(Path.Combine(_defaultDirs.GetSteamCmdDirectory(), "steamcmd.exe")))
            {
                _logger.LogDebug("SteamCMD exists at path {path}", _defaultDirs.GetSteamCmdDirectory());
                return true;
            }
            else
            {
                _logger.LogDebug("SteamCMD does not exist at path {path}", _defaultDirs.GetSteamCmdDirectory());
                return false;
            }
        }

        /// <summary>
        /// If SteamCMD is not available at _steamCmdPath download and extract it
        /// </summary>
        private void InstallSteamCmd()
        {
            string savePath = Path.Combine(_defaultDirs.GetSteamCmdDirectory(), "steamcmd.zip");
            // TODO: Catch exception for dir issues
            if (!Directory.Exists(_defaultDirs.GetSteamCmdDirectory()))
            {
                Directory.CreateDirectory(_defaultDirs.GetSteamCmdDirectory());
            }
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(steamCmdUrl, savePath);
            }

            ZipFile.ExtractToDirectory(savePath, _defaultDirs.GetSteamCmdDirectory());
            if (File.Exists(Path.Combine(_defaultDirs.GetSteamCmdDirectory(), "steamcmd.exe")))
            {
                File.Delete(Path.Combine(_defaultDirs.GetSteamCmdDirectory(), "steamcmd.zip"));
            }

        }

        public int GetInstallProgress()
        {
            throw new NotImplementedException();
        }
    }
}
