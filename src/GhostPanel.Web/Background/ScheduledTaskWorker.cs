using GhostPanel.BackgroundServices;
using GhostPanel.Core.Data.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GhostPanel.Web.Background
{
    public class ScheduledTaskWorker : HostedService
    {
        private readonly ILogger _logger;
        private readonly IScheduledTaskService _scheduledTaskService;

        public ScheduledTaskWorker(ILogger<ScheduledTaskWorker> logger, IScheduledTaskService scheduledTaskService)
        {
            _logger = logger;
            _scheduledTaskService = scheduledTaskService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogDebug("Scheduled Task Service Running");
                ICollection<ScheduledTask> tasks = _scheduledTaskService.GetScheduledTasks();

                foreach (var task in tasks)
                {
                    if (_scheduledTaskService.IsTimeToRun(task))
                    {
                        await _scheduledTaskService.ExecuteScheduledTask(task);
                    }
                }

                await Task.Delay(3000);
            }
        }
    }
}
