using System.Collections.Generic;
using System.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management.GameFiles;
using GhostPanel.Core.Managment.GameFiles;
using Microsoft.Extensions.Logging;
using System.Linq;
using GhostPanel.Core.Exceptions;

namespace GhostPanel.Core.Providers
{
    public class GameFileManagerProvider : IGameFileManagerProvider
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _logFactory;
        private readonly IList<IGameFileManager> _fileManager;

        public GameFileManagerProvider(ILoggerFactory logger, IList<IGameFileManager> fileManager)
        {
            _logger = logger.CreateLogger<GameFileManagerProvider>();
            _logFactory = logger;
            _fileManager = fileManager;
        }

        public IGameFileManager GetGameFileManager(GameServer gameServer)
        {
            _logger.LogDebug("Attempting to determin required GameFileManager for game server {id}", gameServer.Id);
            if (gameServer.Game.SteamAppId == null)
            {
                _logger.LogDebug("No Steam App ID set for game server {id}.  Loading LocalFileManager", gameServer.Id);
                if (gameServer.Game.ArchiveName == null)
                {
                    throw new NoNullAllowedException(string.Format("Game archive not set for Game {0}.  Cannot create local game file manager", gameServer.Game.Id));
                }

                var manager = _fileManager.OfType<LocalGameFileManager>().FirstOrDefault();

                if (manager == null)
                {
                    throw new FailedToLocateGameFileManager(
                        "Unable to locate Game File Manager of Type LocalGameFileManager");
                }
                else
                {
                    return manager;
                }
            }
            else
            {
                _logger.LogDebug("Loading SteamGameFileManager");
                var manager = _fileManager.OfType<SteamCmdGameFiles>().FirstOrDefault();

                if (manager == null)
                {
                    throw new FailedToLocateGameFileManager(
                        "Unable to locate Game File Manager of Type SteamCmdGameFiles");
                }
                else
                {
                    return manager;
                }
            }
        }
    }
}
