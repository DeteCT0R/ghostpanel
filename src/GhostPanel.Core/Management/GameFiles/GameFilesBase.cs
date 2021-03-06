﻿using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;
using System.IO;
using MediatR;

namespace GhostPanel.Core.Management.GameFiles
{
    public class GameFilesBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public GameFilesBase(ILoggerFactory logger, IMediator mediator)
        {
            _logger = logger.CreateLogger<GameFilesBase>();
            _mediator = mediator;
        }

        public void DeleteGameServerFiles(GameServer gameServer)
        {
            _logger.LogInformation("Deleting game server files in {path}", gameServer.HomeDirectory);

            // TODO - Add better checking here
            if (gameServer.HomeDirectory == "/")
            {
                _logger.LogError("Dangerous path detected for GameServer Home Directory.  Aboring delete");
                return;
            }
            try
            {
                if (Directory.Exists(gameServer.HomeDirectory))
                {
                    Directory.Delete(gameServer.HomeDirectory, true);
                }
                
            }
            catch (IOException ex)
            {
                _logger.LogError("Failed to delete game server files in {path}", gameServer.HomeDirectory);
                throw;
            }
        }

    }
}
