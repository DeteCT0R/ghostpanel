using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.BackgroundServices;
using GhostPanel.Core.Providers;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Web.Background
{
    public class ServerStatsRefresh : HostedService
    {
        private readonly ILogger _logger;
        private readonly IServerStatService _statService;
        private readonly IGameServerProvider _gameServerProvider;

        public ServerStatsRefresh(ILogger<ServerStatsRefresh> logger, IServerStatService statService, IGameServerProvider gameServerProvider)
        {
            _logger = logger;
            _statService = statService;
            _gameServerProvider = gameServerProvider;
            _logger.LogDebug("-------> Starting Server Stat Background Service");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var gameServers = _gameServerProvider.GetGameServers();
                foreach (var gameServer in gameServers)
                {
                    _logger.LogDebug("Updating stats for server {id}", gameServer.Id);
                    _statService.CheckServerProc(gameServer);
                    await _statService.UpdateServerQueryStatsAsync(gameServer);
                    await Task.Delay(5000); // TODO: Set in config

                }
                
            }
        }
    }
}
