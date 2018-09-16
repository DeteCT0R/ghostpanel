using GhostPanel.Core.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostPanel.Core.Background
{
    public class ServerStatusBackgroundManager
    {
        private readonly IRepository _repository;
        private readonly IBackgroundService _serverStatusUpdate;
        private ILogger _logger;

        public ServerStatusBackgroundManager(IRepository repository, IBackgroundService serverStatusUpdate, ILogger logger)
        {
            _repository = repository;
            _serverStatusUpdate = serverStatusUpdate;
            _logger = logger;
        }

        public async Task Run()
        {
            _logger.LogInformation("Starting Server Status Background Manager");
            await _serverStatusUpdate.Start();
        }

        public async Task Stop()
        {

        }

    }
}
