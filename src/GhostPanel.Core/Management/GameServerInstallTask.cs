using System;
using System.Collections.Generic;
using System.Text;
using GhostPanel.Core.Managment.GameFiles;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management
{
    public class GameServerInstallTask
    {
        private IGameFileManager _fileManager;
        public string status;
        private ILogger _logger;

        public GameServerInstallTask(IGameFileManager fileManager, ILoggerFactory logger)
        {
            _fileManager = fileManager;
            _logger = logger.CreateLogger<GameServerInstallTask>();
        }

        public void StartInstall()
        {
            status = "Downloading";
            _fileManager.DownloadGameServerFiles();
        }
    }
}
