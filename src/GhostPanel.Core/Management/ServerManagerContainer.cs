using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Management;
using GhostPanel.Core.Management.GameFiles;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GhostPanel.Core.Managment
{
    public class ServerManagerContainer
    {
        private List<GameServerManager> _serverManagers = new List<GameServerManager>();
        private readonly GameServerManagerFactory _serverManagerFactory;
        private readonly ILoggerFactory _logger;
        private readonly IRepository _repository;

        public ServerManagerContainer(GameServerManagerFactory serverManagerFactory, IRepository repository, ILoggerFactory logger)
        {
            _serverManagerFactory = serverManagerFactory;
            _logger = logger;
            _repository = repository;
            Initialize();
        }

        private void Initialize()
        {
           foreach (var server in _repository.List<GameServer>())
            {
                AddAndCreateServerManager(server);
            }
        }

        public List<GameServerManager> GetManagerList()
        {
            return _serverManagers;
        }

        public void AddServerManager(GameServerManager manager)
        {
            _serverManagers.Add(manager);
        }

        public IGameServerManager AddAndCreateServerManager(GameServer server)
        {
            GameServerManager manager = _serverManagerFactory.GetGameServerManager();
            manager.SetGameServer(server);
            _serverManagers.Add(manager);
            return manager;
        }

        public void RemoveServerManager(GameServerManager manager)
        {
            _serverManagers.Remove(manager);
        }
    }
}
