using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Managment.GameFiles;
using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;
using MediatR;
using GhostPanel.Core.Notifications;

namespace GhostPanel.Core.Management.GameFiles
{
    public class LocalGameFileManager : GameFilesBase, IGameFileManager
    {   
        private readonly ILogger _logger;
        private readonly IDefaultDirectoryProvider _defaultDirs;
        private readonly IMediator _mediator;

        public LocalGameFileManager(ILoggerFactory logger, IDefaultDirectoryProvider defaultDirs, IMediator mediator) : base(logger, mediator)
        {
            _logger = logger.CreateLogger<LocalGameFileManager>();
            _defaultDirs = defaultDirs;
            _mediator = mediator;
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
                    gameServer.GameServerCurrentStats.Status = ServerStatusStates.Stopped;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while trying to extract game files");
                    _mediator.Publish(new ServerInstallStatusNotification("Failed",
                        $"Game server install failed during file extraction: {e.ToString()}"));
                    throw;
                }

            }
            else
            {
                _logger.LogError("Unable to find install files at {path}", fullSourcePath);
                _mediator.Publish(new ServerInstallStatusNotification("Failed",
                    $"Unable to located game files {fullSourcePath}"));
                return;
            }
        }


        public void UpdateGameServerFiles(GameServer gameServer)
        {
            throw new NotImplementedException();
        }



    }
}
