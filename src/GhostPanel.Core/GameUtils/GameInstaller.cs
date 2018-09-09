using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace GhostPanel.Core.GameUtils
{
    class GameInstaller
    {
        private Game _game;
        private SteamCmd _steamCmd;
        private string _installPath;

        public GameInstaller(Game game, SteamCmd steamCmd, string installPath)
        {
            _game = game;
            _steamCmd = steamCmd;
            _installPath = installPath;
            Console.WriteLine("");
        }

        public void installGame()
        {

            _steamCmd.downloadGame(_installPath, _game.SteamAppId);

        }        

        

    }
}
