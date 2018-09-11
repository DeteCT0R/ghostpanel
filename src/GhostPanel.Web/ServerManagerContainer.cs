using GhostPanel.Core;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.GameServerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Web
{
    public class ServerManagerContainer
    {
        private List<GameServerManager> _serverManagers = new List<GameServerManager>();
        private readonly IRepository _repository;

        public ServerManagerContainer(IRepository repository)
        {
            _repository = repository;
            Initialize();
        }

        private void Initialize()
        {
           foreach (var server in _repository.List<GameServer>())
            {
                _serverManagers.Add(new GameServerManager(server, new SteamCmd("anonymous", ""), _repository));
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
