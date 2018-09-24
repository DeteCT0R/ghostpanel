﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GhostPanel.Core.Data;
using GhostPanel.Core.Data.Model;
using GhostPanel.Core.Management;
using GhostPanel.Core.Providers;
using GhostPanel.Rcon;
using Microsoft.Extensions.Logging;

namespace GhostPanel.BackgroundServices
{
    public class ServerStatService : IServerStatService
    {
        private readonly ILogger _logger;
        private readonly IServerProcessManager _procManager;
        private readonly IRepository _repository;
        private readonly IGameQueryFactory _gameQueryFactory;

        public ServerStatService(ILoggerFactory logger, IServerProcessManagerProvider procManager, IRepository repository, IGameQueryFactory gameQueryFactory)
        {
            _logger = logger.CreateLogger<ServerStatService>();
            _procManager = procManager.GetProcessManagerProvider();
            _repository = repository;
            _gameQueryFactory = gameQueryFactory;
        }

        public GameServer CheckServerProc(GameServer gameServer)
        {
            if (gameServer.CurrentStats.Pid == null)
            {
                if (gameServer.Status != ServerStatusStates.Stopped)
                {
                    _logger.LogDebug("Game server {id} has no PID and state is not stopped.  Setting state to stopped", gameServer.Id);
                    gameServer.Status = ServerStatusStates.Stopped;
                    _repository.Update(gameServer);
                }

                return gameServer;
            }

            if (_procManager.IsRunning(gameServer.CurrentStats.Pid))
            {
                if (gameServer.Status != ServerStatusStates.Running)
                {
                    _logger.LogDebug("Game server {id} is running but status doesn't match.  Setting status to running", gameServer.Id);
                    gameServer.CurrentStats.RestartAttempts = 0;
                    gameServer.Status = ServerStatusStates.Running;
                    _repository.Update(gameServer);
                }

                return gameServer;
            }


            // If we get here there's a PID but server is not running
            _procManager.HandleCrashedServer(gameServer);
            return gameServer;
        }

        public async Task<GameServer> UpdateServerQueryStats(GameServer gameServer)
        {
            var query = _gameQueryFactory.GetQueryProtocol(gameServer);
            var serverInfo = await query.GetServerInfoAsync();
            var playerInfo = await query.GetServerInfoAsync();

            foreach (PropertyInfo serverInfoProp in serverInfo.GetType().GetProperties())
            {
                var currentStatsProp = gameServer.CurrentStats.GetType().GetProperty(serverInfoProp.Name);
                if (currentStatsProp != null)
                {
                    currentStatsProp.SetValue(gameServer.CurrentStats, serverInfoProp.GetValue(serverInfo));
                }
            }

            _repository.Update(gameServer);
            return gameServer;
        }
    }
}