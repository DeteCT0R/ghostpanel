using GhostPanel.Core.Data;
using GhostPanel.Core.GameServerUtils;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management
{
    public class GameServerManagerFactory
    {
        private readonly SteamCredentialWrapper _steamCmd;
        private readonly IRepository _repository;
        private readonly ILoggerFactory _logger;

        public GameServerManagerFactory(SteamCredentialWrapper steamCmd, IRepository repository, ILoggerFactory logger)
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
