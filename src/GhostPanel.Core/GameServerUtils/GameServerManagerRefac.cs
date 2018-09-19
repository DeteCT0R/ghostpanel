using System;
using System.IO;
using GhostPanel.Core.Background;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.GameServerUtils
{
    public class GameServerManagerRefac : IGameServerManager
    {
        private readonly IGameFileManagerProvider _fileProvider;
        private readonly IBackgroundService _backgroundService;
        private readonly ILoggerFactory _logFactory;
        private readonly ILogger _logger;
        private readonly IServerProcessManager _procManager;
        private readonly IRepository _repository;
        private readonly IPortAndIpProvider _portProvider;
        private readonly IDefaultDirectoryProvider _dirProvider;

        public GameServerManagerRefac(IGameFileManagerProvider fileProvider, 
            IBackgroundService backgroundService, 
            ILoggerFactory logFactory, 
            IServerProcessManagerProvider procManager, 
            IRepository repository,
            IPortAndIpProvider portProvider,
            IDefaultDirectoryProvider dirProvider)
        {
            _fileProvider = fileProvider;
            _logFactory = logFactory;
            _logger = logFactory.CreateLogger<GameServerManagerRefac>();
            _backgroundService = backgroundService;
            _repository = repository;
            _procManager = procManager.GetProcessManagerProvider();
            _portProvider = portProvider;
            _dirProvider = dirProvider;
        }

        public void CreateGameServer(GameServer gameServer)
        {
            gameServer.GamePort = _portProvider.GetNextAvailablePort(gameServer.GameId, gameServer.IpAddress);
            gameServer.QueryPort = _portProvider.GetNextAvailablePort(gameServer.GameId, gameServer.IpAddress);
            gameServer.Status = ServerStatusStates.Installing;
            _repository.Create(gameServer); // Need ID for path
            gameServer.HomeDirectory = Path.Combine(_dirProvider.GetBaseInstallDirectory(), gameServer.Id.ToString());
            _repository.Update(gameServer);
            InstallGameServer(gameServer);
        }

        public void DeleteGameServerFiles(GameServer gameServer)
        {
            var fileProvider = _fileProvider.GetGameFileManager(gameServer);
            try
            {
                if (gameServer.HomeDirectory != null)
                {
                    fileProvider.DeleteGameServerFiles(gameServer);
                }
                else
                {
                    _logger.LogError("Home Directory for game server {id} not set.  Removing from DB", gameServer.Id);
                }
                
                
            } catch (Exception ex)
            {
                throw;
            }
            

        }


        public void InstallGameServer(GameServer gameServer)
        {
            
            var fileProvider = _fileProvider.GetGameFileManager(gameServer);
            Action action = () => fileProvider.DownloadGameServerFiles(gameServer);
            _backgroundService.AddTask(new GenericBackgroundTask(action, _logFactory, string.Format("Install Game Server {0}", gameServer.Game.Id)));


        }

        public void ReinstallGameServer(GameServer gameServer)
        {
            DeleteGameServerFiles(gameServer);
            InstallGameServer(gameServer);
        }

        public void RemoveGameServer(GameServer gameServer)
        {
            DeleteGameServerFiles(gameServer);
            _repository.Remove(gameServer);

        }

        public void RestartServer(GameServer gameServer)
        {
            StopServer(gameServer);
            StartServer(gameServer);
        }

        public void StartServer(GameServer gameServer)
        {
            _procManager.StartServer(gameServer);
        }

        public void StopServer(GameServer gameServer)
        {
            _procManager.StopServer(gameServer);
        }
    }
}
