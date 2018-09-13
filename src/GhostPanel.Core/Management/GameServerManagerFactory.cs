using GhostPanel.Core.Data;
using GhostPanel.Core.GameServerUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostPanel.Core.Management
{
    public class GameServerManagerFactory
    {
        private readonly SteamCmd _steamCmd;
        private readonly IRepository _repository;
        private readonly ILoggerFactory _logger;

        public GameServerManagerFactory(SteamCmd steamCmd, IRepository repository, ILoggerFactory logger)
        {
            _steamCmd = steamCmd;
            _repository = repository;
            _logger = logger;
        }

        public GameServerManager GetGameServerManager()
        {
            return new GameServerManager(_repository, _steamCmd, _logger);
        }
    }
}
