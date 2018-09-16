using GhostPanel.Core.GameServerUtils;
using System;
using Microsoft.Extensions.Logging;

namespace GhostPanel.Core.Managment
{
    public class CreateServerTask : IQueuedTask
    {
        public bool IsDone;
        public int Pid;
        private IGameServerManager _gameServerManager;
        private readonly ILogger _logger;

        public CreateServerTask(IGameServerManager gameServerManager, ILoggerFactory logger)
        {
            IsDone = false;
            _logger = logger.CreateLogger<CreateServerTask>();
            _gameServerManager = gameServerManager;
        }

        public void Invoke()
        {
            _logger.LogInformation("Running install task for game server ID {id}", _gameServerManager.GetGameServerId());
            _gameServerManager.InstallGameServer();
            IsDone = true;
        }

        bool IQueuedTask.IsDone()
        {
            return IsDone;
        }
    }
}
