using GhostPanel.Core.Managment;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Automation System
namespace GhostPanel.Core.Background
{
    public class PanelBackgroundTaskService : IBackgroundService
    {
        private readonly List<IQueuedTask> _tasks = new List<IQueuedTask>();
        private readonly ILogger _logger;
        public PanelBackgroundTaskService(ILogger<PanelBackgroundTaskService> logger)
        {
            _logger = logger;
        }

        public async Task Start()
        {
            //_logger.LogDebug("Running BackgroundService loop");
            var mainLoop = Task.Run(async () =>
            {
                while (true)
                {
                    RunPendingTasks();
                    removeCompleteTasks();
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
            });

            await mainLoop;
        }

        private void RunPendingTasks()
        {
            _logger.LogDebug("Running Pending Tasks");
            foreach (var task in _tasks)
            {
                task.Invoke();
            }
        }

        private void removeCompleteTasks()
        {
            foreach (var task in _tasks)
            {
                if (task.IsDone())
                {
                    _logger.LogDebug("Removing Backgrond Task {task}", task);
                    _tasks.Remove(task);
                }
            }
        }

        public void AddTask(IQueuedTask taskToAdd)
        {
            _logger.LogInformation("Adding task to background queue. {task}", taskToAdd);
            _tasks.Add(taskToAdd);
        }

    }
}
