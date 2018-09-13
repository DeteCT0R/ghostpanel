using GhostPanel.Core;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Management;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Core.Managment
{
    public class ServerManagerContainer
    {
        private List<GameServerManager> _serverManagers = new List<GameServerManager>();
        private readonly GameServerManagerFactory _serverManagerFactory;
        private readonly ILogger _logger;
        private readonly IRepository _repository;

        public ServerManagerContainer(GameServerManagerFactory serverManagerFactory, IRepository repository, ILogger<ServerManagerContainer> logger)
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
                GameServerManager manager = _serverManagerFactory.GetGameServerManager();
                manager.SetGameServer(server);
                _serverManagers.Add(manager);
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

        public void RemoveServerManager(GameServerManager manager)
        {
            
        }
    }
}
