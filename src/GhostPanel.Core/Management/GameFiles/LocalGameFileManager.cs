using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Managment.GameFiles;
using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using GhostPanel.Core.Data;
using GhostPanel.Core.Notifications;
using MediatR;

namespace GhostPanel.Core.Management.GameFiles
{
    public class LocalGameFileManager : GameFilesBase, IGameFileManager
    {   
        private readonly ILogger _logger;
        private readonly IDefaultDirectoryProvider _defaultDirs;


        public LocalGameFileManager(ILoggerFactory logger, IDefaultDirectoryProvider defaultDirs) : base(logger)
        {
            _logger = logger.CreateLogger<FileServerGameFiles>();
            _defaultDirs = defaultDirs;

        }

        public void DownloadGameServerFiles(GameServer gameServer)
        {
            _logger.LogDebug("Attempting to get local game server files for game {name}", gameServer.Game.Name);
            var fullSourcePath = Path.Combine(_defaultDirs.GetGameFileDirectory(), gameServer.Game.ArchiveName);

            if (File.Exists(fullSourcePath))
            {
                try
                {
                    ZipFile.ExtractToDirectory(fullSourcePath, gameServer.HomeDirectory);
                    _logger.LogInformation("Completed extracting files for game server {id}", gameServer.Id);
                    gameServer.Status = ServerStatusStates.Stopped;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while trying to extract game files");
                    throw;
                }

            }
            else
            {
                _logger.LogError("Unable to find install files at {path}", fullSourcePath);
                return;
            }
        }


        public void UpdateGameServerFiles(GameServer gameServer)
        {
            throw new NotImplementedException();
        }



    }
}
