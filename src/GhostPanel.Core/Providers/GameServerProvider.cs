using System.Collections.Generic;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Providers
{
    public class GameServerProvider : IGameServerProvider
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;

        public GameServerProvider(ILogger<GameServerProvider> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _logger.LogInformation("GameServerProvider initialized");
        }

        public List<GameServer> GetGameServers()
        {
            _logger.LogDebug("Returning all game servers from GameServerProvider");
            return _repository.List(DataItemPolicy<GameServer>.All());
        }
    }
}
