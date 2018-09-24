using System;
using System.Collections.Generic;
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

        public ServerStatService(ILoggerFactory logger, IServerProcessManagerProvider procManager, IRepository repository)
        {
            _logger = logger.CreateLogger<ServerStatService>();
            _procManager = procManager.GetProcessManagerProvider();
            _repository = repository;
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

        public async Task UpdateServerQueryStats(GameServer gameServer)
        {
            Type serverInfoType = Type.GetType(gameServer.Protocol.ServerInfoType);
            var query = GameQueryFactory.GetQueryProtocol(gameServer);
            var serverInfo = await query.GetServerInfoAsync() as serverInfoType;
            var playerInfo = await query.GetServerInfoAsync();
            gameServer.CurrentStats.Map = serverInfo.Map;
            gameServer.CurrentStats.CurrentPlayerCount = serverInfo.CurrentPlayers;
            gameServer.CurrentStats.MaxPlayers = serverInfo.MaxPlayers;
            _repository.Update(gameServer);
        }
    }
}
