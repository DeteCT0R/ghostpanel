using System;
using GhostPanel.Core.Background;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Managment.GameFiles;
using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.GameServerUtils
{
    public class GameServerManagerRefac : IGameServerManager
    {
        private readonly IGameFileManagerProvider _fileProvider;
        private readonly IBackgroundService _backgroundService;
        private readonly ILoggerFactory _logFactory;

        public GameServerManagerRefac(IGameFileManagerProvider fileProvider, IBackgroundService backgroundService, ILoggerFactory logFactory)
        {
            _fileProvider = fileProvider;
            _logFactory = logFactory;
            _backgroundService = backgroundService;
        }

        public void DeleteGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }

        public int? GetGameServerId()
        {
            throw new NotImplementedException();
        }

        public void InstallGameServer(GameServer gameServer)
        {
            var fileProvider = _fileProvider.GetGameFileManager(gameServer);
            Action action = () => fileProvider.DownloadGameServerFiles(gameServer);
            _backgroundService.AddTask(new GenericBackgroundTask(action, _logFactory, string.Format("Install Game Server {0}", gameServer.Game.Id)));


        }

        public void ReinstallGameServer(GameServer gameServer)
        {
            throw new NotImplementedException();
        }

        public void StartServer()
        {
            throw new NotImplementedException();
        }

        public void StopServer()
        {
            throw new NotImplementedException();
        }
    }
}
