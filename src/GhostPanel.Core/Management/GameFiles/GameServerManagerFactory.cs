using GhostPanel.Core.Config;
using GhostPanel.Core.Data;
using GhostPanel.Core.GameServerUtils;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Management.GameFiles
{
    public class GameServerManagerFactory
    {
        private readonly SteamCredentialWrapper _steamCmd;
        private readonly IRepository _repository;
        private readonly ILoggerFactory _logger;
        private readonly GhostPanelConfig _config;

        public GameServerManagerFactory(SteamCredentialWrapper steamCmd, IRepository repository, ILoggerFactory logger, GhostPanelConfig config)
        {
            _steamCmd = steamCmd;
            _repository = repository;
            _logger = logger;
            _config = config;
        }

        public GameServerManager GetGameServerManager()
        {
            return new GameServerManager(_repository, _steamCmd, _logger, _config);
        }
    }
}
