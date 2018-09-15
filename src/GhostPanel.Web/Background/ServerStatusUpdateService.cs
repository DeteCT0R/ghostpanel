using GhostPanel.Core.GameServerUtils;
using GhostPanel.Core.Managment;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GhostPanel.Web.Background
{
    public class ServerStatusUpdateService : IBackgroundService
    {
        private readonly ILogger _logger;
        private readonly ServerManagerContainer _managerContainer;

        public ServerStatusUpdateService(ILogger<ServerStatusUpdateService> logger, ServerManagerContainer managerContainer)
        {
            _logger = logger;
            _managerContainer = managerContainer;
        }

        public void AddTask(IQueuedTask taskToAdd)
        {
            throw new NotImplementedException();
        }

        public async Task Start()
        {
            _logger.LogDebug("Running Server Status Update Loop");
            var mainLoop = Task.Run(async () =>
            {
                while (true)
                {
                    foreach (GameServerManager manager in _managerContainer.GetManagerList())
                    {
                        manager.SetServerStatus();
                    }
                    await Task.Delay(TimeSpan.FromSeconds(4));
                }
            });

            await mainLoop;
        }
    }
}
