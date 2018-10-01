using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Data.Specifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Communication.Query
{
    /// <summary>
    /// Write the provide server stats to the GameServerCurrentStats table
    /// </summary>
    public class ServerStatsUpdateNotificationHandler : INotificationHandler<ServerStatsUpdateNotification>
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public ServerStatsUpdateNotificationHandler(IRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task Handle(ServerStatsUpdateNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("---> Server Stats Update Handler");
            /*
            var serverStats = notification.ServerStats;
            var gameServer = _repository.Single(DataItemPolicy<GameServer>.ById(serverStats.gameServerId));
            if (gameServer == null)
            {
                _logger.LogError($"Unable to locate game server with ID {serverStats.gameServerId}");
                return Task.CompletedTask;
            }
            // TODO: Force server stats to load
            _repository.Single(DataItemPolicy<GameServerCurrentStats>.ById(serverStats.gameServerId));
            foreach (PropertyInfo serverInfoProp in serverStats.serverInfo.GetType().GetProperties())
            {
                var currentStatsProp = gameServer.GameServerCurrentStats.GetType().GetProperty(serverInfoProp.Name);
                if (currentStatsProp != null)
                {
                    _logger.LogDebug($"Setting {serverInfoProp.Name} to {serverInfoProp.GetValue(serverStats.serverInfo)}");
                    currentStatsProp.SetValue(gameServer.GameServerCurrentStats, serverInfoProp.GetValue(serverStats.serverInfo));
                }
            }

            _repository.Update(gameServer);
            */
            return Task.CompletedTask;
        }
    }
}
