using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace GhostPanel.Core
{
    public class SteamCmd
    {
        private string _username;
        private string _password;
        private string _steamCmdPath;
        private string steamCmdUrl = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";

        public SteamCmd(string username, string password)
        {
            _username = username;
            _password = password;
            _steamCmdPath = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD", "steamcmd.exe");

        }

        private string GetCredentialString()
        {
            if (_username == "anonymous")
            {
                return _username;
            } else
            {
                return _username + " " + _password;
            }
        }

        public bool detectSteamCmd()
        {
            if (File.Exists(_steamCmdPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void installSteamCmd()
        {
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "steamcmd.zip");
            string extractPath = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD");
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(steamCmdUrl, savePath);
            }

            ZipFile.ExtractToDirectory(savePath, extractPath);
        }

        public Process downloadGame(string installDir, int appId)
        {

            if (!detectSteamCmd())
            {
                installSteamCmd();
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = String.Format("+login {0} +force_install_dir \"{1}\" +app_update {2} +quit", GetCredentialString(), installDir, appId);
            start.FileName = Path.Combine(Directory.GetCurrentDirectory(), "SteamCMD", "steamcmd.exe");
            Process proc = Process.Start(start);
            return proc;
        }
    }
}
