using GhostPanel.Core.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// BotMain
namespace GhostPanel.Core.Background
{
    public class BackgroundManager
    {
        private readonly IRepository _repository;
        private readonly IBackgroundService _backgroundService;
        private ILogger _logger;

        public BackgroundManager(IRepository repository, 
            IBackgroundService backgroundService,
            ILogger<BackgroundManager> logger)
        {
            _repository = repository;
            _backgroundService = backgroundService;
            _logger = logger;
            
        }        

        public async Task Run()
        {
            _logger.LogInformation("Starting Background Manager");
            await _backgroundService.Start();
        }

        public async Task Stop()
        {

        }
    }

    
}
