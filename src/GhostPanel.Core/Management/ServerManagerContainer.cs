using GhostPanel.Core;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
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
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public ServerManagerContainer(IRepository repository, ILogger<ServerManagerContainer> logger)
        {
            _repository = repository;
            _logger = logger;
            Initialize();
        }

        private void Initialize()
        {
           foreach (var server in _repository.List<GameServer>())
            {
                GameServerManager manager = new GameServerManager(server, new SteamCmd("anonymous", ""), _repository, _logger);
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
