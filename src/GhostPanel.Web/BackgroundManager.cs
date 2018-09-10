using GhostPanel.Core.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// BotMain
namespace GhostPanel.Web
{
    public class BackgroundManager
    {
        public BackgroundManager(IRepository repository, 
            IBackgroundService backgroundService)
        {
            _repository = repository;
            _backgroundService = backgroundService;
            
        }

        private readonly IRepository _repository;
        private readonly IBackgroundService _backgroundService;
        private ILogger _logger;

        public async Task Run()
        {
            await _backgroundService.Start();
        }

        public async Task Stop()
        {

        }
    }

    
}
